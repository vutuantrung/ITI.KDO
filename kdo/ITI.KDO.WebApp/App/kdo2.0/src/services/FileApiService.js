import { postFileAsync, postTypeAsync, deleteAsync } from '../helpers/apiHelper';

const endpoint = "/api/file";

class FileApiService {
    constructor() {

    }

    async updateFileAsync(data, userId) {
        console.log("updateFileAsync");
        return await postFileAsync(`${endpoint}/img/${userId}`, data);
    }

    async typeOfPicture(type, userId) {
        console.log("postTypeAsync");
        return await postTypeAsync(`${endpoint}/${type}/${userId}`);
    }

    async deletePicture(type, userId) {
        console.log("postTypeAsync");
        return await deleteAsync(`${endpoint}/${type}/${userId}`);
    }
}

export default new FileApiService();