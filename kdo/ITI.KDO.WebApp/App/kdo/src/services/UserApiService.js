import { getAsync, postAsync, putAsync, deleteAsync, putFileAsync } from '../helpers/apiHelper';

const endpoint = "/api/user";

class UserApiServices {
    constructor() {

    }

    async getAllUserAsync() {
        return await getAsync(`${endpoint}`);
    }

    async getUserByIdAsync(userId) {
        return await getAsync(`${endpoint}/${userId}/search`);
    }

    async getUserAsync(emailUser) {
        return await getAsync(`${endpoint}/${emailUser}`);
    }

    async updateUserAsync(model) {
        return await putAsync(`${endpoint}/${model.userId}`, model);
    }
    
    async getUserId(emailUser){
        return await getAsync(`${endpoint}/${emailUser}`);
    }

    //async updateFileAnsync(data, userId) {
    //    console.log("updateFileAnsync");
    //    return await putFileAsync(`${endpoint}/img/${userId}`, data);
    //}
}

export default new UserApiServices();