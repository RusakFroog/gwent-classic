import { createRouter, createWebHistory } from 'vue-router';
import Navigator from '../components/Navigator.vue';

const routes = [
    { 
        path: '/', 
        alias: '/home',
        components: 
        {
            default: () => import('../pages/RegistrationPage.vue'),
            navigator: () => Navigator
        }
    },
    { 
        path: '/deck', 
        components: 
        {
            default: () => import('../pages/RegistrationPage.vue'),
            navigator: () => Navigator
        }
    },
    { 
        path: '/rules', 
        components: 
        {
            default: () => import('../pages/RegistrationPage.vue'),
            navigator: () => Navigator
        }  
    },
    { 
        path: '/login',
        components:
        {
            navigator: () => Navigator,
            default: () => import('../pages/LoginPage.vue'),
        } 
    },
    { 
        path: '/register',
        components: 
        {
            default: () => import('../pages/RegistrationPage.vue'),
        } 
    },
];

const router = createRouter({
    history: createWebHistory(),
    routes: routes,
});

export default router;