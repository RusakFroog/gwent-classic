<script>
export default {
    props: {
        title: {
            type: String,
            required: true
        },

        right: {
            type: Boolean,
            default: false
        },
    },

    computed: {
        getFloat() {
            return this.right ? "ml-auto" : "mr-5";
        }
    }
};
</script>

<template>
    <li :class="getFloat">    
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
}

li:hover {
    transition: 0.3s;
    margin-top: 0px;
    background-image: url('../assets/buttons/selected-frame.svg');
}

li:hover a {
    transition: 0.3s;
    margin-top: 15px;
    color: black;
}
</style>