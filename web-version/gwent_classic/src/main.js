import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
import router from './router/index.js';
import store from './store/Store.js';

store.dispatch('loadVolume');

createApp(App)
    .use(store)
    .use(router)
    .mount('#app')