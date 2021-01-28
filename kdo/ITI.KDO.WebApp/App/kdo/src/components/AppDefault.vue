<template>
  <div id="parallax-world-of-ugg">
    <section>
      <div class="title" style="margin-top: 50px">
        <h1>WELCOME TO KDO</h1>
      </div>
    </section>

    <section>
      <div class="parallax-one">
        <h2>KDO</h2>
      </div>
    </section>

    <div class="row" style="margin-top: 1.3%">
      <div class="col-sm-6 col-md-3">
        <div class="col-md-12 feature-box">
          <span class="glyphicon glyphicon-cog icon"></span>
          <h4>Create your Event</h4>
          <p>Birthday, Christmas, New Year's eve, etc...</p>
        </div>
      </div>
      <!-- End Col -->

      <div class="col-sm-6 col-md-3">
        <div class="col-md-12 feature-box">
          <span class="glyphicon glyphicon-cog icon"></span>
          <h4>Create your presents list</h4>
          <p>Specify your wished presents and details pertaining to them</p>
        </div>
      </div>
      <!-- End Col -->

      <div class="col-sm-6 col-md-3">
        <div class="feature-box">
          <span class="glyphicon glyphicon-cog icon"></span>
          <h4>Invite your friends</h4>
          <p>Ask your friends for their availability, decide the date</p>
        </div>
      </div>
      <!-- End Col -->

      <div class="col-sm-6 col-md-3">
        <div class="col-md-12 feature-box">
          <span class="glyphicon glyphicon-cog icon"></span>
          <h4>Receive your presents</h4>
          <p>All your friends will contribute to buy you presents</p>
          <p>JUST ENJOY</p>
        </div>
      </div>
      <!-- End Col -->
    </div>

    <section>
      <div class="parallax-two">
        <h2>CHRISTMAS</h2>
      </div>
    </section>

    <section>
      <div class="block">
        <p><span class="first-character ny"></span></p>
      </div>
    </section>

    <section>
      <div class="parallax-three">
        <h2>BIRTHDAY</h2>
      </div>
    </section>

    <section>
      <div class="block">
        <p><span class="first-character atw"></span></p>
        <p class="line-break margin-top-10"></p>
        <p class="margin-top-10"></p>
      </div>
    </section>
  </div>
</template>
<script>
import $ from 'jquery';

// $(window).scroll(function () {
//   parallax();
// });

// function parallax() {
//   var scrolled = $(window).scrollTop();
//   $('.bg').css('top', -(scrolled * 0.1) + 'px');
//   $('header .content').css('top', 50 + scrolled * 0.1 + '%');
//   $('header .content').css('opacity', 1 - (scrolled * 0.01) / 10);
//   $('header .content').css('opacity', 1 - (scrolled * 0.01) / 10);
// }
export default {
  methods: {
    parallax() {
      var scrolled = $(window).scrollTop();
      $('.bg').css('top', -(scrolled * 0.1) + 'px');
      $('header .content').css('top', 50 + scrolled * 0.1 + '%');
      $('header .content').css('opacity', 1 - (scrolled * 0.01) / 10);
      $('header .content').css('opacity', 1 - (scrolled * 0.01) / 10);
    },
  },
};
</script>
<style>
@import url(https://fonts.googleapis.com/css?family=Oswald:300,400,700);
@import url(https://fonts.googleapis.com/css?family=Source+Sans+Pro:200,300,400,600,700,900,200italic,300italic,400italic,600italic,700italic,900italic);

/* Override UGG site */
#main {
  width: 100%;
  padding: 0;
}
.content-asset p {
  margin: 0 auto;
}
.breadcrumb {
  display: none;
}

/* Helpers */
/**************************/
.margin-top-10 {
  padding-top: 10px;
}
.margin-bot-10 {
  padding-bottom: 10px;
}

/* Typography */
/**************************/
#parallax-world-of-ugg h1 {
  font-family: 'Oswald', sans-serif;
  font-size: 24px;
  font-weight: 400;
  text-transform: uppercase;
  color: black;
  padding: 0;
  margin: 0;
}
#parallax-world-of-ugg h2 {
  font-family: 'Oswald', sans-serif;
  font-size: 70px;
  letter-spacing: 10px;
  text-align: center;
  color: white;
  font-weight: 400;
  text-transform: uppercase;
  z-index: 10;
  opacity: 0.9;
}
#parallax-world-of-ugg h3 {
  font-family: 'Oswald', sans-serif;
  font-size: 14px;
  line-height: 0;
  font-weight: 400;
  letter-spacing: 8px;
  text-transform: uppercase;
  color: black;
}
#parallax-world-of-ugg p {
  font-family: 'Source Sans Pro', sans-serif;
  font-weight: 400;
  font-size: 14px;
  line-height: 24px;
}
.first-character {
  font-weight: 400;
  float: left;
  font-size: 84px;
  line-height: 64px;
  padding-top: 4px;
  padding-right: 8px;
  padding-left: 3px;
  font-family: 'Source Sans Pro', sans-serif;
}

.sc {
  color: #3b8595;
}
.ny {
  color: #3d3c3a;
}
.atw {
  color: #c48660;
}

/* Section - Title */
/**************************/
#parallax-world-of-ugg .title {
  background: white;
  padding: 60px;
  margin: 0 auto;
  text-align: center;
}
#parallax-world-of-ugg .title h1 {
  font-size: 35px;
  letter-spacing: 8px;
}

/* Section - Block */
/**************************/
#parallax-world-of-ugg .block {
  background: white;
  padding: 60px;
  width: 820px;
  margin: 0 auto;
  text-align: justify;
}
#parallax-world-of-ugg .block-gray {
  background: #f2f2f2;
  padding: 60px;
}
#parallax-world-of-ugg .section-overlay-mask {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: black;
  opacity: 0.7;
}

/* Section - Parallax */
/**************************/
#parallax-world-of-ugg .parallax-one {
  padding-top: 200px;
  padding-bottom: 200px;
  overflow: hidden;
  position: relative;
  width: 100%;
  background-image: url(https://hdwallsource.com/img/2014/10/gift-pictures-40094-41030-hd-wallpapers.jpg);
  background-attachment: fixed;
  background-size: cover;
  -moz-background-size: cover;
  -webkit-background-size: cover;
  background-repeat: no-repeat;
  background-position: top center;
}
#parallax-world-of-ugg .parallax-two {
  padding-top: 200px;
  padding-bottom: 200px;
  overflow: hidden;
  position: relative;
  width: 100%;
  background-image: url(https://www.boredart.com/wp-content/uploads/2015/10/animated-christmas-wallpaper-35.jpg);
  background-attachment: fixed;
  background-size: cover;
  -moz-background-size: cover;
  -webkit-background-size: cover;
  background-repeat: no-repeat;
  background-position: center center;
}
#parallax-world-of-ugg .parallax-three {
  padding-top: 200px;
  padding-bottom: 200px;
  overflow: hidden;
  position: relative;
  width: 100%;
  background-image: url(http://www.elegantwallpapers.com/wp-content/wallpapers/800x480/00814_800x480.jpg?87a0fa);
  background-attachment: fixed;
  background-size: cover;
  -moz-background-size: cover;
  -webkit-background-size: cover;
  background-repeat: no-repeat;
  background-position: center center;
}

/* Extras */
/**************************/
#parallax-world-of-ugg .line-break {
  border-bottom: 1px solid black;
  width: 150px;
  margin: 0 auto;
}

/* Media Queries */
/**************************/
@media screen and (max-width: 959px) and (min-width: 768px) {
  #parallax-world-of-ugg .block {
    padding: 40px;
    width: 620px;
  }
}
@media screen and (max-width: 767px) {
  #parallax-world-of-ugg .block {
    padding: 30px;
    width: 420px;
  }
  #parallax-world-of-ugg h2 {
    font-size: 30px;
  }
  #parallax-world-of-ugg .block {
    padding: 30px;
  }
  #parallax-world-of-ugg .parallax-one,
  #parallax-world-of-ugg .parallax-two,
  #parallax-world-of-ugg .parallax-three {
    padding-top: 100px;
    padding-bottom: 100px;
  }
}
@media screen and (max-width: 479px) {
  #parallax-world-of-ugg .block {
    padding: 30px 15px;
    width: 290px;
  }
}
.feature-box {
  text-align: center;
  padding: 20px;
  transition: 0.5s;
  margin-bottom: 30px;
  border: 1px solid #e8e8e8;
}
.feature-box:hover {
  box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
}
.feature-box h4 {
  font-size: 20px;
  font-weight: 600;
  margin: 25px 0 15px;
}

/* Site Heading */
.site-heading h3 {
  font-size: 40px;
  margin-bottom: 10px;
  font-weight: 600;
}
</style>