import * as types from '../../mutation-types';

import NotificationApiService from '../../../services/NotificationApiService';
import ContactApiService from '../../../services/ContactApiService';

export default {
  async [types.REFRESH_NOTIFICATION_CONTACT](context, payload) {
    context.state.contactNotificationList = await NotificationApiService.getContactNotificationAsync(
      payload.user.userId
    );
  },
  async [types.RESPONSE_INVITATION_CONTACT](context, payload) {
    try {
      if (payload.response === 'yes') {
        context.state.contactModel.senderEmail = payload.firstEmail;
        context.state.contactModel.recipientsEmail = payload.secondEmail;

        await NotificationApiService.setContactInvitationAsync(
          context.state.contactModel
        );
      } else {
        await ContactApiService.deleteContactAsync(payload.notificationId);
      }

      await context.dispatch(types.REFRESH_NOTIFICATION_CONTACT);
    } catch (error) {
      context.commit(types.ERROR_HAPPENED, error);
    }
  }
};
