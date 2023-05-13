<script setup>
import axios from "axios";
import signalRService from '../services/SignalRService.js';
</script>

<template>
    <div>
        {{ this.gameState }}
    </div>
</template>

<script>
export default {
    data() {
        return {
            gameState: ""
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
        setGameState(fen) {
            this.gameState = fen;
            console.log(fen);
        },
        gameEnd(winner) {
            // Implement when Game is playable and logic is done
            console.log("Game ended, winner is " + winner);
        }
    }
}
</script>