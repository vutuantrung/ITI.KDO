import { createStore } from 'vuex';

import mutations from './mutations';
import getters from './getters';
import actions from './actions';

import app from './modules/app/index';
import contact from './modules/contact/index';
import event from './modules/event/index';
import present from './modules/present/index';

const store = createStore({
  modules: {
    app,
    contact,
    event,
    present
  },
  state() {
    return {};
  },
  mutations,
  getters,
  actions
});

export default store;
