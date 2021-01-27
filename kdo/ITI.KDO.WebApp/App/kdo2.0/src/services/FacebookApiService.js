import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper'

const endpoint = "/api/facebook";

class FacebookApiService {
    constructor() {

    }

    async getFacebookFriendsAsync() {
        return await getAsync(`${endpoint}/getFriends`);
    }
}

export default new FacebookApiService()