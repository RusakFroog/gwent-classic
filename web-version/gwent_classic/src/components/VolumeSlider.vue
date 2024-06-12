<script>
export default {
    data() {
        return {
            volume: 0.5
        };
    },
    mounted() {
        const input = document.querySelector(".slider");
        input.addEventListener("input", function () {
            const value = (this.value - this.min) / (this.max - this.min) * 100;
            this.style.background = `linear-gradient(to top, #F1D177 0%, #F1D177 ${value}%, #fff ${value}%, white 100%)`;
        });
    },
    watch: {
        volume() {
            console.log(this.volume);
        }
    }
};
</script>

<template>
    <div class="slider-container">
        <input type="range" min="0" max="1" step="0.01" v-model="volume" class="slider" />
    </div>
</template>

<style scoped>
.slider-container {
    width: 100%;
}

.slider {
    border-radius: 3px;
    writing-mode: vertical-lr;
    height: 100px;
    direction: rtl;
    vertical-align: middle;
    -webkit-appearance: none;
    appearance: none;
    outline: none;
    opacity: 0.7;
    -webkit-transition: 0.2s;
    transition: opacity 0.2s;
    background-color: hwb(53 60% 36%);
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
    height: 25px;
    background: #F1D177;
    cursor: pointer;
}

.slider::-moz-range-thumb {
    width: 25px;
    height: 25px;
    background: #F1D177;
    cursor: pointer;
    border-radius: 3px;
}
</style>