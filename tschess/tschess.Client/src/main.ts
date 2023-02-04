import axios from "axios";
import process from 'node:process'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'


axios.defaults.baseURL = process.env.NODE_ENV == 'production' ? "/api" : "https://localhost:5001/api";

const app = createApp(App)

app.use(createPinia())
app.use(router)

app.mount('#app')
