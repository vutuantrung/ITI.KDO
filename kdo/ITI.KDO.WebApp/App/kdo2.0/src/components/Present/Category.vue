<template>
<div>
    <section>
        <div class="title">
            <h1>CATEGORIES</h1>
        </div>
    </section>

    <b-row >
        <b-col md="2">
        </b-col>

        <b-col md="5">
        <!--div class="feature box event" v-for="i of categoryPresentList">
       <div class="CatDiv">{{i.categoryName}}</div>
       <img :src="'i.img'">
        </div-->
            <div v-if="categoryPresentList[0] != null" class="feature-box event" style="background-image: url(http://www.languagetop.com/wp-content/uploads/2017/10/why_you_should_only_play_games_on_your_computer.png);">
                <div class="CatDiv">
                    <b-link href="presents/create/0" style="color: white;">{{categoryPresentList[1].categoryName}}</b-link>
                </div>
            <!--img :src="'http://www.languagetop.com/wp-content/uploads/2017/10/why_you_should_only_play_games_on_your_computer.png'" style="width:100%" class="img-thumbnail myImage" img-alt="Image" img-top/-->
            </div>
            <div class="feature-box event" style="background: url(http://bradyemmett.info/wp-content/uploads/2016/12/book-club-recomendations.jpg);">
                <div class="CatDiv">
                    <b-link href="presents/create/1" style="color: white;">{{categoryPresentList[2].categoryName}}</b-link>
                </div>
            </div>
            <div class="feature-box event" style="background:url(http://allpeoplefrom.com/wp-content/uploads/3.decoration-interieur.jpg);background-position: bottom;">
                <div class="CatDiv">
                    <b-link href="presents/create/2" style="color: white;">{{categoryPresentList[3].categoryName}}</b-link>
                </div>
            </div>  
            <div class="feature-box event" style="background:url(https://s3.amazonaws.com/static-asset/op-video-sync/assets/food_and_drink_masthead2.jpg);background-size: cover;">
                <div class="CatDiv">
                    <b-link href="presents/create/3" style="color: white;">{{categoryPresentList[4].categoryName}}</b-link>
                </div>
            </div>
        </b-col>

        <b-col md="5">
            <div class="feature-box event" style="background:url(http://www.thegoodshoppingguide.com/wp-content/uploads/2013/02/Health-and-Beauty_Sector_Featured_WEB-FRIENDLY.jpg);">
                <div class="CatDiv">
                    <b-link href="presents/create/0" style="color: white;">{{categoryPresentList[0].categoryName}}</b-link>            
                </div>
            </div>

            <div class="feature-box event" style="background:url(http://www.thegoodshoppingguide.com/wp-content/uploads/2013/02/Health-and-Beauty_Sector_Featured_WEB-FRIENDLY.jpg);">
                <div class="CatDiv">
                    <b-link href="presents/create/4" style="color: white;">{{categoryPresentList[5].categoryName}}</b-link>            
                </div>
            </div>

            <div class="feature-box event" style="background:url(http://cache.marieclaire.fr/data/photo/w1000_ci/4t/bigorexie-addiction-sport.jpg);background-size: cover;">
                <div class="CatDiv">
                    <b-link href="presents/create/5" style="color: white;">{{categoryPresentList[6].categoryName}}</b-link>
                </div>
            </div>

            <div class="feature-box event" style="background:url(https://thumbs.dreamstime.com/t/clothing-accessories-men-women-ready-travel-li-life-style-90671439.jpg);background-size: cover;">
                <div class="CatDiv">
                    <b-link href="presents/create/6" style="color: white;">{{categoryPresentList[7].categoryName}}</b-link>
                </div>
            </div>
        </b-col>
    </b-row>
</div>
</template>
<script>
 import { mapActions } from 'vuex';
    import PresentApiService from '../../services/PresentApiService';
    import CategoryPresentApiService from '../../services/CategoryPresentApiService';
    import UserApiService from '../../services/UserApiService';
    import FileApiService from '../../services/FileApiService';
    import AuthService from '../../services/AuthService';
    import Vue from 'vue';
    import Vuex from 'vuex';

  export default {
    data() {
      return {
        user:{},
        present:{},
        mode: null,
        id: null,
        errors: [],
        image: '',
        sendImage: '',
        categoryPresentList: [],
        categoryChosen: "Choose a category.",
        showDismissibleAlert: false,
        TypeOfFile : 1,
        data: new FormData()
      };
    },

    async mounted() {
        var userEmail = AuthService.emailUser();
        this.user = await UserApiService.getUserAsync(userEmail);

        this.categoryPresentList = await CategoryPresentApiService.getCategoryPresentListAsync();

        this.mode = this.$route.params.mode;
        this.id = this.$route.params.id;
    },

    methods: {
        ...mapActions(['executeAsyncRequest']),


        onFileChange(e) {      
            var files = e.target.files || e.dataTransfer.files;
            if (!files.length)
                return;
            this.createImage(files[0]);
            this.$router.replace('/presents/create');      
        },
        createImage(file) {
            var image = new Image();
            var reader = new FileReader();
            var vm = this;

            this.data.append('files', file);
            this.sendImage = file;
            
            reader.onload = (e) => {
                vm.image = e.target.result;
            };
            reader.readAsDataURL(file);
        },
        async removeImage(e) {
            e.preventDefault();
            this.image = '';
            this.item.photo = '';
            this.data.append('files',  this.item.photo);
            this.sendItemImage = await this.executeAsyncRequest(() => FileApiService.updateFileAsync(this.data, this.present.userId, 1));
            this.mode = undefined;
            this.$router.replace('/presents/create');           
        },
        
        choseCategory(categoryIdChosen, categoryNameChosen){
            this.categoryChosen = categoryNameChosen;
            this.present.categoryPresentId = categoryIdChosen;
        }
    }
  };
</script>
<style>
.CatDiv{
    position: relative;
    right:0;
    font-size: 35px;
    color:white;
    background-color: black;
    opacity: 0.8;
    font-variant: petite-caps;
    margin-top: 10%;
}

</style>