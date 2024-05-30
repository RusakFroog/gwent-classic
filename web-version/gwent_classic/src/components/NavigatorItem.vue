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
            return this.right ? "ml-auto" : "";
        }
    }
};
</script>

<template>
    <li :class="getFloat">
        <a class="block text-white text-center no-underline px-4 text-3xl">{{ title }}</a>
    </li>
</template>