// ChessBoard.vue
<template>
  <div id="chess-board">
    <div v-html="boardHTML"></div>
  </div>
</template>

<script>
import Game from "./game.js";

export default {
  data() {
    return {
      game: null,
      boardHTML: "",
    };
  },
  mounted() {
    this.game = new Game();
    this.boardHTML = this.game.drawBoard();
  },
};
</script>

<style scoped>
#chess-board {
  display: flex;
  flex-wrap: wrap;
  width: 400px;
  height: 400px;
}

.square {
  width: 50px;
  height: 50px;
  display: flex;
  justify-content: center;
  align-items: center;
}

.black-square {
  background-color: #8b4513;
}

.white-square {
  background-color: #f0e68c;
}
</style>
