<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
</script>

<template>
  <header>

    
      <ul>
        <li>
          <RouterLink to="/" class="nav-link">Home</RouterLink>
        </li>
        <li>
          <RouterLink to="/about" class="nav-link">About</RouterLink>
        </li>
        <li>
          <div v-if="!this.$store.state.infos.isLoggedIn">
            <RouterLink to="/login" class="nav-link">Login</RouterLink>
          </div>
        </li>
        <li>
          <div v-if="this.showWaitingRoom()">
            <RouterLink to="/enter" class="nav-link">Enter Waitingroom</RouterLink>
          </div>
        </li>
        <li>
          <div v-if="this.$store.state.infos.currentGameGuid != 'offline'">
            <RouterLink to="/game" class="nav-link">Game</RouterLink>
          </div>

        </li>

      </ul>
    
  </header>
   <body>
    
   </body>
   <footer>


   </footer>

  <RouterView />
</template>

<script lang="ts">
export default {
  data() {
    return {

    };
  },
  methods: {
    showWaitingRoom() {
      if (this.$store.state.infos.isLoggedIn && this.$store.state.infos.currentGameGuid == "offline") {
        return true;
      }
      else {
        return false;
      }
    }
  },

}
</script>

<style >
body {

  background-color: rgb(54, 51, 51);
}

ul {
  list-style-type: none;
  margin: 0;
  padding: 0;
  overflow: hidden;
  background-color: #222;
}

li {
  float: left;
}

li a {
  display: block;
  color: white;
  text-align: center;
  padding: 14px 16px;
  text-decoration: none;
}

/* Change the link color to #111 (black) on hover */
li a:hover {
  background-color: #111;
}
</style>
