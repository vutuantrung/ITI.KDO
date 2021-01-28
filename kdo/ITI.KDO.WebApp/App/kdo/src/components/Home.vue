<template>
  <div id="fond">
    <b-navbar toggleable="md" type="dark" variant="dark" fixed="top">
      <b-navbar-toggle target="nav_collapse"></b-navbar-toggle>
      <b-collapse is-nav id="nav_collapse">
        <div class="container-fluid">
          <div class="navbar-header">
            <b-nav-bar class="ml-auto">

              <b-navbar-brand href="/Home/">
                <img src="../assets/img/logoKdo.png" style="width: 50px" />
              </b-navbar-brand>

              <b-navbar-nav v-if="!auth.isConnected">
                <b-nav-item-dropdown right text="Sign In" toggle-class="nav-link-custom">
                  <b-dropdown-item
                    @click="login('Google')"
                    class="btn btn-lg btn-block btn-primary"
                    >Se connecter via Google</b-dropdown-item
                  >
                  <b-dropdown-item
                    @click="login('Facebook')"
                    class="btn btn-lg btn-block btn-primary"
                    >Se connecter via Facebook</b-dropdown-item
                  >
                  <b-dropdown-item
                    @click="login('Base')"
                    class="btn btn-lg btn-block btn-primary"
                    >Se connecter via KDO</b-dropdown-item
                  >
                </b-nav-item-dropdown>

                <b-nav-item href="#" @click="register()">Sign Up</b-nav-item>
              </b-navbar-nav>

              <b-navbar-nav v-else>
                <b-nav-item href="/Home/userProfile">Profile</b-nav-item>
                <b-nav-item href="/Home/events">Event</b-nav-item>
                <b-nav-item href="/Home/contact">My contacts</b-nav-item>
                <b-nav-item href="/Home/events/calendar">Calendar</b-nav-item>
                <b-nav-item href="/Home/presents">My presents</b-nav-item>

                <b-nav-item>
                  <span
                    v-if="numberNotification() !== 0"
                    class="badge badge-light"
                  >
                    <tr>
                      <td
                        colspan="7"
                        id="popoverButton-sync"
                        style="font-size: medium"
                        class="text-center"
                      >
                        {{ numberNotification() + 'Notification' }}
                      </td>

                      <b-popover v-show="show" target="popoverButton-sync" title="Accept or Decline">
                    </tr>
                    <tr v-for="cNoti of contactNotificationList" :key="cNoti.contactId">
                      <div v-if="cNoti !== userEmail">
                        <td>{{ cNoti.senderEmail }} wants to add you as a friend.</td>
                          <td>
                              <button @click="resContactInvite('yes', cNoti)" class="btn btn-success">A</button>
                              <button @click="resContactInvite('no', cNoti)" class="btn btn-danger">D</button>
                          </td>
                      </div>
                    </tr>
                    <tr v-for="eNoti of eventNotificationList" :key="eNoti.eventId">
                        <td>{{ eNoti.eventName }}</td>
                        <td>
                            <button @click="resEventInvite('yes', eNoti)" class="btn btn-primary">A</button>
                            <button @click="resEventInvite('no', eNoti)" class="btn btn-primary">D</button>
                        </td>
                    </tr>
                  </span>
                </b-nav-item>
                <b-nav-item  href="/Home/contactEmail">Contact Email</b-nav-item>

                <b-nav-item style="position:absolute; right:10px;" href="#" @click="logout()">
                  <img class="logoImg" src="https://cdn2.iconfinder.com/data/icons/circle-icons-1/64/power-512.png" />
                </b-nav-item>
              </b-navbar-nav>

            </b-nav-bar>
          </div>
        </div>

        <div class="progress" v-show="isLoading">
          <div
            class="progress-bar progress-bar-striped active"
            role="progressbar"
            style="width: 100%"
          ></div>
        </div>
      </b-collapse>
    </b-navbar>
  </div>
</template>
<script>
import { mapGetters, mapActions } from 'vuex';

import AuthService from '../services/AuthService';
import * as types from '../store/mutation-types';

export default {
  data() {
    return {
      userEmail: null,
      user: {},
      show: false,
    };
  },
  computed: {
    ...mapGetters([
      'isLoading',
      'contactNotificationList',
      'eventNotificationList',
      'contactModel',
      'eventModel',
    ]),
    auth: () => AuthService,
  },
  methods: {
    ...mapActions({
      refreshContactNotificationList: types.REFRESH_NOTIFICATION_CONTACT,
      refreshEventNotificationList: types.REFRESH_NOTIFICATION_EVENT,
      responseContactInvitation: types.RESPONSE_INVITATION_CONTACT,
      responseEventInvitation: types.RESPONSE_INVITATION_PRESENT,
    }),

    login(provider) {
      AuthService.login(provider);
    },

    logout() {
      AuthService.logout();
      this.$router.push('/logout');
    },

    onAuthenticated() {
      this.$router.push('/');
    },

    numberNotification() {
      return (
        this.contactNotificationList.length + this.eventNotificationList.length
      );
    },

    resContactInvite(res, notification) {
      const payload = {
        response: res,
        senderEmail: notification.senderEmail,
        recipientsEmail: notification.recipientsEmail,
        notificationId: notification.contactId,
      };

      this.responseContactInvitation(payload);
    },

    resEventInvite(res, notification) {
      const payload = {
        userId: this.user.userId,
        response: res,
        eventId: notification.notification,
      };

      this.responseEventInvitation(payload);
    },

    beforeUnmount() {
      AuthService.removeAuthenticatedCallback(() => this.onAuthenticated());
    },
  },
};
</script>

<style lang="">
.row {
  margin-left: 1%;
  margin-right: 0 !important;
}

.active {
  background-color: #4caf50 !important;
}
.icon-bar {
  height: 100%;
  width: max-content;
  background-color: #343a40; /* Dark-grey background */
  position: fixed;
}
.logoImg {
  position: absolute;
  right: 10px;
  width: 205%;
}

.logoImg:hover {
  transform: scale(1.1);
}

.icon-bar a {
  display: block; /* Make the links appear below each other instead of side-by-side */
  text-align: center; /* Center-align text */
  transition: all 0.3s ease; /* Add transition for hover effects */
  color: white; /* White text color */
  font-size: 18px; /* Increased font-size */
  font-weight: 600px;
  line-height: 30px;
  padding-top: 40%;
}

header {
  width: 100%;
  background-size: 100%;
  height: 400px;
  color: #fff;
  text-align: center;
  text-shadow: 0 1px 3px rgba(0, 0, 0, 0.5);
}

.popover {
  max-width: 100% !important;
}

.header {
  width: 100%;
  height: 100%;
  min-height: 100%;
  -webkit-box-shadow: inset 0 0 100px rgba(0, 0, 0, 0.5);
  box-shadow: inset 0 0 100px rgba(0, 0, 0, 0.5);
}
.icon-bar a:hover {
  color: #bcbcbc;
}

.active {
  background-color: #4caf50; /* Add an active/current color */
}
@import '../styles/global.less';
@media screen and (max-height: 450px) {
  .icon-bar {
    padding-top: 15px;
  }
  .icon-bar a {
    font-size: 18px;
  }
}
.footer {
  position: sticky;
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

.blink-enter,
.blink-leave-active {
  opacity: 0;
}

.fond {
  margin-top: -24px;
}
</style>