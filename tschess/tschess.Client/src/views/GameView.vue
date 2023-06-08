<script setup>
import axios from "axios";
import signalRService from "../services/SignalRService.js";
import { TheChessboard } from "vue3-chessboard";
import "vue3-chessboard/style.css";
import { ref, computed } from "vue";
import { reactive } from "vue";

const errorHandler = (event) => {
  const error = event.error;
  if (error && error.message.startsWith("Invalid move: {")) {
    // Handle the specific invalid move error
    console.log("Invalid move:", error);
    // You can access the boardAPI here and perform any necessary actions
    if (boardAPI) {
      // Access the boardAPI methods or properties
      //console.log(boardAPI.getFen());
      //console.log(boardAPI.getLastMove().san);
      //boardAPI.value.undoLastMove();
      // Perform other actions with boardAPI if needed
    }
    // You can display an error message to the user or perform any other necessary actions
  } else {
    // Handle other errors
    console.error("An error occurred:", error);
  }
};
window.addEventListener("error", errorHandler);
</script>

<template>
  <div class="game-view">
    <div class="flex-container">
      <div v-if="playerColor" class="chessboard-container">
        <TheChessboard :board-config="computedBoardConfig" :player-color="playerColor"
          @board-created="(api) => (boardAPI = api)" @checkmate="gameEnd" @move="handleMove" @check="handleCheck" />
      </div>

      <div class="resign-container">
        <div>
          <h1>{{ $store.state.infos.username }}</h1>
          <h1>gegen</h1>
          <h1>{{ opponent }}</h1>
        </div>

        <button class="resign-button" @click="confirmResign">Resign</button>
      </div>
    </div>

    <div v-if="showPopup" class="popup">
      <div class="popup-content">
        <div class="popup-title">{{ winner }} wins!</div>
        <span class="popup-close" @click="closePopup">&#x2715;</span>
      </div>
    </div>
    <div v-if="showPopupResign" class="popup">
      <div class="popup-content">
        <div class="popup-title">{{ winner }} wins through resignation!</div>
        <span class="popup-close" @click="closePopup">&#x2715;</span>
      </div>
    </div>
  </div>
</template>

<script>
const boardAPI = ref();
var currentFen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
export default {
  data() {
    return {
      gameState: "",
      playerColor: "",
      showPopup: false,
      showPopupResign: false,
      winner: "",
      opponent: "",
    };
  },

  computed: {
    computedBoardConfig() {
      return {
        orientation: this.playerColor,
        movable: {
          free: false,

          // color: this.playerColor,
        },
      };
    },
  },

  mounted() {
    signalRService.subscribeEvent("SetGameState", this.setGameState);
    signalRService.subscribeEvent("GameEnd", this.EndGame);
    signalRService.subscribeEvent("Resign", this.EndGameResign);
    signalRService.getGameState(this.$store.state.infos.currentGameGuid);
    signalRService.getOpponent(this.$store.state.infos.currentGameGuid, this.$store.state.infos.username).then((opponent) => {
      this.opponent = opponent;
    });

    signalRService
      .getColor(
        this.$store.state.infos.currentGameGuid,
        this.$store.state.infos.username
      )
      .then((color) => {
        this.playerColor = color;
      });

    window.onunhandledrejection = function (event) {
      // Handle the unhandled promise rejection here
      console.error("Unhandled promise rejection:", event.reason);

      // Check if the rejection reason is an invalid move error
      if (
        event.reason instanceof Error &&
        event.reason.message.startsWith("Invalid move")
      ) {
        // Access the board API and undo the last move
        if (boardAPI.value) {
          console.log(this.currentFen);
          boardAPI.value.setPosition(currentFen);
        }
      }
    };
  },

  unmounted() {
    signalRService.unsubscribeEvent("SetGameState", this.setGameState);
    signalRService.unsubscribeEvent("GameEnd", this.EndGame);
    signalRService.Resign(this.$store.state.infos.currentGameGuid, this.$store.state.infos.username);
  },

  methods: {
    handleCheck(color) {
      signalRService.setGameState(
        this.$store.state.infos.currentGameGuid,
        boardAPI.value.getFen(),
        boardAPI.value.getLastMove().san
      );
      currentFen = boardAPI.value.getFen();
    },
    gameEnd(isMated) {
      // Implement when Game is playable and logic is done
      currentFen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
      let winner = isMated === "white" ? "black" : "white";
      console.log(boardAPI.value.getFen());
      //console.log(boardAPI.value.getLastMove().san);
      signalRService.setGameState(
        this.$store.state.infos.currentGameGuid,
        boardAPI.value.getFen(),
        " "
      );
      signalRService.EndGame(
        this.$store.state.infos.currentGameGuid,
        winner,
        boardAPI.value.getFen()
      );
    },
    EndGame(winner, fen) {
      this.winner = winner;
      this.showPopup = true;
      this.$store.commit("joinQueue");
      this.signalRService.unsubscribeEvent("Resign");
    },

    EndGameResign(winner, fen) {
      this.winner = winner;
      this.showPopupResign = true;
      this.$store.commit("joinQueue");
      this.signalRService.unsubscribeEvent("Resign");
    },

    handleMove(move) {
      console.log(move);
      let fen = move.after;
      signalRService.setGameState(
        this.$store.state.infos.currentGameGuid,
        fen,
        move.san
      );
    },
    confirmResign() {
      const confirmed = confirm("Are you sure you want to resign?");
      console.log("confirmed value:", confirmed);
      if (confirmed) {
        const winner = this.playerColor;
        this.gameEnd(winner);
      }
    },

    setGameState(s) {
      console.log(this.playerColor);
      console.log(s);
      currentFen = s[0];
      //fen = s[0];
      if (boardAPI.value)
        try {
          boardAPI.value.setPosition(s[0]);
        } catch (e) {
          console.log(e);
          if (e.message.startsWith("Invalid move")) {
            //   boardAPI.value.setPosition(s[0]);
          }
          // boardAPI.value.move(this.lastMove);
        }
      console.log(this.computedBoardConfig);
      // boardAPI.value.setPosition(s[0]);
      console.log(s[0]);
    },
    closePopup() {
      this.showPopup = false;
      this.$store.commit("setGameGuid", "offline");
      this.$router.push("/enter");
    },
    setOpponent(opponent) {
      this.opponent = opponent;
    },
  },
};
</script>

<style>
.footer {
  display: none;
}

.popup {
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  background-color: #ffffff;
  border: 2px solid #000000;
  border-radius: 5px;
  padding: 20px;
  z-index: 9999;
  display: block;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
  font-family: Arial, sans-serif;
}

.popup-content {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.popup-title {
  font-size: 24px;
  font-weight: bold;
  margin-bottom: 10px;
  color: rgb(75, 75, 75);
}

.popup-close {
  cursor: pointer;
  font-size: 18px;
  color: #888888;
  transition: color 0.3s;
}

.popup-close:hover {
  color: #000000;
}

.game-view {
  height: 100vh;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}

.flex-container {
  display: flex;
  align-items: center;
  justify-content: center;
}

.chessboard-container {
  margin-bottom: 20px;
}

.resign-container {
  margin-top: -100px;
  color: #dddddd;
}

.resign-button {
  padding: 10px 20px;
  font-size: 16px;
  background-color: #f44336;
  color: #fff;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}

.resign-button:hover {
  background-color: #d32f2f;
}
</style>
