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
    <nav class="bg-repeat h-[90px] bg-[url('./assets/images/navigator/background-village.svg')] p-2">
        <ul class="flex justify-start">
            <NavigatorItem title="HOME" />
            <NavigatorItem title="DECK" />
            <NavigatorItem title="RULES" />
            <NavigatorItem title="LOG IN" right />
        </ul>
    </nav>
</template>

<style scoped>
* {
    font-family: "Witcher";
}
</style>