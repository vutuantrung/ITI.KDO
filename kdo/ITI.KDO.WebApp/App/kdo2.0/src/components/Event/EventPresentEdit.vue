<template>
<div>
    <section>
    <div class="title">
        <h1 v-if="mode == 'create'">CREATE A PRESENT</h1>
        <h1 v-else>EDIT A PRESENT</h1>
    </div>
    </section>

    <b-row>
        <b-col md="2">
        </b-col>
        <b-col md="8">
            <b-card>
                <form @submit="onSubmit($event)">
            <div class="alert alert-danger" v-if="errors.length > 0">
                <b>Les champs suivants semblent invalides : </b>

                <ul>
                    <li v-for="e of errors">{{e}}</li>
                </ul>
            </div>
            
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

            <div class="form-group">
                <label>Category Present</label>
                <br>
                <b-dropdown right variant="success" style="margin-bottom: 1%;"text="Category Present">
                    <tr v-for="i of categoryPresentList">
                        <b-dropdown-item-button @click="choseCategory(i.categoryPresentId, i.categoryName)">{{ i.categoryName }}</b-dropdown-item-button>
                    </tr>
                </b-dropdown>
                <input type="text" v-model="this.categoryChosen" class="form-control" disabled>
            </div>

            <div>
                Recipiant
                <b-form-select v-model="quantity.recipientId" :options="recipientList" class="mb-3">
                </b-form-select>
            </div>

            <div class="form-group">
                <label>Quantity</label>
                <input type="text" v-model="quantity.quantity" class="form-control">
            </div>

            <button type="submit" class="btn btn-success">Sauvegarder</button>
            <b-button v-if="mode == 'edit'" @click="DeletePresent()" class="btn btn-danger">Delete the present</b-button>
        </form>
            </b-card>
        </b-col>
    </b-row>
</div>
</template>
<script>
   import { mapActions } from 'vuex';
    import PresentApiService from '../../services/PresentApiService';
    import QuantityApiService from '../../services/QuantityApiService';
    import CategoryPresentApiService from '../../services/CategoryPresentApiService';
    import UserApiService from "../../services/UserApiService";
    import AuthService from "../../services/AuthService";
    import ParticipantApiService from '../../services/ParticipantApiService';

  export default {
    data() {
      return {
        user:{},
        present:{},
        quantity:{},
        recipiant:{},
        mode: null,
        quantityId: null,
        eventId: null,
        errors: [],
        categoryPresentList: [],
        participantList: [],
        recipientList: [],
        categoryChosen: "Choose a category."
      };
    },

    async mounted() {
        var userEmail = AuthService.emailUser();
        this.user = await UserApiService.getUserAsync(userEmail);

        this.categoryPresentList = await CategoryPresentApiService.getCategoryPresentListAsync();

        this.mode = this.$route.params.mode;
        this.quantityId = this.$route.params.qid;
        this.eventId = this.$route.params.eid;
        this.participantList = await ParticipantApiService.getParticipantListAsync(this.user.userId, this.eventId);

        for(var i = 0; i < this.participantList.length; i++)
        {
            var aux = await UserApiService.getUserByIdAsync(this.participantList[i].userId);

            if (this.participantList[i].participantType == true)                    
                this.recipientList.push({value: aux.userId, text: aux.firstName});
        }
        
        if(this.mode == 'edit'){
            try {
                // Here, we use "executeAsyncRequest" action. When an exception is thrown, it is not catched: you have to catch it.
                // It is useful when we have to know if an error occurred, in order to adapt the user experience.
                this.quantity = await this.executeAsyncRequest(() => QuantityApiService.getQuantityAsync(this.quantityId));
                this.present = await this.executeAsyncRequest(() => PresentApiService.getPresentAsync(this.quantity.presentId));
            }
            catch(error) {
                // So if an exception occurred, we redirect the user to the students list.
                this.$router.go(-1);
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
            if(!this.present.categoryPresentId) errors.push("Category Present Id");

            if(!this.quantity.quantity) errors.push("Quantity");
            this.quantity.eventId = this.eventId;
            this.quantity.nominatorId = this.user.userId;
            if(!this.quantity.recipientId) errors.push("Recipiant");

            this.errors = errors;

            if(errors.length == 0) {
            try {
                if(this.mode == 'create') {
                    this.present.userId = 0;
                    var aux = await this.executeAsyncRequest(() => PresentApiService.createPresentAsync(this.present));
                    this.quantity.presentId = aux.presentId;
                    await this.executeAsyncRequest(() => QuantityApiService.createQuantityAsync(this.quantity));
                }
                else {
                    await this.executeAsyncRequest(() => PresentApiService.updatePresentAsync(this.present));
                    await this.executeAsyncRequest(() => QuantityApiService.updateQuantityAsync(this.quantity));
                }
                this.$router.go(-1);
            }
            catch(error) {
                // Custom error management here.
                // In our application, errors throwed when executing a request are managed globally via the "executeAsyncRequest" action: errors are added to the 'app.errors' state.
                // A custom component should react to this state when a new error is added, and make an action, like showing an alert message, or something else.
                // By the way, you can handle errors manually for each component if you need it...
                }
            }
        },

        choseCategory(categoryIdChosen, categoryNameChosen){
            this.categoryChosen = categoryNameChosen;
            this.present.categoryPresentId = categoryIdChosen;
        },

        async DeletePresent(){
            await this.executeAsyncRequest(() => QuantityApiService.deleteQuantityAsync(this.quantityId));
            this.$router.go(-1);
        }
    }
  };
</script>
<style lang="less">
</style>