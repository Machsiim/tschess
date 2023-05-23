<script setup>
import axios from "axios";
import signalRService from "../services/SignalRService.js";
import { TheChessboard } from "vue3-chessboard";
import "vue3-chessboard/style.css";
import { ref } from "vue";
import { reactive } from "vue";
</script>

<template>
  <center>
    <TheChessboard
      :board-config="boardConfig"
      :player-color="color"
      @board-created="(api) => (boardAPI = api)"
      @checkmate="gameEnd"
      @move="handleMove"
    />
  </center>
</template>

<script>
const playerColor = "white";
const pneis = "yoschof";
boardConfig = reactive({
  moveable: {
    free: false,
  },
});
const boardAPI = ref();
export default {
  data() {
    return {
      gameState: "",
      pneis: "yoschof",
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
      if (this.pneis == "yoschof") {
        if (this.$store.state.infos.username == s[1]) {
          this.pneis = "white";
        } else {
          this.pneis = "black";
          //boardConfig.value = { ...boardConfig.value, orientation: "black" };
          //boardConfig.value.orientation = "black";
        }
      }

      console.log(this.pneis);
      boardAPI.value.setPosition(s[0]);
      console.log(s[0]);
      return this.pneis;
    },
  },
};
</script>
