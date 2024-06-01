import { createRouter, createWebHistory } from 'vue-router';

const routes = [
    { path: '/', alias: '/home', component: () => import('../pages/RegistrationPage.vue') },
    { path: '/deck', component: () => import('../pages/RegistrationPage.vue') },
    { path: '/rules', component: () => import('../pages/RegistrationPage.vue') },
    { path: '/login', component: () => import('../pages/RegistrationPage.vue') },
];

const router = createRouter({
    history: createWebHistory(),
    routes: routes,
});

export default router;