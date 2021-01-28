import * as types from '../../mutation-types';

import NotificationApiService from '../../../services/NotificationApiService';

export default {
  async [types.REFRESH_NOTIFICATION_PRESENT](context, payload) {
    context.state.contactNotificationList = await NotificationApiService.getContactNotificationAsync(
      payload.user.userId
    );
  }
};
