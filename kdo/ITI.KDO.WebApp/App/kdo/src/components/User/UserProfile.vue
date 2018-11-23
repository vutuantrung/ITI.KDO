<template>
<div>
    <section>
        <div class="title">
            <h1>YOUR PROFILE</h1>
        </div>
    </section>

    <b-row style="margin-right: 3% !important;">
        <b-col md="3" offset-md="2">
            <b-card     tag="article"
                        style="max-width: 20rem;"
                        class="text-center mb-2">

                <img :src="'data:image/jpeg;base64,'+ item.photo" style="width:100%" class="img-thumbnail myImage" img-alt="Image" img-top/>
                <h4>Chose a picture</h4>
                
                <div class="input-group">
                    <label class="input-group-btn">
                        <span class="btn btn-primary btn-file">
                            Browse
                            <input type="file" @change="onFileChange" style="display:none;" multiple>
                        </span>
                    </label>
                    <input type="text" class="form-control" style="height:37px;" v-model="sendImage.name" readonly>
                </div>

                <b-row style="margin-left: 0%;">
                    <b-col style="padding: 0%;">
                        <button v-show="item.photo != null || image != null" class="btn btn-success" @click="removeImage($event)">Delete picture</button>
                    </b-col>

                    <b-col style="padding: 0%;" @click="setPhoto()">
                        <button type="button" class="btn btn-success" aria-label="Left Align">
                            <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span>
                            Set picture
                        </button>
                    </b-col>
                </b-row>  
            </b-card>
        </b-col>

        <b-col md="5">
            <b-card title="Edit your Profile">
                <h1 class="text-center">
                    {{item.firstName}} {{item.lastName}}
                </h1>

                <b-form  asp-controller="Account" asp-action="Register" method="post" @submit="onSubmit($event)" >
                    <b-col>
                        <b-form-group label="Email:">
                            <b-form-input asp-for="Email" class="form-control" v-model="auth.email">
                            <span asp-validation-for="Email"></span>
                            </b-form-input>
                        </b-form-group>
                    </b-col>

                    <b-col>
                        <b-form-group  label="Firstname:">
                            <b-form-input  asp-for="FirstName" class="form-control" v-model="item.firstName">
                            <span asp-validation-for="FirstName"></span>
                            </b-form-input>
                        </b-form-group>
                    </b-col>

                    <b-col>
                        <b-form-group  label="Lastname:">
                            <b-form-input asp-for="LastName" class="form-control" v-model="item.lastName">
                            <span asp-validation-for="LastName"></span>
                            </b-form-input>
                        </b-form-group>
                    </b-col>

                    <b-col>
                        <b-form-group  label="BirthDate:">
                            <b-form-input asp-for="Birthdate" class="form-control" type="date" v-model="item.birthdate">
                            <span asp-validation-for="Birthdate"></span>
                            </b-form-input>
                        </b-form-group>
                    </b-col>
                    
                    <b-col>
                        <b-button type="submit" variant="primary">Submit</b-button>
                        <b-button @click="modifyPassword()" variant="primary">Modify password</b-button>
                    </b-col>
                </b-form>
            </b-card>
        </b-col>
    </b-row>
</div>

</div>
</template>
<script>
    import AuthService from "../../services/AuthService";
    import UserApiService from "../../services/UserApiService";
    import FileApiService from "../../services/FileApiService";
    import { mapGetters, mapActions } from "vuex";
    import "../../directives/requiredProviders";
    import Vue from 'vue';
    import Vuex from 'vuex';

    export default {
        data() {
            return {
                userEmail: null,
                item: {},
                image: '',
                sendImage: '',
                errors: [],
                data: new FormData(),
                TypeOfFile: 0
            };
        },

        computed: {
            ...mapGetters(["isLoading"]),
            auth: () => AuthService
        },

        async mounted() {
            var userEmail = AuthService.emailUser();
            this.item = await UserApiService.getUserAsync(userEmail);
        },

        methods: {
           ...mapActions(['notifyLoading', 'notifyError']),

            modifyPassword(){
                AuthService.modifyPassword();
            },

            async refresh(){
                this.activeUser = await UserApiService.getUserAsync(this.item.userEmail);
                try {
                    this.id = this.$route.query.Id;
                    this.item = await UserApiService.getUserAsync(this.id);
                }
                catch(error) {
                    this.item = this.activeUser;
                }
            },

            async onSubmit(e) {
                try{
                    await UserApiService.updateUserAsync(this.item);
                    if(item.sendImage != null) setPhoto();
                }
                catch (error){
                    this.notifyError(error);
                    
                    // Custom error management here.
                    // In our application, errors throwed when executing a request are managed globally via the "executeAsyncRequest" action: errors are added to the 'app.errors' state.
                    // A custom component should react to this state when a new error is added, and make an action, like showing an alert message, or something else.
                    // By the way, you can handle errors manually for each component if you need it...
                }
                finally {
                    this.notifyLoading(false);
                 }
                 //this.$router.replace('userProfil');
            },

            async setPhoto() {
              
                if(this.data != null)
                {
                    this.sendItemImage = await FileApiService
                        .updateFileAsync(this.data, this.item.userId)
                        .then( () => { FileApiService.typeOfPicture(this.typeOfFile, this.item.userId)});
                }  
     
            },

            onFileChange(e) {        
                var files = e.target.files || e.dataTransfer.files;
                if (!files.length)
                    return;
                this.createImage(files[0]);
                this.refresh();
                this.$router.replace('userProfile');      
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
                this.file = '';
                this.data.append('files',  this.file);
                if(this.data != null)
                    await FileApiService.deletePicture( 0,this.item.userId );                        
                this.refresh();
                this.$router.replace('userProfil');           
            }
        }
    };
</script>

<style lang="less" scoped>
.row {
    margin-top:1%;
    margin-left:3%;
}

/* Section - Title */
/**************************/
.title {
    background: white;
    padding: 60px;
    margin-top: 50px;
    text-align: center;
}

.title h1 {
    font-size:35px;
    letter-spacing:8px;
}

.progress {
    margin: 0px;
    padding: 0px;
    height: 5px;
}

a.router-link-active {
    font-weight: bold;
}
.card{
    color:#343a40;
    margin-right: -15px;
    margin-left: -15px;
    padding:6px;

}
.card-text {
    position:center;
}

</style>

<style lang="less">
    @import "../../styles/global.less";
</style>