<script setup>
import axios from 'axios';
import signalRService from '../services/SignalRService.js';
</script>
<template>
    <div class="chatView">
        <div class="info">Melde dich mit deinen Schul-Zugangsdaten an. Danach wird mit dem
            Token eine SignalR Verbindung aufgebaut. Hast du in der Datei appsettings.Development.json
            deinen User als Suchuser hinterlegt, dann kannst du auch andere User mit einem beliebigen
            Passwort probieren.
            Wenn du diese Seite in einem 2. Tab aufmachst, dann siehst du wie die Seiten sich
            die Nachrichten senden.
        </div>
        <div class="loginForm">
            <div class="formRow">
                <label for="username">Username</label>
                <input id="username" name="username" v-model="loginModel.username" type="text" />
            </div>
            <div class="formRow">
                <label for="password">Password</label>
                <input id="password" name="password" v-model="loginModel.password" type="password" />
            </div>
            <div class="formRow">
                <button v-on:click="login()">Login</button>
            </div>
        </div>
        <div v-if="connected" class="messageForm">
            <textarea id="message" type="text" v-model="message" />
            <button v-on:click="sendMessage()">Send message</button>
        </div>
        <div class="log">
            <div v-for="m in messages" v-bind:key="m">{{ m }}</div>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            loginModel: {},
            message: '',
            messages: [],
            signalRConnection: null,
            connected: false,
        };
    },



    methods: {
        async login() {
            try {
                // Send login request to /api/user/login. The baseUrl of axios is configured
                // in main.ts.
                const userdata = (await axios.post('https://localhost:5001/api/users/login', this.loginModel)).data;
                axios.defaults.headers.common['Authorization'] = `Bearer ${userdata.token}`;

                // Try to connect to SignalR with our SignalR Service in
                // ../services/SignalRService.js
                await signalRService.connectWithToken(userdata.token);
                this.connected = true;
                // ReceiveMessage is corresponding to the first parameter in SendAsync
                // in the c# hub class. We can register a method in our vue.js template
                // to process this message.
                signalRService.subscribeEvent('ReceiveMessage', this.onReceiveMessage);
                console.log("Vor Commit")
                this.$store.commit("authenticate", userdata);
                console.log("this.$store.state.infos.token: " + this.$store.state.infos.token);
            } catch (e) {
                this.messages.push(`Login failed: ${JSON.stringify(e)}`);
            }
        },
        async sendMessage() {
            signalRService.sendMessage(this.message);
        },
        onReceiveMessage(message) {
            this.messages.push(`Received from SignalR: ${message}`);
        },
    },
    unmounted() {
        signalRService.unsubscribeEvent('ReceiveMessage', this.onReceiveMessage);
    },

};
</script>

<style scoped>
.chatView {
    max-width: 50em;
    margin: auto;
    color: white;
    display: flex;
    flex-direction: column;
    gap: 1em;
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    line-height: 1.4em;
}

.loginForm {
    display: flex;
    flex-direction: column;
    gap: 0.5rem;
}

.formRow {
    display: flex;
    gap: 1rem;
}

.formRow label {
    flex: 0 0 5em;
}

.formRow input {
    flex-grow: 0.5;
}

.messageForm {
    display: flex;
    gap: 0.5rem;
}

.messageForm textarea {
    flex-grow: 1;
}

.log {
    word-wrap: break-word;
    font-family: monospace;
    line-height: 1em;
}
</style>