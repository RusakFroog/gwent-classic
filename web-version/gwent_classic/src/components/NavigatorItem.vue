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
            return (this.right ? "ml-auto" : "mr-5") + " p-7 w-[321px] h-[90px] bg-[url('./assets/buttons/frame.svg')]"; //
        }
    }
};
</script>

<template>
    <li :class="getFloat">    
        <a class="block text-[#D2B47C] text-center no-underline cursor-pointer text-4xl">
            {{ title }}
        </a>
    </li>
</template>


<style scoped>
li:not(:hover) {
    transition: 0.3s;
}

li:hover {
    a {
        transition: 0.3s;
        color: black;
    }
    transition: 0.3s;
    background-image: url('../assets/buttons/selected-frame.svg');
}

</style>