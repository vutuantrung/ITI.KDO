import mutations from './mutations';
import getters from './getters';

export default {
  state() {
    return {
      isLoading: false,
      errors: []
    };
  },
  mutations,
  getters
};
