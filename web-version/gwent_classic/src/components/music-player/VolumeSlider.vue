<script>
export default {
    data() {
        return {
            volume: 50
        };
    },

    mounted() {
        this.volume = localStorage.getItem('volume') ?? 50;
    },
    
    watch: {
        volume(newVal) {
            this.saveVolume();
            this.$emit('update-volume', newVal);
        }
    },

    methods: {
        saveVolume() {
            localStorage.setItem('volume', this.volume);
        }
    }
};
</script>

<template>
    <input 
        class="slider" 
        ref="slider"
        v-model="volume" 
        type="range" 
        min="0" max="100" step="1" 
        :style="{
            '--thumb-color': volume < 0.01 ? '#FFFFFF' : '#F1D177',
            'background': `linear-gradient(to top, #F1D177 0%, #F1D177 ${volume}%, #fff ${volume}%, white 100%)`
        }"
    />
</template>

<style scoped>
.slider {
    border-radius: 3px;
    margin-top: -20px;
    writing-mode: vertical-lr;
    height: 60px;
    direction: rtl;
    vertical-align: middle;
    -webkit-appearance: none;
    appearance: none;
    outline: none;
    opacity: 0.7;
    -webkit-transition: 0.2s;
    transition: opacity 0.2s;
    background: linear-gradient(to top, #F1D177 0%, #F1D177 50%, #fff 50%, white 100%);
}

.slider:hover {
    opacity: 1;
    color: #F1D177;
}

.slider::-webkit-slider-thumb {
    -webkit-appearance: none;
    appearance: none;
    border-radius: 3px;
    width: 25px;
    height: 15px;
    background: var(--thumb-color, #F1D177);
    cursor: pointer;
}

.slider::-moz-range-thumb {
    width: 15px;
    height: 15px;
    background: var(--thumb-color, #F1D177);
    cursor: pointer;
    border-radius: 3px;
}
</style>