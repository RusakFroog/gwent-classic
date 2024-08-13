import { createApp } from 'vue';
import gapiPlugin from 'vue3-googleapis';
import './style.css';
import App from './App.vue';
import router from './router/index.js';

const config = {
    apiKey: 'AIzaSyCyhrRIgxsumc1LVQ90G42rg0SEVT-cBg4',
    clientId: '1071941762255-lcfpsm1044ehpv01lksc9sp494q6debd.apps.googleusercontent.com',
    discoveryDocs: ['https://www.googleapis.com/discovery/v1/apis/drive/v3/rest'],
    scope: 'https://www.googleapis.com/auth/drive.metadata.readonly',
};

const app = createApp(App);

app.use(gapiPlugin, config);

app.use(router);

app.mount('#app');