import mutations from './mutations';
import getters from './getters';

export default {
  state() {
    return {
      eventNotificationList: []
    };
  },
  mutations,
  getters
};
