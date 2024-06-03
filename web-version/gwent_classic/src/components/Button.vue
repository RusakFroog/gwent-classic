<script>
export default {
    props: {
        text: {
            type: String,
            required: true
        },
    },

    computed: {
        getStyles() {
            return {
                width: this.width,
                height: this.height
            }
        }
    }
};
</script>

<template>
    <button :style="getStyles"><a>{{ text }}</a></button>
</template>

<style scoped>
button {
    width: 320px;
    height: 100px;
    transition: 0.3s;
    cursor: pointer;
    background-image: url('../assets/buttons/frame.svg');
    background-size: contain;
    background-repeat: no-repeat;
    display: flex;
    align-items: center;
    justify-content: center;
}

button a {
    display: block;
    color: #D2B47C;
    text-decoration: none;
    text-align: center;
    font-size: 36px;
    line-height: 40px;
    transition: 0.3s ease;
    user-select: none;
}

button:hover {
    transition: 0.3s;
    background-image: url('../assets/buttons/frame-hover.svg');
}

button:hover a {
    transition: 0.3s;
    color: black;
}
@media screen and (max-height: 770px) {
    button {
        width: 200px;
        height: 56px
    }
}
</style>
