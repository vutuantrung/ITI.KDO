<template>

 <div id="fond" style="margin-top:-24px;">
 
    <b-navbar toggleable="md" type="dark" variant="dark" fixed="top">

    <b-navbar-toggle target="nav_collapse"></b-navbar-toggle>

    <b-collapse is-nav id="nav_collapse">

    <div class="container-fluid">
          <!-- Brand and toggle get grouped for better mobile display -->
      <div class="navbar-header">

        <!-- Right aligned nav items -->
        <b-navbar-nav class="ml-auto">

          <b-navbar-brand href="/Home/">
            <img src="../img/logoKdo.png" style="width:50px"></img>
          </b-navbar-brand>

          <b-navbar-nav v-if="!auth.isConnected">
            <b-nav-item-dropdown left>
              <!-- Using button-content slot -->
              <template slot="button-content">
                Sign In
              </template>
              <b-dropdown-item @click="login('Google')" class="btn btn-lg btn-block btn-primary">Se connecter via Google</b-dropdown-item>
              <b-dropdown-item @click="login('Facebook')" class="btn btn-lg btn-block btn-primary">Se connecter via Facebook</b-dropdown-item>
              <b-dropdown-item @click="login('Base')" class="btn btn-lg btn-block btn-primary">Se connecter via KDO</b-dropdown-item>
            </b-nav-item-dropdown>

            <b-nav-item href="#" @click="register()">Sign Up</b-nav-item>
          </b-navbar-nav>

          <b-navbar-nav v-if="auth.isConnected">

            <b-nav-item  href="/Home/userProfile">Profile</b-nav-item>
            <b-nav-item  href="/Home/events">Event</b-nav-item>
            <b-nav-item href="/Home/contact">My contacts</b-nav-item>
            <b-nav-item href="/Home/events/calendar">Calendar</b-nav-item>
            <b-nav-item href="/Home/presents">My presents</b-nav-item>

            <b-nav-item> 
               <span v-if="contactNotificationList.length != 0 || eventNotificationList.length != 0" class="badge badge-light">
                        <tr>
                            <td colspan="7" id="popoverButton-sync" style="font-size: medium;" class="text-center" >{{this.contactNotificationList.length + this.eventNotificationList.length}} Notifications</td>
                            <b-popover :show.sync="show" target="popoverButton-sync" title="Accept or Decline">
                        <tr v-if="i.senderEmail != userEmail" v-for="i of contactNotificationList">
                          <td>{{ i.senderEmail }} wants to add you as a friend. </td>
                          <td>
                              <button  @click="responseContactInvitation('yes', i.senderEmail, i.recipientsEmail, i.contactId)" class="btn btn-success">A</button>
                              <button @click="responseContactInvitation('no', i.senderEmail, i.recipientsEmail, i.contactId)" class="btn btn-danger">D</button>
                          </td>
                        </tr>
                        <tr v-for="i of eventNotificationList">
                          <td>{{ i.eventName }}</td>
                          <td>
                              <button @click="responseEventInvitation('yes', i.eventId)" class="btn btn-primary">A</button>
                              <button @click="responseEventInvitation('no', i.eventId)" class="btn btn-primary">D</button>
                          </td>
                        </tr>
                          </b-popover>
                      </tr>
               </span>
            </b-nav-item>
            <b-nav-item  href="/Home/contactEmail">Contact Email</b-nav-item>


            <b-nav-item style="position:absolute; right:10px;" href="#"@click="logout()">
            <div class="logoImg">
                    <img class="logoImg" src="https://cdn2.iconfinder.com/data/icons/circle-icons-1/64/power-512.png">
            </div>
            </b-nav-item>

            
          </b-navbar-nav>

        </b-navbar-nav>

      </div>
    </div>

    <div class="progress" v-show="isLoading">
      <div class="progress-bar progress-bar-striped active" role="progressbar" style="width: 100%"></div>
    </div>
    </b-collapse>
    </b-navbar>
    
    <transition name="blink" appear style="margin-top:10%; margin-bottom:10%; position:relative; height:100%;">
      <router-view class="child"></router-view>
    </transition>
  </div>
</template>

<script>
import AuthService from "../services/AuthService";
import $ from 'jquery';
import ContactApiService from '../services/ContactApiService';
import NotificationApiService from '../services/NotificationApiService';
import UserApiService from "../services/UserApiService";
import { mapGetters, mapActions } from "vuex";
import "../directives/requiredProviders";
import '../directives/bsDropdown';
//import Vue from 'vue';


export default {
  data() {
    return {
      userEmail: null,
      user: {},
      contactNotificationList: [],
      eventNotificationList: [],
      contactModel: {},
      eventModel: {},
      show: false
    };

  },
  async mounted() {
    var userEmail = AuthService.emailUser();
    AuthService.registerAuthenticatedCallback(() => this.onAuthenticated());
    this.user = await UserApiService.getUserAsync(userEmail);
    await this.refreshContactNotificationList();
    await this.refreshEventNotificationList();

    

  },

  beforeDestroy() {
    AuthService.removeAuthenticatedCallback(() => this.onAuthenticated());
  },

  computed: {
    ...mapGetters(["isLoading"]),
    auth: () => AuthService
  },

  methods: {
    ...mapActions(['executeAsyncRequest']), 

    async refreshContactNotificationList() {
                this.contactNotificationList = await NotificationApiService.getContactNotificationAsync(this.user.userId);
                console.log("tg : " + this.contactNotificationList.length);
    },
    async refreshEventNotificationList(){
    this.eventNotificationList = await NotificationApiService.getEventNotificationAsync(this.user.userId);
    },
    async responseContactInvitation(response, firstEmail, secondEmail, notificationId){
                if(response == 'yes'){
                    try {
                        this.contactModel.senderEmail = firstEmail;
                        this.contactModel.recipientsEmail = secondEmail;

                        await NotificationApiService.setContactInvitationAsync(this.contactModel);
                    } catch (error) {
                        
                    }
                }else{
                    try {
                        await ContactApiService.deleteContactAsync(notificationId);
                    } catch (error) {
                        
                    }
                }
                await this.refreshContactNotificationList();
            },

    async responseEventInvitation(response, eventId){
        
        if(response == 'yes'){
            try {
                var modelEvent = {};
                this.eventModel.userId = this.user.userId;
                this.eventModel.eventId = eventId;
                
                await NotificationApiService.setEventInvitationAsync(this.eventModel);
            } catch (error) {

            }
        }else{
            try {
                await ParticipantApiService.deleteParticipantAsync(this.user.userId, eventId);
            } catch (error) {
                
            }
        }
        await this.refreshEventNotificationList();
    },
    login(provider) {
      AuthService.login(provider);
    },

    register() {
      AuthService.register();
    },

    logout(){
      this.$router.replace('/logout');
    },

    onAuthenticated() {
      this.$router.replace("/");
    }
  }
};
</script>

<style lang="less">
.row{
  margin-left:1%;
  margin-right:0!important;
}

.active {
    background-color: #4CAF50 !important;
}
.icon-bar {
    height: 100%;
    width: max-content;
    background-color: #343a40; /* Dark-grey background */
    position: fixed;

}
.logoImg{
  
  position: absolute;
  right: 10px;
  width: 205%;
  :hover {
    transform: scale(1.1);

  }
}

.icon-bar a {
    display: block; /* Make the links appear below each other instead of side-by-side */
    text-align: center; /* Center-align text */
    transition: all 0.3s ease; /* Add transition for hover effects */
    color: white; /* White text color */
    font-size: 18px; /* Increased font-size */
    font-weight:600px;
    line-height: 30px;
    padding-top: 40%;

}

header {
  width: 100%;
  background-size: 100%;
  height:400px;
  color: #fff;
  text-align: center;
  text-shadow: 0 1px 3px rgba(0, 0, 0, .5);
}

.popover {
max-width: 100% !important;
}

.header {
  width: 100%;
  height: 100%;
  min-height: 100%;
  -webkit-box-shadow: inset 0 0 100px rgba(0, 0, 0, .5);
  box-shadow: inset 0 0 100px rgba(0, 0, 0, .5);
}
.icon-bar a:hover {
color: #bcbcbc;
}

.active {
    background-color: #4CAF50; /* Add an active/current color */
}
</style>
<style lang="less">
  @import "../styles/global.less";
  @media screen and (max-height: 450px) {
    .icon-bar {padding-top: 15px;}
    .icon-bar a {font-size: 18px;}
}
.footer {
   position:sticky;
   left: 0;
   bottom: 0;
   width: 100%;
   background-color: #343a40;
   color: white;
   text-align: center;
}

.blink-enter-active {
  transition: opacity 1s ease;
}

.blink-enter, .blink-leave-active {
  opacity: 0
}
</style>