<script>
import Button from '../components/Button.vue';
import CustomInput from '../components/Input.vue';

export default {
    data() {
        return {
            isActive: false
        };
    },
    components: {
        Button,
        CustomInput
    },
    methods: {
        signUp() {
            this.isActive = !this.isActive; // Toggle active state
        }
    }
};
</script>

<template>
    <section class="flex flex-col place-items-start h-screen w-screen bg-[#242424] bg-cover bg-no-repeat bg-[url('./assets/images/registration/background.svg')]">
        <div class="flex items-center flex-col ms-20 h-screen bg-[#0D0D0D] bg-opacity-65 pe-14 ps-14 relative">
            <h1 class="font-witcher-alternative mt-6 text-3xl text-[#D2B47C]">Create account</h1>
            <img class="mt-8" src="../assets/images/registration/logo-png.png"/>
            <div class="flex items-center space-y-6 flex-col mt-8">
                <CustomInput :placeholder="'LOGIN'" />
                <CustomInput :placeholder="'EMAIL'" />
                <CustomInput :placeholder="'PASSWORD'" />
                <CustomInput :placeholder="'REPEAT PASSWORD'" />
            </div>
            <Button class="button" :title="'CONFIRM'" :active="isActive" @click="signUp" />
        </div>
        <img class="absolute top-0 translate-x-[630px] w-[80%] " src="../assets/images/registration/gwent-game.png"/>
    </section>
</template>

<style scoped>
.button {
	margin-top: 30px;
}
* :not(h1, h3) {
    font-family: "Witcher";
}
</style>
