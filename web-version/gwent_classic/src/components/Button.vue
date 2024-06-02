<script>
export default {
    props: {
        title: {
            type: String,
            required: true
        },

        active: {
            type: Boolean,
            default: false
        },

        right: {
            type: Boolean,
            default: false
        },
    },

    computed: {
        getFloat() {
            return this.right ? "ml-auto" : "mr-5";
        },

        isActive() {
            return this.active ? "active" : "";
        },

        getStyles() {
            return [this.getFloat, this.isActive];
        }
    }
};
</script>

<template>
    <li :class="getStyles" @click.prevent="onClick">
        <a>{{ title }}</a>
    </li>
</template>

<style scoped>
li {
    transition: 0.3s;
    cursor: pointer;
    background-image: url('../assets/buttons/frame.svg');
    background-size: contain;
    background-repeat: no-repeat;
    width: 320px;
    height: 100px;
    display: flex;
    align-items: center;
    justify-content: center;
}

.active {
    background-image: url('../assets/buttons/selected-frame.svg');
    height: 150px;
}

.active a {
    color: black;
}

li a {
    display: block;
    color: #D2B47C;
    text-decoration: none;
    text-align: center;
    font-size: 36px;
    line-height: 40px;
    transition: 0.3s ease;
    user-select: none;
}

li:hover {
    transition: 0.3s;
    background-image: url('../assets/buttons/frame-hover.svg');
}

.active:hover {
    background-image: url('../assets/buttons/selected-frame.svg');
}

li:hover a {
    transition: 0.3s;
    color: black;
}
</style>
