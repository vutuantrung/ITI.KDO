import {
  getAsync,
  postAsync,
  putAsync,
  deleteAsync
} from '../helpers/apiHelper';

const endpoint = '/api/participant';

class ParticipantApiService {
  constructor() {}

  async setEventInvitation(model) {
    return await postAsync(`${endpoint}/setInvitation`, model);
  }

  async getParticipant(userId, eventId) {
    return await getAsync(`${endpoint}/${userId}/${eventId}/getParticipant`);
  }

  async getParticipantListAsync(userId, eventId) {
    return await getAsync(`${endpoint}/${userId}/${eventId}/getByUserId`);
  }

  async getParticipantInvitedListAsync(userId, eventId) {
    return await getAsync(`${endpoint}/${userId}/${eventId}/getByEventId`);
  }

  async getParticipantAsync(userId) {
    return await getAsync(`${endpoint}/${userId}`);
  }

  async createParticipantAsync(model) {
    return await postAsync(endpoint, model);
  }

  async updateParticipantAsync(model) {
    return await putAsync(
      `${endpoint}/${model.userId}/${model.eventId}/update`,
      model
    );
  }

  async deleteParticipantAsync(userId, eventId) {
    return await deleteAsync(`${endpoint}/${userId}/${eventId}/delete`);
  }
}

export default new ParticipantApiService();
