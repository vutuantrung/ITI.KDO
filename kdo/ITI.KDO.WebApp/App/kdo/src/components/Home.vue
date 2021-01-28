<template lang="">
    <div>
        
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
      contactModel: {},
      eventModel: {},
      show: false,
      contactNotificationList: [],
      eventNotificationList: [],
    };
  },
  computed: {
    ...mapGetters(['isLoading']),
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
</style>