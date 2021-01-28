<template>
  <div class="container">
    <h1>Contact notifications list</h1>

    <table class="table table-striped table-hover table-bordered">
      <thead>
        <tr>
          <th>From</th>
          <th>To</th>
          <th>Options</th>
        </tr>
      </thead>

      <tbody>
        <tr v-if="contactNotificationList.length === 0">
          <td colspan="7" class="text-center">No notification</td>
        </tr>

        <tr v-for="cNoti of contactNotificationList" :key="cNoti.contactId">
          <td>{{ cNoti.senderEmail }}</td>
          <td>{{ cNoti.recipientsEmail }}</td>
          <td>
            <button
              @click="resContactInvite('yes', cNoti)"
              class="btn btn-primary"
            >
              Accept
            </button>
            <button
              @click="resContactInvite('no', cNoti)"
              class="btn btn-primary"
            >
              Decline
            </button>
          </td>
        </tr>
      </tbody>
    </table>

    <table class="table table-striped table-hover table-bordered">
      <thead>
        <tr>
          <th>Event Name</th>
          <th>Descriptions</th>
          <th>Options</th>
        </tr>
      </thead>

      <tbody>
        <tr v-if="eventNotificationList.length == 0">
          <td colspan="7" class="text-center">No notification</td>
        </tr>

        <tr v-for="eNoti of eventNotificationList" :key="eNoti.eventId">
          <td>{{ eNoti.eventName }}</td>
          <td>{{ eNoti.description }}</td>
          <td>
            <button
              @click="resEventInvite('yes', eNoti)"
              class="btn btn-primary"
            >
              Accept
            </button>
            <button
              @click="resEventInvite('no', eNoti)"
              class="btn btn-primary"
            >
              Decline
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
<script>
import { mapActions, mapGetters } from 'vuex';

import * as types from '../store/mutation-types';

import AuthService from '../services/AuthService';
import UserApiService from '../services/UserApiService';

export default {
  data() {
    return {};
  },

  computed: {
    ...mapGetters(['contactNotificationList', 'eventNotificationList']),
  },

  async mounted() {
    var userEmail = AuthService.emailUser();
    this.user = await UserApiService.getUserAsync(userEmail);
    await this.refreshContactNotificationList();
    await this.refreshEventNotificationList();
  },

  methods: {
    ...mapActions({
      refreshContactNotificationList: types.REFRESH_NOTIFICATION_CONTACT,
      refreshEventNotificationList: types.REFRESH_NOTIFICATION_EVENT,
      responseContactInvitation: types.RESPONSE_INVITATION_CONTACT,
      responseEventInvitation: types.RESPONSE_INVITATION_PRESENT,
    }),

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
  },
};
</script>
<style lang="">
</style>