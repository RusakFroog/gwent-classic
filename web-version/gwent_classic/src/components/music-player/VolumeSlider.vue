<script>
import musicPlayer from '../../services/musicPlayer.js';

export default {
    data() {
        return {
            isSliderVisible: false,
            isMuted: false,
            volume: 50
        };
    },

    mounted() {
        this.isMuted = localStorage.getItem('isMuted') ? localStorage.getItem('isMuted') === 'true' : false;
        
        this.volume = localStorage.getItem('volume') ? parseFloat(localStorage.getItem('volume')) : 50;
    },
    
    watch: {
        volume() {
            if (localStorage.getItem('isMuted') == 'false') {
                this.isMuted = this.volume == 0;
            }

            this.saveVolume();
        }
    },

    computed: {
        getVolumeClass() {
            return this.isMuted ? "mute" : "un-mute";
        }
    },

    methods: {
        saveVolume() {
            musicPlayer.setVolume(this.volume);
            musicPlayer.setMuted(this.isMuted);
            
            localStorage.setItem('volume', this.volume);
            localStorage.setItem('isMuted', this.isMuted);
        },

        toggleVolumeSlider() {
            setTimeout(() => {
                this.isSliderVisible = !this.isSliderVisible;
            }, 100);
        },

        toggleMute() {
            this.isMuted = !this.isMuted;
            this.volume == 0;

            this.saveVolume();
        }
    }
};
</script>

<template>
    <div class="volume"> 
        <div :class="getVolumeClass" @click="toggleMute()" @mouseenter="toggleVolumeSlider()" />
        <input
            v-show="isSliderVisible"
            class="slider" 
            v-model="volume" 
            @mouseleave="toggleVolumeSlider()" 
            type="range" 
            min="0" max="100" step="1" 
            :style="{
                '--thumb-color': volume < 1 ? '#FFFFFF' : '#F1D177',
                'background': `linear-gradient(to right, #F1D177 0%, #F1D177 ${volume}%, #fff ${volume}%, white 100%)`
            }"
        />
    </div>
</template>

<style scoped>
.volume {
    position: absolute;
    right: 220px;
}

.slider {
    position: absolute;
    border-radius: 3px;
    margin-top: 10px;
    writing-mode: horizontal-tb;
    height: 15px;
    direction:ltr;
    vertical-align: middle;
    -webkit-appearance: none;
    appearance: none;
    outline: none;
    opacity: 0.7;
    -webkit-transition: 0.2s;
    transition: opacity 0.2s;
    background: linear-gradient(to right, #F1D177 0%, #F1D177 50%, #fff 50%, white 100%);
}

.slider:hover {
    opacity: 1;
    color: #F1D177;
}

.slider::-webkit-slider-thumb {
    -webkit-appearance: none;
    appearance: none;
    border-radius: 3px;
    width: 0px;
    height: 3px;
    background: var(--thumb-color, #F1D177);
    cursor: pointer;
}

.slider::-moz-range-thumb {
    width: 0px;
    height: 3px;
    background: var(--thumb-color, #F1D177);
    cursor: pointer;
    border-radius: 3px;
}

.mute {
    position: static;
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
    position: static;
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

</style>