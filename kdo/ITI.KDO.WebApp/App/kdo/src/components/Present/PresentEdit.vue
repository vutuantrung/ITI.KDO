<template>
<div>
    <section>
    <div class="title">
        <h1 v-if="mode == 'create'">CREATE A PRESENT</h1>
        <h1 v-else>EDIT A PRESENT</h1>
    </div>
    </section>

    <b-row>
        <b-col md="4">
        </b-col>
        <b-col md="4">
        <b-card style="margin-left:1%;">
                <form @submit="onSubmit($event)">
                <div class="alert alert-danger" v-if="errors.length > 0">
                <b>Les champs suivants semblent invalides : </b>

                <ul>
                <li v-for="e of errors">{{e}}</li>
                </ul>
            </div>
            <b-card>

                <img :src="'data:image/jpeg;base64,'+ present.picture" style="width:100%" class="img-thumbnail myImage"
                <h4>Chose a picture for your present</h4>
                <div class="input-group">
                    <label class="input-group-btn">
                        <span class="btn btn-primary btn-file">
                            Browse
                            <input type="file" @change="onFileChange" style="display: none;" multiple>
                        </span>
                    </label>
                    <input type="text" class="form-control" v-model="sendImage.name" style="height:37px;" readonly>
                </div>
            </b-card>
            <div class="form-group">
                <label class="required">Present Name:</label>
                <input type="text" v-model="present.presentName" class="form-control" required>
                </div>

                <div class="form-group">
                <label>Price</label>
                <input type="text" v-model="present.price" class="form-control">
                </div>

                <div class="form-group">
                <label>Link Present</label>
                <input type="text" v-model="present.linkPresent" class="form-control">
                </div>

                <!--div class="form-group">
                <label>Category Present</label>
                <b-dropdown right variant="success"text="Category Present">
                    <tr v-for="i of categoryPresentList" :key="i.categoryPresentId">
                        <b-dropdown-item-button @click="choseCategory(i.categoryPresentId, i.categoryName)">{{ i.categoryName }}</b-dropdown-item-button>
                    </tr>
                </b-dropdown>
                <input type="text" v-model="this.categoryChosen" class="form-control" disabled-->
                <!--/div-->

                <button type="submit" class="btn btn-success">Sauvegarder</button>
                </form>
        </b-card>
        </b-col>
    </b-row>
</div>
</template>
<script>
    import { mapActions } from 'vuex';
    import PresentApiService from '../../services/PresentApiService';
    import CategoryPresentApiService from '../../services/CategoryPresentApiService';
    import UserApiService from '../../services/UserApiService';
    import FileApiService from '../../services/FileApiService';
    import AuthService from '../../services/AuthService';
    import Vue from 'vue';
    import Vuex from 'vuex';

  export default {
    data() {
      return {
        user:{},
        present:{},
        mode: null,
        id: null,
        errors: [],
        image: '',
        sendImage: '',
        categoryPresentList: [],
        categoryChosen: "Choose a category.",
        showDismissibleAlert: false,
        TypeOfFile : 1,
        data: new FormData()
      };
    },

    async mounted() {
        var userEmail = AuthService.emailUser();
        this.user = await UserApiService.getUserAsync(userEmail);

        this.categoryPresentList = await CategoryPresentApiService.getCategoryPresentListAsync();

        this.mode = this.$route.params.mode;
        this.id = this.$route.params.id;
        
        if(this.mode == 'edit'){
            try {
                // Here, we use "executeAsyncRequest" action. When an exception is thrown, it is not catched: you have to catch it.
                // It is useful when we have to know if an error occurred, in order to adapt the user experience.
                this.present = await this.executeAsyncRequest(() => PresentApiService.getPresentAsync(this.id));
            }
            catch(error) {
                // So if an exception occurred, we redirect the user to the students list.
                this.$router.replace('/presents');
            }   
        }
    },

    methods: {
        ...mapActions(['executeAsyncRequest']),

        async onSubmit(e){
            e.preventDefault();

            var errors = [];

            if(!this.present.presentName) errors.push("Present Name");
            if(!this.present.price) errors.push("Price");
            if(!this.present.linkPresent) errors.push("Link Present");
            this.errors = errors;

            if(errors.length == 0) {
            try {
                if(this.mode == 'create') {
                    this.present.userId = this.user.userId;
                    this.present.categoryPresentId = this.id;
                    this.present = await this.executeAsyncRequest(() => PresentApiService.createPresentAsync(this.present));
                    if(this.data != null) {
                        this.sendItemImage = await FileApiService
                        .updateFileAsync(this.data, this.present.presentId)
                        .then( () => { FileApiService.typeOfPicture(1, this.present.presentId)});
                     }
                        
                        
                }
                else {
                    await this.executeAsyncRequest(() => PresentApiService.updatePresentAsync(this.present));
                    if(this.data != null) {
                        this.sendItemImage = await FileApiService
                        .updateFileAsync(this.data, this.present.presentId)
                        .then( () => { FileApiService.typeOfPicture(1, this.present.presentId)});
                     }
                }
                this.$router.replace('/presents');
            }
            catch(error) {
                // Custom error management here.
                // In our application, errors throwed when executing a request are managed globally via the "executeAsyncRequest" action: errors are added to the 'app.errors' state.
                // A custom component should react to this state when a new error is added, and make an action, like showing an alert message, or something else.
                // By the way, you can handle errors manually for each component if you need it...
                }
            }
        },

        onFileChange(e) {      
            var files = e.target.files || e.dataTransfer.files;
            if (!files.length)
                return;
            this.createImage(files[0]);
            this.$router.replace('/presents/create');      
        },
        createImage(file) {
            var image = new Image();
            var reader = new FileReader();
            var vm = this;

            this.data.append('files', file);
            this.sendImage = file;
            
            reader.onload = (e) => {
                vm.image = e.target.result;
            };
            reader.readAsDataURL(file);
        },
        async removeImage(e) {
            e.preventDefault();
            this.image = '';
            this.item.photo = '';
            this.data.append('files',  this.item.photo);
            this.sendItemImage = await this.executeAsyncRequest(() => FileApiService.updateFileAsync(this.data, this.present.userId, 1));
            this.mode = undefined;
            this.$router.replace('/presents/create');           
        },
        
        choseCategory(categoryIdChosen, categoryNameChosen){
            this.categoryChosen = categoryNameChosen;
            this.present.categoryPresentId = categoryIdChosen;
        }
    }
  };
</script>
<style lang="less">

</style>