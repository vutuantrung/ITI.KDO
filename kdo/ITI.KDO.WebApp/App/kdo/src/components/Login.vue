<template>
    <div>
        <div class="text-center" style="padding: 50px">
            <button type="button" @click="login('Google')" class="btn btn-lg btn-block btn-primary"><i class="fa fa-google" aria-hidden="true"></i> Se connecter via Google</button>

            <button type="button" @click="login('Facebook')" class="btn btn-lg btn-block btn-primary"><i class="fa fa-facebook-square" aria-hidden="true"></i> Se connecter via Facebook</button>

            <button type="button" @click="login('Base')" class="btn btn-lg btn-block btn-default">Se connecter via KDO</button>
        </div>
    </div>
</template>

<script>
import AuthService from "../services/AuthService";
import Vue from "vue";
import $ from "jquery";
import Vuex from 'vuex';

export default {
  data() {
    return {
      endpoint: null
    };
  },

  mounted() {
    AuthService.registerAuthenticatedCallback(() => this.onAuthenticated());
  },

  //Called right before a Vue instance is destroyed. At this stage the instance is still fully functional.
  beforeDestroy() {
    AuthService.removeAuthenticatedCallback(() => this.onAuthenticated());
  },

  methods: {
    login(provider) {
      AuthService.login(provider);
    },

    onAuthenticated() {
      this.$router.replace("/");
    }
  }
};
</script>

<style lang="less">
iframe {
  height: 600px;
}
</style>