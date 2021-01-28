import {
  getAsync,
  postAsync,
  putAsync,
  deleteAsync
} from '../helpers/apiHelper';

const endpoint = '/api/contact';

class ContactApiService {
  constructor() {}

  async getContactListAsync(userId) {
    return await getAsync(`${endpoint}/${userId}/getAll`);
  }

  async getFriendsAsync(userId) {
    return await getAsync(`${endpoint}/${userId}/getFriends`);
  }

  async getContactAsync(contactId) {
    return await getAsync(`${endpoint}/${contactId}`);
  }

  async createContactAsync(model) {
    return await postAsync(`${endpoint}/createContact`, model);
  }

  async deleteContactAsync(contactId) {
    return await deleteAsync(`${endpoint}/${contactId}`);
  }
}

export default new ContactApiService();
