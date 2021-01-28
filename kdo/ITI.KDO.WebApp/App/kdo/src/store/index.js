import { createStore } from 'vuex';

import mutations from './mutations';
import getters from './getters';
import actions from './actions';

import app from './modules/app/index';

const store = createStore({
  modules: {
    app
  },
  state() {
    return {};
  },
  mutations,
  getters,
  actions
});

export default store;
