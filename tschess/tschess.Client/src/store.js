import {createStore} from 'vuex'

export default createStore({
    state() {
        return {
            infos: {
                username: "",
                token: "",
                isLoggedIn: false,
                isInQueue: false,
                currentGameGuid: "offline",
            },
        }
    },
    mutations: {
        authenticate(state, userdata) {
            if (!userdata) {
                state.infos = { username: "", token: "", isLoggedIn: false };
                return;
            }
            state.infos.username = userdata.username;
            state.infos.token = userdata.token;
            state.infos.isLoggedIn = true;
        },
        joinQueue(state) {
            state.infos.isInQueue = true;
        },
        joinGame(state, gameGuid) {
            state.infos.isInQueue = false;
            state.infos.currentGameGuid = gameGuid;
        }
    }
});
