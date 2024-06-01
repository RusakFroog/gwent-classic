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
    <li :class="getStyles">
        <a>{{ title }}</a>
    </li>
</template>


<style scoped>
li {
    transition: 0.3s;
    margin-top: 8px;

    background-image: url('../assets/buttons/frame.svg');
    background-size: contain;
    background-repeat: no-repeat;
    width: 200px;
    height: 90px;
}

.active {
    margin-top: 0px;
    background-image: url('../assets/buttons/selected-frame.svg');
}

.active a {
    margin-top: 15px;
    color: black;
}

li a {
    display: block;
    color: #D2B47C;
    text-decoration: none;
    text-align: center;
    cursor: pointer;
    font-size: 36px;
    line-height: 40px;
    transition: 0.3s ease;
    margin-top: 8px;
    user-select: none;
}

li:hover {
    transition: 0.3s;
    background-image: url('../assets/buttons/frame-hover.svg');
}

.active:hover {
    margin-top: 0px;
    background-image: url('../assets/buttons/selected-frame.svg');
}

li:hover a {
    transition: 0.3s;
    color: black;
}
</style>