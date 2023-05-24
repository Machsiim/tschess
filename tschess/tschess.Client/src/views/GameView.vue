<script setup>
import axios from "axios";
import signalRService from "../services/SignalRService.js";
import { TheChessboard } from "vue3-chessboard";
import "vue3-chessboard/style.css";
import { ref, computed } from "vue";
import { reactive } from "vue";
</script>

<template>
  <center>
    <div v-if="playerColor === 'white' || playerColor === 'black'">
      <TheChessboard
        :board-config="computedBoardConfig"
        :player-color="playerColor"
        @board-created="(api) => (boardAPI = api)"
        @checkmate="gameEnd"
        @move="handleMove"
      />
    </div>
  </center>
</template>

<script>
const boardAPI = ref();

export default {
  data() {
    return {
      gameState: "",
      playerColor: null,
    };
  },
  computed: {
    computedBoardConfig() {
      const config = {
        orientation: this.playerColor,
        moveable: {
          free: false,
          //color: this.playerColor,
        },
      };
      console.log(config);
      return config;
    },
  },
  mounted() {
    signalRService.subscribeEvent("SetGameState", this.setGameState);
    signalRService.subscribeEvent("GameEnd", this.gameEnd);
    signalRService.getGameState(this.$store.state.infos.currentGameGuid);
    signalRService
      .getColor(
        this.$store.state.infos.currentGameGuid,
        this.$store.state.infos.username
      )
      .then((color) => {
        this.playerColor = color;
      });
  },
  /*unmounted() {
    signalRService.unsubscribeEvent("SetGameState", this.setGameState);
  }, */
  methods: {
    gameEnd(winner) {
      // Implement when Game is playable and logic is done

      console.log("Game ended, winner is " + winner);
    },

    handleMove(move) {
      console.log(move);
      let fen = move.after;
      signalRService.setGameState(this.$store.state.infos.currentGameGuid, fen);
    },

    setGameState(s) {
      console.log(this.playerColor);
      if (boardAPI.value) boardAPI.value.setPosition(s[0]);
      console.log(this.computedBoardConfig);
      // boardAPI.value.setPosition(s[0]);
      console.log(s[0]);
    },
  },
};
</script>
