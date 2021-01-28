import * as types from '../mutation-types';

export default {
  [types.ERROR_HAPPENED](state, { isLoading }) {
    state.isLoading = isLoading;
  },

  [types.SET_IS_LOADING](state, { error }) {
    state.errors.push(error || '');
  }
};
