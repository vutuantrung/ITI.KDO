import {
  getAsync,
  postAsync,
  putAsync,
  deleteAsync
} from '../helpers/apiHelper';

const endpoint = '/api/event';

class EventApiService {
  constructor() {}

  async getEventListAsync(userId) {
    return await getAsync(`${endpoint}/${userId}/getEventList`);
  }

  async getEventSuggestListAsync(userId, eventId) {
    return await getAsync(
      `${endpoint}/${eventId}/${userId}/getEventSuggestList`
    );
  }

  async getEventSuggestListByUserIdAsync(userId) {
    return await getAsync(`${endpoint}/${userId}/getEventSuggestListByUserId`);
  }

  async getEventByIdAsync(eventId) {
    return await getAsync(`${endpoint}/${eventId}`);
  }

  async getEventByIdsAsync(userId, eventId) {
    return await getAsync(`${endpoint}/${eventId}/${userId}/getEvent`);
  }

  async getEventSuggestAsync(userId, eventId) {
    return await getAsync(`${endpoint}/${eventId}/${userId}/getEventSuggest`);
  }

  async createEventAsync(model) {
    return await postAsync(`${endpoint}/createEvent`, model);
  }

  async createEventSuggestAsync(model) {
    return await postAsync(`${endpoint}/createEventSuggest`, model);
  }

  async updateEventAsync(model) {
    return await putAsync(`${endpoint}/${model.eventId}`, model);
  }

  async deleteEventAsync(eventId) {
    return await deleteAsync(`${endpoint}/${eventId}`);
  }
}

export default new EventApiService();
