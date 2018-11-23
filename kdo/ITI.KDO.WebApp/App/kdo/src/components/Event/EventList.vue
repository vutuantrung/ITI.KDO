<template>
<div>
    <!--button style="width: 21%; margin-left: 38%;background-color: #81cc67;"type="button" @click="sendEmail('OccasionInvitation')" class="btn btn-lg btn-block btn-success"><i class="fa fa-google" aria-hidden="true"></i> Send Occasion Invitation</button-->

    <section>
        <div class="title">
            <h1>EVENTS LIST</h1>
        </div>
    </section>

    <div class="row" style="margin-left:11%;">
        <div md="12" class="feature-box event" v-for="i of eventList">
            <img :src="'data:image/jpeg;base64,'+ i.picture" class="img-thumbnail myImage" style="height: 120px; width: auto;">
            <div class="eventDiv">
                <span>{{ i.eventName }}</span>
            </div>

            <div v-if = "isCreator(i.userId) == true">
                <b-img src="https://image.freepik.com/icones-gratuites/corbeille_318-55452.jpg" class="delete" @click="deleteEvent(i.eventId)" fluid alt="Responsive image"/>
                <div class="edit">
                    <router-link tag="img" src="https://image.flaticon.com/icons/svg/84/84380.svg" :to="`events/edit/${i.eventId}`"></router-link>
                    <router-link tag="img" class="voirT" src="http://www.icone-png.com/png/24/23722.png" :to="`events/view/${i.eventId}`"></router-link>
                </div>
            </div>

            <div v-if = "isCreator(i.userId) == false">
                <router-link tag="img" class="calendar" src="https://png.icons8.com/metro/540/calendar.png" :to="`events/dateSuggest/${i.eventId}`"></router-link>
                <router-link tag="img" class="voir" src="http://www.icone-png.com/png/24/23722.png" :to="`events/view/${i.eventId}`"></router-link>
                <b-img src="http://cdn.onlinewebfonts.com/svg/img_447829.png"  class="quit" @click="quitEvent(i.eventId) " fluid alt="Responsive image" />
            </div>                
        </div>

        <div md="12" class="feature-box1 test">
            <router-link tag="img" style=" margin-top: -5%;" src="https://blazer-net.com/wp-content/uploads/blazer-nett.png" :to="`events/create`">Add a present</router-link>
            <span style="font-family: cursive; font-size: larger;" >Add an event</span>
        </div>
    </div>
</div>
</template>

<script>
    import { mapActions } from 'vuex';
    import AuthService from "../../services/AuthService";
    import ParticipantApiService from '../../services/ParticipantApiService';
    import EventApiService from '../../services/EventApiService';
    import UserApiService from '../../services/UserApiService';
    
  export default {
    data() {
        return {
            user: {},
            eventList: [],
            eventSuggest: {}
        };
    },
    async mounted() {
        var userEmail = AuthService.emailUser();
        this.user = await UserApiService.getUserAsync(userEmail);
        await this.refreshList();
    },
    methods: {
        ...mapActions(['executeAsyncRequestOrDefault', 'executeAsyncRequest']),
        async refreshList() {
            this.eventList = await EventApiService.getEventListAsync(this.user.userId);
            for (var i = 0; i < this.eventList.length; i++){
                this.eventList[i].dates = this.eventList[i].dates.slice(0, 10);
            }
        },
        async deleteEvent(eventId) {
            try {
                
                await EventApiService.deleteEventAsync(eventId);
                await this.refreshList();
            }
            catch(error) {
            }
        },
        sendEmail(mailType){
            AuthService.sendEmail(mailType);
        },
        isCreator(creatorId){
            if(this.user.userId == creatorId){
                return true;
            } else {
                return false;
            }
        },
        async quitEvent(eventId){
            await ParticipantApiService.deleteParticipantAsync(this.user.userId, eventId);
            await this.refreshList();
        },
        async onSubmit(e, eventId){
            e.preventDefault();
            try {
                this.eventSuggest.eventId = eventId;
                this.eventSuggest.userId = this.user.userId;
                await EventApiService.createEventSuggestAsync(this.eventSuggest);
            }
            catch(error) {
                // Custom error management here.
                // In our application, errors throwed when executing a request are managed globally via the "executeAsyncRequest" action: errors are added to the 'app.errors' state.
                // A custom component should react to this state when a new error is added, and make an action, like showing an alert message, or something else.
                // By the way, you can handle errors manually for each component if you need it...
            }
        },
        changeRoute(){
            this.$router.replace("events/suggest/calendar");
        }
  }
  };
</script>

<style lang="less">
.row{
    margin-top: 10px;
}
/* Section - Title */
/**************************/
.title {background: white; padding: 60px; margin:0 auto; text-align:center;}
.title h1 {font-size:35px; letter-spacing:8px;}
.test div span{
    color: white;
}
.quit {
    width: 7%;
    margin-left: 99%;
    margin-top: -137%;
}
.calendar {
    width: 7%;
    margin-left: 73%;
    margin-top: -110%;
}
.voir{
    width: 7%;
    margin-left: 89%;
    margin-top: -123%;
}
.voirT{
    width: 124%;
    margin-left: 97%;
    margin-top: -206%;
}
.eventDiv{
    position: relative;
    right:0;
    font-size: 24px;
    background-color: #81cc67;
    opacity: 0.8;
    margin-top: 3%;
}
.feature-box{
   // background-image:url("https://financesonline.com/uploads/2017/10/ev.jpg");
    width: 400px;
    height: 200px;
    margin-top: 2%;
    margin-right: 5%;
}
</style>