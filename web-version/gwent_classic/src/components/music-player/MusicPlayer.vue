<script>
import VolumeSlider from './VolumeSlider.vue';
import musicPlayer from '../../services/musicPlayer.js';

export default {
    components: {
        VolumeSlider,
    },

    data() {
        return {
            isPaused: true,
        }
    },

    computed: {
        getPaused() {
            return this.isPaused ? "pause" : "play";
        }
    },

    methods: {
        async play() {
            this.isPaused = !this.isPaused;
            
            await musicPlayer.play();
        },

        nextSong() {
            musicPlayer.nextSong();

            if (this.isPaused === true)
                this.isPaused = false;
        },

        previousSong() {
            musicPlayer.previousSong();
            
            if (this.isPaused === true)
                this.isPaused = false;
        }
    }
};
</script>

<template>
    <div class="music-player">
        <VolumeSlider />
        <div class="previous" @click="previousSong()" />
        <div :class="getPaused" @click="play()" />
        <div class="next" @click="nextSong()" />
    </div>
</template>

<style scoped lang="scss">
.music-player {
    display: flex;
    gap: 25px;
    width: 180px;
}

.next,
.previous {
    width: 27px;
    height: 27px;
    background-image: url('../../assets/images/music-player/player-next.svg');
    background-repeat: no-repeat;
    background-size: contain;
    cursor: pointer;

    &:hover {
        background-image: url('../../assets/images/music-player/player-next-hover.svg');
    }
}

.previous {
    margin-right: 3px;    
    background-image: url('../../assets/images/music-player/player-previous.svg');

    &:hover {
        background-image: url('../../assets/images/music-player/player-previous-hover.svg');
    }
}

.pause, 
.play {
    width: 37px;
    height: 37px;
    background-image: url('../../assets/images/music-player/player-play.svg');
    background-repeat: no-repeat;
    background-size: contain;
    cursor: pointer;
    margin-top: -6px;

    &:hover {
        background-image: url('../../assets/images/music-player/player-play-hover.svg');
    }
}

.play {
    background-image: url('../../assets/images/music-player/player-pause.svg');

    &:hover {
        background-image: url('../../assets/images/music-player/player-pause-hover.svg');
    }
}
</style>