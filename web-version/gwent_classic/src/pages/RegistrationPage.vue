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
        <div class="panel flex items-center flex-col ms-20 h-screen bg-[#0D0D0D] bg-opacity-65 px-14 relative">
            <h1 class="font-witcher-alternative mt-6 text-3xl text-[#D2B47C]">Create account</h1>
            <img class="logo mt-8" src="../assets/images/registration/logo.png"/>
            <div class="inputs">
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
.inputs {
    display: flex;
    align-items: center;
    gap: 12px;
    flex-direction: column;
    margin-top: 20px;
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
		margin-top: 0px;
	}
    .inputs {
        margin-top: 10px;
    }
}
@media screen and (max-height: 770px) {
    .inputs {
        gap: 2px;
    }
    .logo {
        width: 280px;
        height: 120px;
    }   
    .Button {
        margin-top: 30px;
    }

}
@media screen and (max-width: 700px) {
    .panel {
        margin-inline-start: 0;
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
    }
}
</style>
