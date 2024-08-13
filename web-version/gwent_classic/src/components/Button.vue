<script>
export default {
    name: "Button",

    props: {
        text: {
            type: String,
            required: true
        }
    }
};
</script>

<template>
    <button type="button">{{ text }}</button>
</template>

<style scoped lang="scss">
$transition-duration: 0.3s;

button {
    display: flex;
    align-items: center;
    justify-content: center;
    width: 320px;
    height: 92px;

    background-image: url('../assets/buttons/frame.svg');
    background-size: contain;
    background-repeat: no-repeat;
    
    font-size: 36px;
    color: #D2B47C;
    text-decoration: none;
    text-align: center;
    line-height: 40px;

    user-select: none;
    transition: background-image $transition-duration ease, color $transition-duration ease;

    &:hover {
        background-image: url('../assets/buttons/frame-hover.svg');
        color: black;
    }
}
</style>