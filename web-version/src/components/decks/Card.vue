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
.card {
    width: 192px;
    height: 365px;
    background-image: url('../../assets/decks/card.svg');
    background-repeat: no-repeat;
    // background-size: contain;
    // background-position: center;
}
</style>