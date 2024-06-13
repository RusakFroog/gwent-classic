import { createStore } from 'vuex';

const store = createStore({
    state: {
        volume: 0.5
    },
    mutations: {
        setVolume(state, volume) {
            state.volume = volume;
        },
        loadVolume(state) {
            const savedVolume = localStorage.getItem('volume');
            if (savedVolume !== null) {
                state.volume = parseFloat(savedVolume);
            }
        }
    },
    actions: {
        setVolume({ commit }, volume) {
            commit('setVolume', volume);
            localStorage.setItem('volume', volume);
        },
        loadVolume({ commit }) {
            commit('loadVolume');
        }
    },
    getters: {
        volume: state => state.volume
    }
});

export default store;