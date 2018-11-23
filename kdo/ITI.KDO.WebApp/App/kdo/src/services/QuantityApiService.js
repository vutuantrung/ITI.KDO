import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper';

const endpoint = "/api/quantity";

class QuantityApiService{
    constructor(){

    }

    async getQuantityListAsync(eventId){
        return await getAsync(`${endpoint}/${eventId}/getQuantityByEventId`);
    }

    async getQuantityAsync(quantityId){
        return await getAsync(`${endpoint}/${quantityId}`);
    }

    async createQuantityAsync(model){
        return await postAsync(endpoint, model);
    }

    async updateQuantityAsync(model){
        return await putAsync(`${endpoint}/${model.quantityId}`, model)
    }

    async deleteQuantityAsync(quantityId){
        return await deleteAsync(`${endpoint}/${quantityId}`);
    }

    async getQuantityPresentListAsync(userId, eventId){
        return await getAsync(`${endpoint}/${userId}/${eventId}`);
    }
}

export default new QuantityApiService()