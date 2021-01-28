<template lang="">
    <b-card
        v-if="eventInfo !== null"
        :img-src="getEventImage"
        class="mb-2"
        border-variant="dark"
        bg-variant="dark"
        img-alt="Image"
        img-top
        tag="article"
    >
        <h2 class="card-text">
            {{ eventInfo.eventName }}
        </h2>
        <router-link :to="`events/view/${eventInfo.eventId}`">Go</router-link>
    </b-card>
</template>
<script>
export default {
  props: ['eventInfo'],
  data() {
    return {};
  },
  methods: {
    getEventImage() {
      return 'data:image/jpeg;base64,' + this.eventInfo.picture;
    },
  },
};
</script>
<style lang="">
</style>