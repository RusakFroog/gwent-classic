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
    <section class="flex place-items-start h-screen w-screen bg-[#242424] bg-cover bg-no-repeat bg-[url('./assets/images/registration/background.png')]">
        <div class="flex items-center flex-col ms-20 h-screen bg-[#0D0D0D] bg-opacity-65 px-14 relative">
            <h1 class="font-witcher-alternative mt-6 text-3xl text-[#D2B47C]">Create account</h1>
            <img class="mt-8" src="../assets/images/registration/logo.png"/>
            <div class="flex items-center gap-6 flex-col mt-8">
                <CustomInput placeholder="LOGIN" />
                <CustomInput placeholder="EMAIL" />
                <CustomInput placeholder="PASSWORD" />
                <CustomInput placeholder="REPEAT PASSWORD" />
				<Button class="Button" text="SIGN UP" />
            </div>
        </div>
        <div class="image" />
    </section>
</template>

<style scoped>
* :not(h1, h3) {
    font-family: "Witcher";
}

.Button {
	margin-top: 60px;
}

.image {
    flex-grow: 1;
    height: 100vh;
    background-repeat: no-repeat;
    background-size: cover;
    background-image: url('../assets/images/registration/gwent-game.png');
}

@media screen and (max-height: 905px) {
	.Button {
		margin-top: 30px;
	}
}

@media screen and (max-height: 870px) {
	.Button {
		margin-top: -10px;
	}
}
</style>
