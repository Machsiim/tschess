<script setup lang="ts">
import { RouterLink, RouterView } from "vue-router";
</script>

<template>
  <header>
      <nav class="navbar ">
  
  <div class="collapse navbar-collapse" id="navbarNav">
    <ul class="navbar-nav">
      <li class="nav-item">
        <router-link class="nav-link" to="/">Home</router-link>
      </li>
      <li class="nav-item">
        <router-link class="nav-link" to="about">About</router-link>
      </li>
      <li class="nav-item">
        <router-link class="nav-link" to="game">Spiel</router-link>
      </li>
      <li class="nav-item">
        <router-link class="nav-link" to="login">Login</router-link>
      </li>
      <li class="nav-item">
        <router-link class="nav-link" to="chess">ChessBoard</router-link>
      </li>
    </ul>
  </div>
</nav>

    </header>
  <body>
    
   
  </body>


  

  <RouterView />
</template>

<script lang="ts">

export default {
  
  data() {
    return {};
  },
  methods: {
    showWaitingRoom() {
      if (
        this.$store.state.infos.isLoggedIn &&
        this.$store.state.infos.currentGameGuid == "offline"
      ) {
        return true;
      } else {
        return false;
      }
    },
  },
};
</script>

<style >

  body {
  margin: 0;
  padding: 0;
  font-family: "Poppins", sans-serif;
  background-image: linear-gradient(rgba(0, 0, 0, 1), rgba(0, 0, 0, 0.7)),
    url("./views/background.jpg");
  background-repeat: no-repeat;
  background-size: cover;
  box-shadow: 0 0 20px rgba(0, 0, 0, 0.5);
}

.navbar {
  top: 0;
  width: 100%;
  height: 60px;
}

.navbar ul {
  display: flex;
  list-style: none;
  width: 100%;
  justify-content: center;
}

.navbar ul li {
  margin: 0 1rem;
  padding: 1rem;
}

.navbar ul li a {
  text-decoration: none;
  text-transform: uppercase;
  color: #f4f4f4;
  z-index: 1;
}

.navbar a {
  font-size: 17px;
  background: transparent;
  border: none;
  padding: 1em 1.5em;
  color: #ffedd3;
  text-transform: uppercase;
  position: relative;
  transition: .5s ease;
  z-index: 1;
}

.navbar a::before {
  content: '';
  position: absolute;
  left: 0;
  bottom: 0;
  height: 2px;
  width: 0;
  background-color: #454545;
  transition: .5s ease;
  z-index: 1;
}

.navbar a:hover {
  color: #ffffff;
  transition-delay: .5s;
}

.navbar a:hover::before {
  width: 100%;
}

.navbar a::after {
  content: '';
  position: absolute;
  left: 0;
  bottom: 0;
  height: 0;
  width: 100%;
  background-color: #474747;
  transition: .4s ease;
  z-index: -1;
}

.navbar a:hover::after {
  height: 100%;
  transition-delay: 0.4s;
  color: aliceblue;
}




</style>
