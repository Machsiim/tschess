import {createStore} from 'vuex'

export default createStore({
    state() {
        return {
            infos: {
                username: "",
                token: "",
                isLoggedIn: false
            },
        }
    },
    mutations: {
        authenticate(state, userdata) {
            if (!userdata) {
                state.infos = { username: "", guid: "", isLoggedIn: false };
                return;
            }
            state.infos.username = userdata.username;
            state.infos.token = userdata.roken;
            state.infos.isLoggedIn = true;
        },
    }
});
