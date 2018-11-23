<template>
<div>
    <section>
    <div class="title">
        <h1>CONTACTS</h1>
    </div>
    </section>

    <div style="margin-left:1%; margin-right:1%;display:inline-flex;width:100%;flex-wrap:wrap;">
        <div class="borderCard" v-for="i of friendsList" :key="i.userId">
            
            <div class="contactCard largeur">
                <div class="contactImg"><img src="http://officegabrielperi-vierzon-notaires.fr/wp-content/uploads/2014/07/unknown.png"></div>
                <div class="contactName">{{i.lastName + i.firstName}}</div>
                <b-img src="https://icon-icons.com/icons2/614/PNG/512/mail-black-envelope-symbol_icon-icons.com_56519.png" class="phoneIcon"/>
                <div class="contactEmail">{{i.phone}}</div>
                <b-img src="https://d30y9cdsu7xlg0.cloudfront.net/png/52971-200.png" class="mailIcon"/>
                <div class="contactPhone">{{i.email}}</div>
            </div>
            
        </div>
    </div>

    <b-row style="margin-top: 2%;">
        <b-col md="3">
        </b-col>
        <b-col md="7">
                <form @submit="onSubmit($event)">
                <input v-model="recipientsEmail" placeholder="Find friend's email">
                <!--button type="submit" class="btn btn-primary">Send friend request</button-->

                <b-btn type="submit" @click="showDismissibleAlert=true" variant="info" class="m-1">
                Send friend request
                </b-btn>
                </form>
        </b-col>
        <b-col md="2">
        </b-col>
    </b-row>
</div>
</template>
<script>
   import Vue from "vue";
    import { mapActions } from 'vuex';
    import AuthService from "../../services/AuthService";
    import UserApiService from "../../services/UserApiService";
    import NotificationApiService from "../../services/NotificationApiService";
    import ContactApiService from "../../services/ContactApiService";
    

export default {
    data() {
        return {
            endpoint: null,
            user:{},
            receivePerson: {},
            recipientsEmail: null,
            model:{},
            listFacebookFriends: {},
            friendsList: [],
            nbline: 0,
            showDismissibleAlert: false
        };
    },

    async mounted() {
        var userEmail = AuthService.emailUser();
        this.user = await UserApiService.getUserAsync(userEmail);
        await this.refreshList();
    },

    methods: {
        ...mapActions(['executeAsyncRequest']),

        async refreshList() {
            this.friendsList = await ContactApiService.getFriendsAsync(this.user.userId);
            this.nbline = Math.trunc((this.friendsList.length + 5) / 6);
        },

        sendEmail(mailType){
            AuthService.sendEmail(mailType);
        },

        async getFacebookFriends(){
            try {
                    this.listFacebookFriends = await FacebookApiService.getFacebookFriends();
                }
                catch(error) {
                // Custom error management here.
                // In our application, errors throwed when executing a request are managed globally via the "executeAsyncRequest" action: errors are added to the 'app.errors' state.
                // A custom component should react to this state when a new error is added, and make an action, like showing an alert message, or something else.
                // By the way, you can handle errors manually for each component if you need it...
                }
            
        },

        async onSubmit(e){
            e.preventDefault();

            var errors = [];

            if(this.friendEmail) errors.push("Friend Email");

            this.errors = errors;
            if(errors.length == 0) {
                try {
                    var recipientsUser = await UserApiService.getUserAsync(this.recipientsEmail);

                    this.model.userId = this.user.userId;
                    this.model.friendId = recipientsUser.userId;
                    this.model.invitaion = false;

                    await this.executeAsyncRequest(() => ContactApiService.createContactAsync(this.model));
                }
                catch(error) {
                // Custom error management here.
                // In our application, errors throwed when executing a request are managed globally via the "executeAsyncRequest" action: errors are added to the 'app.errors' state.
                // A custom component should react to this state when a new error is added, and make an action, like showing an alert message, or something else.
                // By the way, you can handle errors manually for each component if you need it...
                }
            }
        }
    }
};
</script>
<style lang="less">

.borderCard{
    position: relative;
    width:300px; 
    height:200px;
	border-radius: 10px;
	-webkit-border-radius: 10px;
	margin: 15px;
	padding-top: 10px;
}

.phoneIcon{
    position: absolute;
    width: 25px;
    height: 25px;
    top: 105px;
    left: 10px;
    display: none;
    background-color: transparent;
}

.mailIcon{
    position: absolute;
    width: 25px;
    height: 25px;
    top: 130px;
    left: 10px;
    display: none;
    background-color: transparent;
}

.contactCard{
    width: 180px;
    height: 150px;
    margin: auto;
}

.contactCard:hover{
    box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
    width: 300px;
}

.contactImg{
    position: absolute;
    top: -40px;
    left: 100px;
    width: 100px;
    height: 100px;
    filter: blur(2px);
}

.contactPhone{
    position: absolute;
    top: 105px;
    left: 50px;
    width: 100px;
    height: 100px;
    display: none;
}

.contactEmail{
    position: absolute;
    top: 130px;
    left: 50px;
    width: 100px;
    height: 100px;
    display: none;
}

.contactImg img{
   height: 100%;
   width: 100%;
   border-radius: 50%;
}

.contactName{
    position: absolute;
    font-size: 20px;
    top: 60px;
    left: 95px;
    width: 100px;
    height: 100px;
}

.largeur{
    transition: width 0.5s ease-out;
	-webkit-transition: width 0.5s ease-out;
}

.largeur:hover{
    width: 100%;
    height: 80%;
}

.borderCard:hover .contactImg{
    filter: blur(0);
}

.contactCard:hover .contactEmail{
    display: inline-block;
}

.contactCard:hover .contactPhone{
    display: inline-block;
}

.contactCard:hover .mailIcon{
    display: inline-block;
}

.contactCard:hover .phoneIcon{
    display: inline-block;
}
</style>