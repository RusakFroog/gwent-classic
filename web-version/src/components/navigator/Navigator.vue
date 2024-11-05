<script>
import NavigatorItem from './NavigatorItem.vue';
import ProfileModal from './ProfileModal.vue';
import MusicPlayer from '../music-player/MusicPlayer.vue';
import { useRouter } from 'vue-router';
import { loggedIn } from '../../services/Accounts.js';

export default {
    components: {
        NavigatorItem,
        ProfileModal,
        MusicPlayer
    },

    data() {
        return {
            router: useRouter(),
            items: [
                { title: "HOME", link: "/home", right: false, active: false, onClick: null },
                { title: "DECK", link: "/deck", right: false, active: false, onClick: null },
                { title: "RULES", link: "/rules", right: false, active: false, onClick: null },
                { title: "LOG IN", link: "/login", right: true, active: false, onClick: null }
            ],
            profileShow: false,
        }
    },

    methods: {
        goToPage(link) {
            if (this.isCurrentRoute(link)) 
                return;

            this.resetActiveItems();
            this.router.push(link);
            this.setActiveItem(link);
        },

        isCurrentRoute(link) {
            return this.router.currentRoute.path === link;
        },

        resetActiveItems() {
            this.items.forEach(item => item.active = false);
        },

        setActiveItem(link) {
            const item = this.getItem(link);

            if (item) 
                item.active = true;
        },

        getItem(link) {
            return this.items.find(item => item.link === link);
        },

        async checkLoggedIn() {
            if (sessionStorage.getItem("new_session") === "false") 
                return;
            
            const isLoggedIn = await loggedIn();
            
            this.updateLoginStatus(isLoggedIn);
            
            sessionStorage.setItem('logged_in', isLoggedIn ? 'true' : 'false');
            sessionStorage.setItem('new_session', 'false');
        },

        updateLoginStatus(isLoggedIn) {
            if (isLoggedIn) {
                const loginItem = this.getItem("/login");

                this.setProfileItem(loginItem);
            } else {
                const profileItem = this.getItem("/profile");

                this.setLoginItem(profileItem);
            }
        },

        setProfileItem(item) {
            if (item) {
                item.title = "PROFILE";
                item.link = "/profile";
                item.onClick = this.toggleProfileModal;
            }
        },

        setLoginItem(item) {
            if (item) {
                item.title = "LOG IN";
                item.link = "/login";
                item.onClick = null;
            }
        },

        toggleProfileModal() {
            this.profileShow = !this.profileShow;
        },

        handleRouteChange() {
            this.resetActiveItems();
            this.setActiveItem(this.router.currentRoute.path);
            this.checkLoggedIn();

            this.profileShow = false;
        },

        initializeLoginState() {
            if (sessionStorage.getItem("logged_in") === "true") {
                this.setProfileItem(this.getItem("/login"));
            }
        }
    },

    watch: {
        '$route.path': {
            immediate: true,
            handler() {
                this.handleRouteChange();
            }
        }
    },

    mounted() {
        this.initializeLoginState();
    }
}
</script>

<template>
    <nav class="bg-repeat h-[90px] bg-[url('./assets/navigator/background-village.svg')] p-2">
        <ul class="flex justify-start">
            <NavigatorItem v-for="item in items"
                :title="item.title"
                :right="item.right"
                :active="item.active"
                @click="item.onClick ? item.onClick() : goToPage(item.link)"
            />
            <MusicPlayer class="absolute right-0 mr-[300px] mt-[30px]" />
        </ul>
    </nav>
    <transition name="fade">
        <ProfileModal class="fade-in" v-if="profileShow" />
    </transition>
</template>

<style scoped>
.fade-in {
	opacity: 1;
	animation-name: fade-in-opacity;
	animation-iteration-count: 1;
	animation-timing-function: ease-in-out;
	animation-duration: 0.3s;
}

@keyframes fade-in-opacity {
	0% {
		opacity: 0;
	}
	100% {
		opacity: 1;
	}
}

.fade-enter-active, .fade-leave-active {
    transition: opacity 0.3s ease;
}

.fade-enter, .fade-leave-to {
    opacity: 0;
}
</style>