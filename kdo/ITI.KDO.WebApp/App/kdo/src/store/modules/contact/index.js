import mutations from './mutations';
import getters from './getters';

export default {
  state() {
    return {
      contactNotificationList: []
    };
  },
  mutations,
  getters
};
