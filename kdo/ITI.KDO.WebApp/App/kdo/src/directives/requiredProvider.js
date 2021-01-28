import $ from 'jquery';
import AuthService from '../services/AuthService';

export default {
  mounted(el, binding) {
    const providers = binding.value;

    if (!providers || !(providers instanceof Array))
      throw new Error('v-required-providers Expected Array value.');

    if (!AuthService.isBoundToProvider(providers)) {
      $(el).hide();
    }
  }
};
