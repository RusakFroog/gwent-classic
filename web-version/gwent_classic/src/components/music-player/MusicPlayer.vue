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
        },

        getTime() {
            const timeInSeconds = musicPlayer.currentSongTime;

            const minutes = Math.floor(timeInSeconds / 60);
            const seconds = Math.floor(timeInSeconds % 60);

            return `${minutes}:${seconds.toString().padStart(2, '0')}`;
        }
    },

    methods: {
        play() {
            this.isPaused = !this.isPaused;
            
            musicPlayer.play();
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

<style scoped>
.music-player {
    display: flex;
    gap: 25px;
    width: 180px;
}

.previous:hover {
    background-image: url('../../assets/images/music-player/player-previous-hover.svg');
}

.next:hover {
    background-image: url('../../assets/images/music-player/player-next-hover.svg');
}

.play:hover {
    background-image: url('../../assets/images/music-player/player-pause-hover.svg');
}

.pause:hover {
    background-image: url('../../assets/images/music-player/player-play-hover.svg');
}

.previous {
    margin-right: 3px;
    width: 27px;
    height: 27px;
    background-image: url('../../assets/images/music-player/player-previous.svg');
    background-repeat: no-repeat;
    background-size: contain;
    cursor: pointer;
}

.play {
    width: 37px;
    height: 37px;
    background-image: url('../../assets/images/music-player/player-pause.svg');
    background-repeat: no-repeat;
    background-size: contain;
    cursor: pointer;
    margin-top: -6px;
}

.pause {
    width: 37px;
    height: 37px;
    background-image: url('../../assets/images/music-player/player-play.svg');
    background-repeat: no-repeat;
    background-size: contain;
    cursor: pointer;
    margin-top: -6px;
}

.next {
    width: 27px;
    height: 27px;
    background-image: url('../../assets/images/music-player/player-next.svg');
    background-repeat: no-repeat;
    background-size: contain;
    cursor: pointer;
}
</style>