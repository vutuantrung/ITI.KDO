import 'babel-polyfill';

import VueI18n from 'vue-i18n';

import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import $ from 'jquery';
import Vuex from 'vuex';
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import store from './vuex/store';
import VueRouter from 'vue-router';

import App from './components/App.vue';
import Home from './components/Home.vue';
import Login from './components/Login.vue';
import Logout from './components/Logout.vue';


import UserProfile from './components/user/UserProfile.vue';
import UserProfileDisplay from './components/user/UserProfileDisplay.vue';

import Category from './components/present/Category.vue';
import PresentList from './components/present/PresentList.vue';
import PresentEdit from './components/present/PresentEdit.vue';

import EventList from './components/event/EventList.vue';
import EventEdit from './components/event/EventEdit.vue';
import EventView from './components/event/EventView.vue';
import EventPresentEdit from './components/event/EventPresentEdit.vue';
import EventParticipate from './components/event/EventParticipate.vue';
import EventImportPresent from './components/event/EventImportPresent.vue';

import Calendar from './components/event/calendar/Cal.vue';
import ContactEmail from './components/Contact/ContactEmail.vue';
import Contact from './components/Contact/Contact.vue';
import ContactList from './components/Contact/ContactList.vue';
import FacebookContactList from './components/Contact/FacebookContact.vue';

import NotificationUser from './components/Notification.vue';

import AppDefault from './components/AppDefault.vue';

import icons from 'glyphicons';

//import Register from './components/Register.vue';

//import User from './components/User/User.vue';
//import UserModificationMP from './components/User/UserEditPassword.vue';
//import QuiSommesNous from './components/QuiSommesNous.vue';

//import Simi from './components/Simulateur.vue';

import AuthService from './services/AuthService';

Vue.use(VueI18n);
Vue.use(VueRouter);
Vue.use(BootstrapVue);

import {messages} from 'vue-bootstrap4-calendar';

window.i18n = new VueI18n({
    locale: 'en',
    messages
});
/**
 * Filter for routes requiring an authenticated user
 * @param {*} to 
 * @param {*} from 
 * @param {*} next 
 */
function requireAuth(to, from, next) {
    console.log(AuthService.isConnected);
    if (!AuthService.isConnected) {
        next({
            path: '/appDefault',
            query: { redirect: to.fullPath }
        });
        return;
    }

    var requiredProviders = to.meta.requiredProviders;

    if (requiredProviders && !AuthService.isBoundToProvider(requiredProviders)) {
        next(false);
    }

    next();
}

/**
 * Declaration of the different routes of our application, and the corresponding components
 */
const router = new VueRouter({
    mode: 'history',
    base: '/Home',
    routes: [
        { path: '/login', component: Login },
        { path: '/logout', component: Logout, beforeEnter: requireAuth },

        { path: '/userProfile', component: UserProfile, beforeEnter: requireAuth },
        { path: '/userProfile/display/:email?', component: UserProfileDisplay, beforeEnter: requireAuth },

        { path: '/appDefault', component: AppDefault },

        { path: '/category', component: Category, beforeEnter: requireAuth },
        { path: '/presents', component: PresentList, beforeEnter: requireAuth },
        { path: '/presents/:mode([create|edit]+)/:id?', component: PresentEdit, beforeEnter: requireAuth },

        { path: '/events', component: EventList, beforeEnter: requireAuth },
        { path: '/events/:mode([create|edit]+)/:id?', component: EventEdit, beforeEnter: requireAuth },

        { path: '/events/:mode([create|view]+)/:id?', component: EventView, beforeEnter: requireAuth },
        { path: '/events/importPresent/:id?', component: EventImportPresent, beforeEnter: requireAuth },
        { path: '/events/presents/:mode([create|edit]+)/:eid?/:qid?', component: EventPresentEdit, beforeEnter: requireAuth },
        { path: '/events/participate/:eid?/:qid?', component: EventParticipate, beforeEnter: requireAuth },
        { path: '/events/calendar', component: Calendar, beforeEnter: requireAuth },


        { path: '/contactEmail', component: ContactEmail, beforeEnter: requireAuth },
        { path: '/contact', component: Contact, beforeEnter: requireAuth },
        { path: '/facebookContacts', component: FacebookContactList, beforeEnter: requireAuth },

        { path: '/notification/:id?', component: NotificationUser, beforeEnter: requireAuth },

        { path: '', component: App, beforeEnter: requireAuth },
    ]
})

/**
 * Configuration of the authentication service
 */

// Allowed urls to access the application (if your website is http://mywebsite.com, you have to add it)
AuthService.allowedOrigins = ['http://localhost:54822', /* 'http://mywebsite.com' */ ];

// Server-side endpoint to logout
AuthService.logoutEndpoint = '/Account/LogOff';

// Allowed providers to log in our application, and the corresponding server-side endpoints
AuthService.loginEndpoint = '/Account/Login';

// Allowed providers to sign up our application, and the corresponding server-side endpoints
AuthService.registerEndpoint = '/Account/Register';

//Registered the project into the bdd
AuthService.registerProjectEndpoint = '/Project/Register';


AuthService.modifyPasswordEndpoint = '/Account/ModifyPassword';

AuthService.emailTypes = {
    'FriendInvitation': {
        endpoint: '/Email/FriendInvitation'
    }
}

AuthService.providers = {
    'Base': {
        endpoint: '/Account/Login'
    },
    'Google': {
        endpoint: '/Account/ExternalLogin?provider=Google'
    },
    'Facebook': {
        endpoint: '/Account/ExternalLogin?provider=Facebook'
    },
};
AuthService.appRedirect = () => router.replace('/app');


// Creation of the root Vue of the application
new Vue({
    el: '#app',
    router,
    store,
    render: h => h(Home)
});