<script setup>
import axios from 'axios';
import signalRService from '../services/SignalRService.js';
</script>


<template>
    <main>
        <html>

        <body>
            <div class="center-logout" v-if="$store.state.infos.isLoggedIn">
                <h1> {{ $store.state.infos.username }}</h1>

                <button v-on:click="logout()">Logout </button>
            </div>
            <div v-else class="center">
                <h1> Login </h1>
                <div class="amin">
                    <div class="txt_field">
                        <input type="text" name="username" v-model="loginModel.username" id="username">
                        <span></span>
                        <label for="username">Username</label>
                    </div>
                    <div class="txt_field">
                        <input id="password" name="password" v-model="loginModel.password" type="password" />
                        <span></span>
                        <label for="password">Password</label>
                    </div>

                    <div class="button-l">
                        <button v-on:click="login()">Login</button>
                        <p v-if="loginError" class="error-message">Login failed. Invalid credentials.</p>
                    </div>
                </div>
            </div>
        </body>

        <footer class="footer"></footer>

        </html>
    </main>
</template>

<script>
export default {
    data() {
        return {
            loginModel: {},
            isLoggedIn: false,
            token: "",
            devUser: {},
            loginError: false,
        };
    },
    computed: {
        currentImage() {
            return this.images[this.currentIndex];
        },
    },
    methods: {
        prev() {
            this.currentIndex =
                (this.currentIndex - 1 + this.images.length) % this.images.length;
        },
        next() {
            this.currentIndex = (this.currentIndex + 1) % this.images.length;
        },

        logout() {

            delete axios.defaults.headers.common['Authorization'];
            this.$store.commit('authenticate', null);



        },
        async login() {

            const host =
                process.env.NODE_ENV == "production"
                    ? ""
                    : "https://localhost:5001";

            try {
                const userdata = (await axios.post(host + '/api/users/login', this.loginModel)).data;
                axios.defaults.headers.common['Authorization'] = `Bearer ${userdata.token}`;
                this.$store.commit('authenticate', userdata);
                this.loginError = false;
            } catch (e) {
                if (e.response.status == 500) {

                    this.loginError = true;
                }
            }

        },



    },
    computed: {
        authenticated() {
            return this.$store.state.infos.isLoggedIn;
        },
        username() {
            return this.$store.state.infos.name;
        }
    },
};

</script>


<style>
body {
    margin: 0;
    padding: 0;
    background: linear-gradient(90deg, #464446, #2a2f2f);
}

.center {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    width: 400px;
    background: white;
    border-radius: 10px;
}

.center-logout {

    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    width: 400px;
    background: white;
    border-radius: 10px;
    color: #454648;

}

.center-logout button {
    width: 100%;
    height: 50px;
    border: 1px solid;
    background: #585a5c;
    border-radius: 25px;
    font-size: 18px;
    color: #e9f4fb;
    font-weight: 700;
    cursor: pointer;
    outline: none;
    margin: 30px 0;
}

.center-logout h1 {
    text-align: center;
    padding: 0 0 20px 0;
    border-bottom: 1px solid silver;
}

.footer {
    position: fixed;
    left: 0;
    bottom: 0;
    width: 100%;
    background-color: #222;
    color: white;
    text-align: center;
}

.center h1 {
    text-align: center;
    padding: 0 0 20px 0;
    border-bottom: 1px solid silver;
}

.amin {
    padding: 0 40px;
    box-sizing: border-box;
}

.txt_field {
    position: relative;
    border-bottom: 2px solid #adadad;
    margin: 30px 0;
}

.txt_field input {
    width: 100%;
    padding: 0 5px;
    height: 40px;
    font-size: 16px;
    border: none;
    background: none;
    outline: none;
}

.txt_field label {

    position: absolute;
    top: 50%;
    left: 5px;
    color: #adadad;
    transform: translateY(-50%);
    font-size: 16px;
    pointer-events: none;
    transition: 0.5s;
}

.txt_field span::before {
    content: "";
    position: absolute;
    top: 40px;
    left: 0;
    width: 100%;
    height: 2px;
    background: #21292e;
}

.txt_field input:focus~label {
    top: -5px;
    color: #21292e;
}

.txt_field input:valid~label {
    top: -5px;
    color: #21292e;
}






.button-l button {
    width: 100%;
    height: 50px;
    border: 1px solid;
    background: #585a5c;
    border-radius: 25px;
    font-size: 18px;
    color: #e9f4fb;
    font-weight: 700;
    cursor: pointer;
    outline: none;
    margin: 30px 0;
}

.button-l button:hover {
    border-color: #585a5c;
    transition: 0.5s;
}

.error-message {
    color: red;
    font-size: 14px;
    margin-top: 5px;
    text-align: center;
}
</style>