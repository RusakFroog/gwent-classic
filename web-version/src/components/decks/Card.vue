<script>
import { CDN_URL } from '../../services/data/constants.js';

export default {
    name: "Card",
    
    props: {
        cardId: {
            type: Number,
            required: true
        }
    },

    computed: {
        getImagePath() {
            return `${CDN_URL}/cards/${this.$props.cardId}.png`;
        }
    }
}
</script>

<template>
<div class="card">
    <img :src=getImagePath>
</div>
</template>

<style scoped lang="scss">
@import "../../utilities/variables.scss";

.card {
    width: 192px;
    height: 365px;
    border: 5px solid transparent;
    border-radius: 20px;
    // transition: background-color 0.3s ease-in-out;

    &:hover {
        background: linear-gradient(0deg, gold, $bronze);
        background-size: 300% 300%;
        filter: drop-shadow(0 0 6px gold);
    }

    img {
        border-radius: 15px;
    }
}
</style>