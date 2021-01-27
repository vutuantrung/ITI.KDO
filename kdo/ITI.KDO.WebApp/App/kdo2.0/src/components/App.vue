<template>
<div>
<div class="bg"></div>
<header>
  <div class="content">
    <h1>Welcome to KDO</h1>
    <h3 style="color: rgb(38, 38, 38);">Your are connected, {{item.firstName}}</h3>
  </div>
</header>
<b-col md="12">
  <b-jumbotron class="bg-light">
    <h1>Your events</h1>
        <b-carousel id="friend_carousel"
                style="text-shadow: 1px 1px 2px #333; margin-top: 20px; margin-bottom: 20px;"
                controls
                indicators
                :interval="0"
                img-width="15%"
                img-height="4%"
                v-model="slide"
                @sliding-start="onSlideStart"
                @sliding-end="onSlideEnd">

          <b-carousel-slide v-for = "i in nbslide" :key="i" img-blank variant="dark" img-alt="Blank image">
            <b-row>
              <b-col v-for = "j in 3" :key="j" md="4" style="padding-left: 30px; padding-right: 30px;">
                <b-card v-if="eventList[(3 * (i - 1)) + j - 1] != null"
                      :img-src="'data:image/jpeg;base64,'+ eventList[(3 * (i - 1)) + j - 1].picture"
                      img-alt="Image"
                      img-top
                      tag="article"
                      class="mb-2"
                      border-variant="dark"
                      bg-variant="dark">
                  <h2 class="card-text">
                    {{eventList[(3 * (i - 1)) + j - 1].eventName}}
                  </h2>
                  <router-link :to="`events/view/${eventList[(3 * (i - 1)) + j - 1].eventId}`">Go</router-link>
                </b-card>
              </b-col>
            </b-row>
        </b-carousel-slide>
    </b-carousel>
  </b-jumbotron>
</b-col>

<b-col md="12">
  <b-jumbotron class="bg-light">
    <h1>Your presents</h1>
        <b-carousel id="friend_carousel"
                style="text-shadow: 1px 1px 2px #333; margin-top: 20px; margin-bottom: 20px;"
                controls
                indicators
                :interval="0"
                img-width="15%"
                img-height="4%"
                v-model="slide1"
                @sliding-start="onSlideStart1"
                @sliding-end="onSlideEnd1">

          <b-carousel-slide v-for="i in nbslide1" :key="i" img-blank variant="dark" img-alt="Blank image">
            <b-row>
              <b-col v-for = "j in 3" :key="j" md="4" style="padding-left: 30px; padding-right: 30px;">
                <b-card v-if="presentList[(3 * (i - 1)) + j - 1] != null"
                      :img-src="'data:image/jpeg;base64,'+ presentList[(3 * (i - 1)) + j - 1].picture"
                      img-alt="Image"
                      img-top
                      tag="article"
                      class="mb-2"
                      border-variant="dark"
                      bg-variant="dark">
                  <h2 class="card-text">
                    {{presentList[(3 * (i - 1)) + j - 1].presentName}}
                  </h2>
                </b-card>
              </b-col>
            </b-row>
        </b-carousel-slide>
    </b-carousel>
  </b-jumbotron>
  </b-col>
</div>
</template>

<script>
import { mapActions } from 'vuex';
import AuthService from "../services/AuthService";
import EventApiService from '../services/EventApiService';
import PresentApiService from '../services/PresentApiService';
import UserApiService from '../services/UserApiService';

export default {
    data () {
        return {
            slide: 0,
            sliding: null,
            slide1: 0,
            sliding1: null,
            item: {},
            userEmail: null,
            user: {},
            eventList: [],
            nbslide: 0,
            presentList: [],
            nbslide1:0,
            
        };
    },
     async mounted() {
        var userEmail = AuthService.emailUser();
        this.user = await UserApiService.getUserAsync(userEmail);
        this.item = await UserApiService.getUserAsync(userEmail);

        await this.refreshList();
    },
    methods: {
      onSlideStart (slide) {
        this.sliding = true
      },
      onSlideEnd (slide) {
        this.sliding = false
      },
      onSlideStart1 (slide1) {
        this.sliding = true
      },
      onSlideEnd1 (slide1) {
        this.sliding = false
      },


      ...mapActions(['executeAsyncRequestOrDefault', 'executeAsyncRequest']),

      async refreshList() {
            this.eventList = await EventApiService.getEventListAsync(this.user.userId);
            this.nbslide = Math.trunc((this.eventList.length + 2) / 3);
            this.presentList = await PresentApiService.getPresentListAsync(this.user.userId);
            this.nbslide1 = Math.trunc((this.presentList.length + 2) / 3);

      },
      async deleteEvent(eventId) {
          try {
              await EventApiService.deleteEventAsync(eventId);
              await this.refreshList();
          }
          catch(error) {
          }
      }
  }
  };
</script>

<style>
.img-fluid {
    background-color: #f8f9fa;
}

body {
  font-family: 'Roboto';
}

.layout {
  display: block;
  position: relative;
  background: #fff;
} 

.carousel-control-prev-icon:after
{
  content: '<';
  font-size: 60px;
  color: black;
}
.carousel-control-next-icon:after
{
  content: '>';
  font-size: 60px;
  color: black;
}
/* header */
header {
  height: 100vh;
  position: relative;
  overflow: hidden;
  background-size: cover;
  background: rgba(0, 0, 0, 0.7);
  background-image:url(http://ngn-mag.com/image/wallpaper/christmas-gift-wallpaper-9.jpg);
}
header .content {
  position: relative;
  display: block;
  color: black;
  top: 50%;
  left: 50%;
  text-align: center;
  transform: translate(-50%, -50%);
  text-shadow: 0px 0px 5px #000000;
}
header .content h1 {
  font-size: 3em;
  font-weight: normal;
}
header .content h3 {
  font-size: 1.5em;
  font-weight: lighter;
}
.carousel-control-prev-icon {
    background-color: black;
}
.bg {
  display: block;
  position: fixed;
  width: 100vw;
  height: 100vh;
  top: 0;
  left: 0;
  background-size: cover;
  -moz-transition: 0.1s;
  -o-transition: 0.1s;
  -webkit-transition: 0.1s;
  transition: 0.1s;
  z-index: -1;
}
</style>