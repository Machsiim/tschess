<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
</script>

<template>
  <header>
    <body>
  <ul>
    <li><RouterLink to="/" class="nav-link">Home</RouterLink></li>
    <li><RouterLink to="/about" class="nav-link">About</RouterLink></li>
    <li><RouterLink to="/" class="nav-link">Chess</RouterLink></li>
    <li><RouterLink to="/chat" class="nav-link">Chat (SignalR Demo)</RouterLink></li>
    <li><a href="#">About</a></li>
  </ul>
 </body>
  </header>
  <RouterView/>
</template>

<style >
body{

  background-color: rgb(54, 51, 51);
}
ul {
  list-style-type: none;
  margin: 0;
  padding: 0;
  overflow: hidden;
  background-color: rgb(54, 51, 51);
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
