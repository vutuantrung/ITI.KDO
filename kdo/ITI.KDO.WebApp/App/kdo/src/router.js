import { createRouter, createWebHistory } from 'vue-router';

import AuthService from './services/AuthService';

const router = createRouter({
  history: createWebHistory(),
  routes: [{}]
});

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
