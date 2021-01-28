import { createRouter, createWebHistory } from 'vue-router';

import AuthService from './services/AuthService';

const router = createRouter({
  history: createWebHistory(),
  routes: [{}]
});

/**
 * Configuration of the authentication service
 */

// Allowed urls to access the application (if your website is http://mywebsite.com, you have to add it)
AuthService.allowedOrigins = [
  'http://localhost:54822' /* 'http://mywebsite.com' */
];

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
  FriendInvitation: {
    endpoint: '/Email/FriendInvitation'
  }
};

AuthService.providers = {
  Base: {
    endpoint: '/Account/Login'
  },
  Google: {
    endpoint: '/Account/ExternalLogin?provider=Google'
  },
  Facebook: {
    endpoint: '/Account/ExternalLogin?provider=Facebook'
  }
};
AuthService.appRedirect = () => router.replace('/app');

function requireAuth(to, from, next) {
  if (!AuthService.isConnected) {
    next({
      path: '/login',
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
