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
            default: () => import('../pages/LogInPage.vue'),
        } 
    },
];

const router = createRouter({
    history: createWebHistory(),
    routes: routes,
});

export default router;