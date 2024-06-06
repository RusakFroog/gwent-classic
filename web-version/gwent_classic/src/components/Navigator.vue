<script>
import NavigatorItem from './NavigatorItem.vue';
import { useRouter } from 'vue-router';

export default {
    components: {
        NavigatorItem
    },

    methods: {
        goToPage(link) {
            if (this.router.currentRoute.path === link)
                return;

            this.items.forEach(item => item.active = false);
            
            this.router.push(link);

            const findNextItem = this.getItem(link);

            if (findNextItem)
                findNextItem.active = true;
        },

        getItem(link) {
            return this.items.find(item => item.link === link);
        }
    },

    watch: {
        '$route.path': {
            immediate: true,
            handler() {
                const currentRoutePath = this.router.currentRoute.path;

                this.items.forEach(item => item.active = false);
    
                const findCurrentItem = this.getItem(currentRoutePath);
        
                if (findCurrentItem)
                    findCurrentItem.active = true;
            }
        }
    },

    data() {
        return {
            router: useRouter(),
            items: [
                { title: "HOME", link: "/home", right: false, active: false },
                { title: "DECK", link: "/deck", right: false, active: false },
                { title: "RULES", link: "/rules", right: false, active: false },
                { title: "LOG IN", link: "/login", right: true, active: false }
            ]
        }
    }
}
</script>

<template>
    <nav class="bg-repeat h-[90px] bg-[url('./assets/images/navigator/background-village.svg')] p-2">
        <ul class="flex justify-start">
            <NavigatorItem v-for="item in items"
                :title="item.title"
                :right="item.right"
                :active="item.active"
                @click="goToPage(item.link)"
            />
        </ul>
    </nav>
</template>

<style scoped>
* {
    font-family: "Witcher";
}
</style>