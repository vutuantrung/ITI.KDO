import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper';

const endpoint = "/api/participation";

class ParticipantApiService{
    constructor(){

    }

    async getParticipationByQuantityAsync(quantityId){
        return await getAsync(`${endpoint}/${quantityId}`);
    }

    async getParticipationAsync(quantityId, userId){
        return await getAsync(`${endpoint}/${quantityId}/${userId}/getParticipation`);
    }

    async existingParticipationAsync(quantityId, userId){
        return await getAsync(`${endpoint}/${quantityId}/${userId}/existingParticipation`);
    }

    async createParticipationAsync(model){
        return await postAsync(endpoint, model);
    }

    async updateParticipationAsync(model){
        return await putAsync(`${endpoint}/${model.quantityId}/${model.userId}`, model)
    }

    async deleteParticipationAsync(quantityId, userId){
        return await deleteAsync(`${endpoint}/${quantityId}/${userId}`);
    }
}

export default new ParticipantApiService()