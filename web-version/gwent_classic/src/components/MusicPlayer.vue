<script>
import VolumeSlider from './VolumeSlider.vue';
export default {
    components: {
        VolumeSlider
    },
    computed: {
        getPaused() {
            return this.isPaused ? "pause" : "play";
        },

        getClass() {
            return this.getPaused;
        }
    },

    methods: {
        play() {
            this.isPaused = !this.isPaused;
        }
    },
    data() {
        return {
            isPaused: true,
        }
    }
}

</script>

<template>
    <div class="music-player flex gap-[25px]">
        <div class="previous" />
        <div  :class="getClass" @click="play()" />
        <div class="next" />
    </div>
</template>

<style scoped>
.music-player {
    width: 173px;
    height: 47px;
}

.previous:hover {
    background-image: url('../assets/images/music-player/player-previous-hover.svg');
}

.next:hover {
    background-image: url('../assets/images/music-player/player-next-hover.svg');
}

.pause:hover {
    background-image: url('../assets/images/music-player/player-pause-hover.svg');
}

.play:hover {
    background-image: url('../assets/images/music-player/player-play-hover.svg');
}

.previous {
    margin-right: 3px;
    width: 27px;
    height: 27px;
    background-image: url('../assets/images/music-player/player-previous.svg');
    background-repeat: no-repeat;
    background-size: contain;
    cursor: pointer;

}

.pause {
    width: 37px;
    height: 37px;
    background-image: url('../assets/images/music-player/player-pause.svg');
    background-repeat: no-repeat;
    background-size: contain;
    cursor: pointer;
    margin-top: -6px;
}

.play {
    width: 37px;
    height: 37px;
    background-image: url('../assets/images/music-player/player-play.svg');
    background-repeat: no-repeat;
    background-size: contain;
    cursor: pointer;
    margin-top: -6px;
}

.next {
    width: 27px;
    height: 27px;
    background-image: url('../assets/images/music-player/player-next.svg');
    background-repeat: no-repeat;
    background-size: contain;
    cursor: pointer;
}
</style>