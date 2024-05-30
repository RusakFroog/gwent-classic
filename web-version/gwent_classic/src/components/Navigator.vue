<script>
import NavigatorItem from './NavigatorItem.vue';

export default {
    components: {
        NavigatorItem
    },

    data() {
        return {

        }
    },
}
</script>

<template>
    <nav class="bg-black p-4">
        <ul class="flex justify-start items-center">
            <NavigatorItem title="HOME" />
            <NavigatorItem title="DECK" />
            <NavigatorItem title="RULES" right />
        </ul>
    </nav>
</template>

<style scoped>
* {
    font-family: "Witcher";
}
</style>