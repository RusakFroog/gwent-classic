<script>
import VolumeSlider from './VolumeSlider.vue';
import { play, next, previous, setVolume, getCurrentTimecode } from './PlayerMp3';

export default {
    components: {
        VolumeSlider,
        
    },

    data() {
        return {
            isVolumeSliderVisible: false,
            isPaused: true,
            volume: 50,
            isMuted: false
        }
    },

    computed: {
        getPaused() {
            return this.isPaused ? "pause" : "play";
        },

        getVolume() {
            return this.isMuted ? "mute" : "un-mute";
        },

        getVolumeClass() {
            return this.getVolume;
        },

        getClass() {
            return this.getPaused;
        }
    },

    methods: {
        toggleVolumeSlider() {
            setTimeout(() => {
                this.isVolumeSliderVisible = !this.isVolumeSliderVisible;
            }, 100);
        },

        playPause() {
            this.isPaused = !this.isPaused;
            play();
        },

        handleVolumeUpdate(newVolume) {
            if (newVolume == 0) {
                this.isMuted = true;
            } else {
                this.isMuted = false;
            }

            this.volume = newVolume;
            
            setVolume(newVolume);
        },

        eventNext() {
            next();
        },

        eventPrevious() {
            previous();
        }
    }
};
</script>

<template>

    <div class="music-player">
        <div class="volume">
            <div :class="getVolumeClass" @mouseenter="toggleVolumeSlider()" v-if="!isVolumeSliderVisible"
                @mouseleave="toggleVolumeSlider()" />
            <VolumeSlider @update-volume="handleVolumeUpdate" @mouseleave="toggleVolumeSlider()"
                v-if="isVolumeSliderVisible" />
        </div>
        <div class="previous" @click="eventPrevious()" />
        <div :class="getClass" @click="playPause()" />
        <div class="next" @click="eventNext()" />
    </div>
</template>

<style scoped>
.volume {
    position: absolute;
    right: 220px;
}

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

.pause:hover {
    background-image: url('../../assets/images/music-player/player-pause-hover.svg');
}

.play:hover {
    background-image: url('../../assets/images/music-player/player-play-hover.svg');
}

.mute {
    width: 27px;
    height: 27px;
    background-image: url('../../assets/images/music-player/volume-none.svg');
    background-repeat: no-repeat;
    background-size: contain;
    cursor: pointer;
}

.mute:hover {
    background-image: url('../../assets/images/music-player/volume-none-hover.svg');
}

.un-mute {
    width: 27px;
    height: 27px;
    background-image: url('../../assets/images/music-player/volume.svg');
    background-repeat: no-repeat;
    background-size: contain;
    cursor: pointer;
}

.un-mute:hover {
    background-image: url('../../assets/images/music-player/volume-hover.svg');
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

.pause {
    width: 37px;
    height: 37px;
    background-image: url('../../assets/images/music-player/player-pause.svg');
    background-repeat: no-repeat;
    background-size: contain;
    cursor: pointer;
    margin-top: -6px;
}

.play {
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