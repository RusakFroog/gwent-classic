<script>
import Button from '../Button.vue';

export default {
    components: {
        Button
    },

    props: {
        id: {
            type: Number,
            required: true
        },

        owner: {
            type: String,
            required: true
        },

        
        roomName: {
            type: String,
            required: true
        },
        
        password: {
            type: Boolean,
            required: true
        }
    }
}
</script>

<template>
    <li class="flex outline outline-2 outline-[#AA9D9D] items-center mb-2 h-[50px]">
        <h1 class="text-[#D2B47C] text-2xl ml-2 w-[4.5%]">{{ id }}</h1>
        <h1 class="text-[#D2B47C] text-2xl w-[30.8%]">{{ owner }}</h1>
        <h1 class="text-[#D2B47C] text-2xl w-[30.8%]">{{ roomName }}</h1>
        <h1 class="text-[#D2B47C] text-2xl w-[3.7%]">{{ password ? "YES" : "NO" }}</h1>
        <Button class="button-join" text="JOIN" />
    </li>
</template>

<style scoped>
.button-join {
    margin-left: auto;
    margin-right: 8px;
    width: 130px;
    height: 38px;
    margin-top: 2px;
}

.button-join :deep(a) {
    font-size: 26px;
    margin-bottom: 0px;
}
</style>