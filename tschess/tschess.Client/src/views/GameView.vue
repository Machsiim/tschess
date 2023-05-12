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
    }
}
</script>