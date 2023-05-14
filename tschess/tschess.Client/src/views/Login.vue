<script setup>
import axios from 'axios';
import signalRService from '../services/SignalRService.js';
</script>


<template>
    <main>
        <!DOCTYPE html>
        <html>

        

        <body > 
            <div class ="center">
                <h1> Login </h1>
                 <div class = "amin"> 
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
                </div>
            </div>
            </div>
         </body>

        
            <div class="formRow">
                <button v-on:click="loginDevServer()">Login Dev</button>
            </div>

       

        
       

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
        async login() {
            //try {
            const userdata = (await axios.post('https://localhost:5001/api/users/login', this.loginModel)).data;
            axios.defaults.headers.common['Authorization'] = `Bearer ${userdata.token}`;
            console.log("userdata.token = " + userdata.token);
            this.token = userdata.token;
            console.log("isLoggedIn=true")
            this.isLoggedIn = true;

            console.log(userdata)
            console.log(userdata.username + " " + userdata.token)
            this.$store.commit("authenticate", userdata);
            console.log("this.$store.state.infos.token: " + this.$store.state.infos.token)

            //

            //catch (e) {
            //    console.log("login failed");
            //    this.isLoggedIn = false;
            //}

        },

        async loginDevServer() {
            await this.fetchDevUser();

            const userdata = (await axios.post('https://localhost:5001/api/users/login', this.devUser)).data;
            axios.defaults.headers.common['Authorization'] = `Bearer ${userdata.token}`;
            console.log("userdata.token = " + userdata.token);
            this.token = userdata.token;
            console.log("isLoggedIn=true")
            this.isLoggedIn = true;

            console.log(userdata)
            console.log(userdata.username + " " + userdata.token)
            this.$store.commit("authenticate", userdata);
            console.log("this.$store.state.infos.token: " + this.$store.state.infos.token)
        },

        async fetchDevUser() {
            await fetch('../../ad_secret.json')
                .then(response => response.json())
                .then(data => this.devUser = data);
            console.log("Dev User: " + this.devUser)
        },

        async joinQ() {
            console.log(this.token + "token");
            await signalRService.connectWithToken(this.token);
        },
    },
};
</script>


<style>
body{
    margin: 0;
    padding: 0;
    font-family: montserrat;
    background: linear-gradient(90deg, #464446, #2a2f2f);
    
     
}

.center{
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    width: 400px;
    background: white;
    border-radius: 10px;
}

.center h1{
    text-align: center;
    padding: 0 0 20px 0;
    border-bottom: 1px solid silver;
}
.amin{
    padding: 0 40px;
    box-sizing: border-box;
}
.txt_field{
    position: relative;
    border-bottom: 2px solid #adadad;
    margin: 30px 0;
}

.txt_field input{
    width: 100%;
    padding: 0 5px;
    height: 40px;
    font-size: 16px;
    border: none;
    background: none;
    outline: none;
}
.txt_field label{

    position: absolute;
    top: 50%;
    left: 5px;
    color: #adadad;
    transform: translateY(-50%);
    font-size: 16px;
    pointer-events: none;
    transition: 0.5s;
}
.txt_field span::before{
    content: "";
    position: absolute;
    top: 40px;
    left: 0;
    width: 100%;
    height: 2px;
    background: #21292e;
}

.txt_field input:focus ~ label
{
    top: -5px;
    color: #21292e;
}





.button-l button{
    width: 100%;
    height: 50px;
    border: 1px solid;
    background:#585a5c;
    border-radius: 25px;
    font-size: 18px;
    color: #e9f4fb;
    font-weight: 700;
    cursor: pointer;
    outline: none;
    margin: 30px 0;
} 

.button-l button:hover{
    border-color:#585a5c ;
    transition: 0.5s;
}


</style>
