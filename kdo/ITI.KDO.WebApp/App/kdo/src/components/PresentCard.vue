<template lang="">
    <b-card 
        v-if="presentInfo !== null"
        :img-src="getPresentImage"
        img-alt="Image"
        img-top
        tag="article"
        class="mb-2"
        border-variant="dark"
        bg-variant="dark"
    >
        <h2 class="card-text">
            {{ presentInfo.presentName }}
        </h2>
    </b-card>
</template>
<script>
export default {
  props: ['presentInfo'],
  data() {
    return {};
  },
  methods: {
    getPresentImage() {
      return 'data:image/jpeg;base64,' + this.presentInfo.picture;
    },
  },
};
</script>
<style lang="">
</style>