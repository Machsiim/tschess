<script setup>
import axios from "axios";
import signalRService from "../services/SignalRService.js";
</script>

<template>
  <main>
    <!DOCTYPE html>
    <html>
    <section class="test">
      <div v-if="!joinedQueue">
        <button v-on:click="connect()" class="custom-button">
          Beitreten
        </button>
      </div>

      <br />

      <div v-if="joinedQueue && !isQueueEmpty">

        <ul>
          <h2>Connected Users:</h2>
          <div v-for="user in connectedUsers" v-bind:key="user">
            <ul v-if="user != $store.state.infos.username">
              {{
                user
              }}
              <button v-on:click="challenge(user)" class="custom-button">
                Challenge
              </button>
            </ul>
          </div>
          <br>
          <button v-on:click="leaveWaitingroom()" class="custom-button">
            Verlassen
          </button>
        </ul>

        <div v-if="joinedQueue && activeChallengesCheck()">
          <h2>Incoming Challenges:</h2>
          <div v-for="challenge in activeChallenges" v-bind:key="challenge">
            <ul>
              {{
                challenge
              }}
              <button v-on:click="processChallenge('accepted', challenge)" class="custom-button">
                Accept
              </button>
              <button v-on:click="processChallenge('declined', challenge)" class="custom-button">
                Decline
              </button>
            </ul>
          </div>
        </div>
      </div>

      <div v-if="joinedQueue && isQueueEmpty">
        <h2>Waiting for other players...</h2>
        <button v-on:click="leaveWaitingroom()" class="custom-button">
          Verlassen
        </button>
      </div>
    </section>

    </html>
  </main>
</template>

<script>
export default {
  data() {
    return {
      connectedUsers: [],
      isQueueEmpty: false,
      joinedQueue: false,
      activeChallenges: [],
    };
  },

  mounted() {
    const store = this.$store;
    if (store.state.infos.isInQueue) this.connect()
  },

  unmounted() {
    signalRService.leaveWaitingroom();
  },

  computed: {},
  methods: {
    async connect() {
      console.log("connect");
      await signalRService.connectWithToken(this.$store.state.infos.token);
      signalRService.subscribeEvent("SetWaitingroomState", this.addUser);
      signalRService.subscribeEvent("GetChallenges", this.printChallenges);
      signalRService.enterWaitingroom();
      signalRService.subscribeEvent("GameStarted", this.pushRouter);
      this.joinedQueue = true;
    },
    async enterWaitingroom() {
      signalRService.enterWaitingroom();
    },

    addUser(names) {
      this.connectedUsers = names;

      console.log(this.connectedUsers);
      if (this.connectedUsers.length == 1) {
        this.connectedUsers = ["Ziemlich leer hier..."];
        this.isQueueEmpty = true;
      }
      else {
        this.isQueueEmpty = false;
      }
    },

    challenge(username) {
      signalRService.subscribeEvent("GameStarted", this.pushRouter);
      signalRService.challenge(username);
    },

    printChallenges(challenges) {
      if (challenges[1] == this.$store.state.infos.username && !this.activeChallenges.includes(challenges[0])) {
        this.activeChallenges.push(challenges[0]);
      }
    },

    processChallenge(state, challenge) {
      if (state == "accepted") {
        const index = this.activeChallenges.indexOf(challenge);
        if (index > -1) {
          this.activeChallenges.splice(index, 1);
        }
        signalRService.startGame(challenge);
      } else {
        const index = this.activeChallenges.indexOf(challenge);
        if (index > -1) {
          this.activeChallenges.splice(index, 1);
        }
      }
    },

    async pushRouter(gameId) {
      this.$store.commit("joinGame", gameId);
      this.leaveWaitingroom();
      await this.$router.push("/game/");
    },

    leaveWaitingroom() {
      console.log("leaveWaitingroom");
      this.joinedQueue = false;
      signalRService.leaveWaitingroom();
    },
    activeChallengesCheck() {
      if (this.activeChallenges.length > 0) {
        return true;
      }
      else return false;
    },
  },
};
</script>

<style>
.custom-button {
  padding: 10px 20px;
  font-size: 16px;
  background-color: #8a2be2;
  /* Purple color */
  color: #fff;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.custom-button+.custom-button {
  margin-left: 10px;
}

.custom-button:hover {
  background-color: #6a1c9a;
  /* Darker shade of purple on hover */
}

header {
  color: white;
}

.test {
  text-align: center;
}

body {
  margin: 0;
  padding: 0;
  font-family: Arial, sans-serif;
}

section {
  /* Remove the float and width properties */
  float: none;
  width: auto;

  /* Add centering properties */
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  padding: 20px;
  color: white;
  margin-top: -90px !important;
}

aside {
  float: left;
  width: 30%;
  padding: 20px;
}

footer {
  background-color: #222;
  color: #fff;
  padding: 20px;
  text-align: center;
  font-size: 0.8em;
  clear: both;
}

.carousel {
  position: relative;
  width: 70%;
  height: 500px;
  overflow: hidden;
  margin-left: 15%;
  transition: transform 1s ease-in-out;
}

.carousel img {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
}

.carousel button {
  position: absolute;
  top: 50%;
  transform: translateY(-50%);
  font-size: 2rem;
  background-color: transparent;
  border: none;
  color: white;
  cursor: pointer;
}

.carousel button:first-child {
  left: 0;
}

.carousel button:last-child {
  right: 0;
}

.container {
  display: flex;
  justify-content: space-evenly;
  flex-wrap: wrap;
}

.book {
  position: relative;
  border-radius: 10px;
  width: 220px;
  height: 300px;
  background-color: whitesmoke;
  -webkit-box-shadow: 1px 1px 12px #000;
  box-shadow: 1px 1px 12px #000;
  -webkit-transform: preserve-3d;
  -ms-transform: preserve-3d;
  transform: preserve-3d;
  -webkit-perspective: 2000px;
  perspective: 2000px;
  display: -webkit-box;
  display: -ms-flexbox;
  display: flex;
  -webkit-box-align: center;
  -ms-flex-align: center;
  align-items: center;
  -webkit-box-pack: center;
  -ms-flex-pack: center;
  justify-content: center;
  color: #000;
}

.cover {
  top: 0;
  position: absolute;
  background-color: lightgray;
  width: 100%;
  height: 100%;
  border-radius: 10px;
  cursor: pointer;
  -webkit-transition: all 0.5s;
  transition: all 0.5s;
  -webkit-transform-origin: 0;
  -ms-transform-origin: 0;
  transform-origin: 0;
  -webkit-box-shadow: 1px 1px 12px #000;
  box-shadow: 1px 1px 12px #000;
  display: -webkit-box;
  display: -ms-flexbox;
  display: flex;
  -webkit-box-align: center;
  -ms-flex-align: center;
  align-items: center;
  -webkit-box-pack: center;
  -ms-flex-pack: center;
  justify-content: center;
}

.book:hover .cover {
  -webkit-transition: all 0.5s;
  transition: all 0.5s;
  -webkit-transform: rotatey(-80deg);
  -ms-transform: rotatey(-80deg);
  transform: rotatey(-80deg);
}

.test {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
}

.test>* {
  flex: 1;
  text-align: center;
}

p {
  font-size: 15px;
  font-weight: bolder;
}

img {
  height: 300px;
}
</style>
