<script setup>
import { RouterLink, RouterView } from "vue-router";
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
        <div v-if="!$store.state.infos.isLoggedIn">
          <RouterLink to="/login" class="nav-link">Login</RouterLink>
        </div>
      </li>

      <li v-if="$store.state.infos.isLoggedIn">
        <RouterLink to="/enter" class="nav-link">Enter Waitingroom</RouterLink>
      </li>

      <li v-if="showGame()">
        <RouterLink to="/game" class="nav-link">Game</RouterLink>
      </li>

      <li v-if="$store.state.infos.isLoggedIn">
        
        <RouterLink to="/login" class="nav-link">{{ $store.state.infos.username }}</RouterLink>
      </li>
    </ul>
  </header>

  <RouterView />

  <footer class="footer">
    <p>&copy; InParis </p>
    <p>2023 Tschess </p>
  </footer>
</template>

<script>
export default {
  data() {
    return {};
  },
  methods: {
    showGame() {
      if (this.$store.state.infos.currentGameGuid == "offline") return false;
      else return true;
    }
  },
};
</script>

<style>
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
