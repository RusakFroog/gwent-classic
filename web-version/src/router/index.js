import { createRouter, createWebHistory } from 'vue-router';

const routes = [
    { 
        path: '/', 
        alias: '/home',
        components: 
        {
            default: () => import('../pages/HomePage.vue'),
            navigator: () => import('../components/navigator/Navigator.vue')
        }
    },
    { 
        path: '/deck', 
        components: 
        {
            default: () => import('../pages/DecksPage.vue'),
            navigator: () => import('../components/navigator/Navigator.vue')
        }
    },
    { 
        path: '/rules', 
        components: 
        {
            default: () => import('../pages/LogInPage.vue'),
            navigator: () => import('../components/navigator/Navigator.vue')
        }  
    },
    { 
        path: '/login',
        components:
        {
            default: () => import('../pages/LogInPage.vue'),
            navigator: () => import('../components/navigator/Navigator.vue')
        } 
    },
    { 
        path: '/register',
        components: 
        {
            default: () => import('../pages/SignUpPage.vue'),
        } 
    }

];

const router = createRouter({
    history: createWebHistory(),
    routes: routes,
});

export default router;