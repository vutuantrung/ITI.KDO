import * as types from '../../mutation-types';

import NotificationApiService from '../../../services/NotificationApiService';
import ParticipantApiService from '../../../services/ParticipantApiService';

export default {
  async [types.REFRESH_NOTIFICATION_EVENT](context, payload) {
    context.state.eventNotificationList = await NotificationApiService.getEventNotificationAsync(
      payload.user.userId
    );
  },
  async [types.RESPONSE_INVITATION_EVENT](context, payload) {
    try {
      if (payload.response === 'yes') {
        context.state.eventModel.userId = payload.userId;
        context.state.eventModel.eventId = payload.eventId;

        await NotificationApiService.setEventInvitationAsync(
          context.state.eventModel
        );
      } else {
        await ParticipantApiService.deleteParticipantAsync(
          context.state.user.userId,
          payload.eventId
        );
      }

      await context.dispatch(types.REFRESH_NOTIFICATION_EVENT);
    } catch (error) {
      context.commit(types.ERROR_HAPPENED, error);
    }
  }
};
