<script>
import NavigatorItem from './NavigatorItem.vue';
import ProfileModal from "./ProfileModal.vue";
import { useRouter } from 'vue-router';
import { loggedIn } from '../services/accounts.js';

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
        },

        async checkLoggedIn() {
            if (sessionStorage.getItem("new_session") === "false")
                return;

            console.log("Check if looged in");

            const isLoggedIn = await loggedIn();
    
            if (isLoggedIn) {
                const loginItem = this.getItem("/login");
    
                if (loginItem) {
                    loginItem.title = "PROFILE";
                    loginItem.link = "/profile";
                  
                    loginItem.onClick = () => {
                        console.log("on click profile");
                    };
                }
            }
            else {
                const profileItem = this.getItem("/profile");
    
                if (profileItem) {
                    profileItem.title = "LOG IN";
                    profileItem.link = "/login";
                }
            }

            sessionStorage.setItem('logged_in', isLoggedIn ? 'true' : 'false');
            sessionStorage.setItem('new_session', 'false');
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

                this.checkLoggedIn();
            }
        }
    },

    mounted() {
        if (sessionStorage.getItem("logged_in") == "true") {
            console.log("on mount nav");

            const loginItem = this.getItem("/login");
    
            if (loginItem) {
                loginItem.title = "PROFILE";
                loginItem.link = "/profile";

                loginItem.onClick = () => {
                    ProfileModal.showModal();
                };
            }
        }
    },

    data() {
        return {
            router: useRouter(),
            items: [
                { title: "HOME", link: "/home", right: false, active: false, onClick: null },
                { title: "DECK", link: "/deck", right: false, active: false, onClick: null },
                { title: "RULES", link: "/rules", right: false, active: false, onClick: null },
                { title: "LOG IN", link: "/login", right: true, active: false, onClick: null }
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
                @click="item.onClick ? item.onClick() : goToPage(item.link)"
            />
        </ul>
    </nav>
</template>

<style scoped>
* {
    font-family: "Witcher";
}
</style>