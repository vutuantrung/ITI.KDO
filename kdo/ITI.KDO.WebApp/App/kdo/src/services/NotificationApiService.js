import {
  getAsync,
  postAsync,
  putAsync,
  deleteAsync
} from '../helpers/apiHelper';

const endpoint = '/api/notification';

class NotificationApiService {
  constructor() {}

  async setContactInvitationAsync(model) {
    return await postAsync(`${endpoint}/setContactInvitation`, model);
  }

  async getContactNotificationAsync(userId) {
    return await getAsync(`${endpoint}/${userId}/getContactNotification`);
  }

  async setEventInvitationAsync(model) {
    return await postAsync(`${endpoint}/setEventInvitation`, model);
  }

  async getEventNotificationAsync(userId) {
    return await getAsync(`${endpoint}/${userId}/getEventNotification`);
  }
}

export default new NotificationApiService();
