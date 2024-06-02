<script>
import Button from '../components/Button.vue';
import CustomInput from '../components/CustomInput.vue';

export default {
    data() {
        return {
            
        }
    },

    components: {
        Button,
        CustomInput
    }
};
</script>

<template>
    <section class="flex flex-col place-items-start h-screen w-screen bg-[#242424] bg-cover bg-no-repeat bg-[url('./assets/images/registration/background.png')]">
        <div class="flex items-center flex-col ms-20 h-screen bg-[#0D0D0D] bg-opacity-65 px-14 relative">
            <h1 class="font-witcher-alternative mt-6 text-3xl text-[#D2B47C]">Create account</h1>
            <img class="mt-8" src="../assets/images/registration/logo.png"/>
            <div class="flex items-center space-y-6 flex-col mt-8">
                <CustomInput placeholder="LOGIN" />
                <CustomInput placeholder="EMAIL" />
                <CustomInput placeholder="PASSWORD" />
                <CustomInput placeholder="REPEAT PASSWORD" />
            </div>
            <Button class="mt-[10vh]" text="SIGN UP"/>
        </div>
        <div class="image" />
    </section>
</template>

<style scoped>
* :not(h1, h3) {
    font-family: "Witcher";
}

.image {
    width: 100%;
    height: 100%;
    position: absolute;
    background-repeat: no-repeat;
    background-size: contain;
    background-image: url('../assets/images/registration/gwent-game.png');
}

</style>
