<script setup>
import axios from "axios";
import signalRService from "../services/SignalRService.js";
import { TheChessboard } from "vue3-chessboard";
import "vue3-chessboard/style.css";
import { ref } from "vue";
</script>

<template>
  <TheChessboard
    :board-config="boardConfig"
    @board-created="(api) => (boardAPI = api)"
    @checkmate="gameEnd"
    @move="handleMove"
  />
</template>

<script>
const color = 2;
const boardConfig = {
  moveable: {
    free: false,
  },
};
const boardAPI = ref();
export default {
  data() {
    return {
      gameState: "",
    };
  },
  mounted() {
    signalRService.subscribeEvent("SetGameState", this.setGameState);
    signalRService.subscribeEvent("GameEnd", this.gameEnd);
    signalRService.getGameState(this.$store.state.infos.currentGameGuid);
  },
  unmounted() {
    signalRService.unsubscribeEvent("SetGameState", this.setGameState);
  },
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
      boardAPI.value.setPosition(s[0]);
      if (this.$store.state.infos.username == s[1]) {
        this.color = 0;
      } else this.color = 1;

      if (this.color == 1) {
        boardConfig.value = {
          ...boardConfig.value,
          movable: { color: "black" },
          orientation: "black",
        };
      }
    },
  },
};
</script>
