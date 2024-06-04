<script>
import Button from '../components/Button.vue';
import CustomInput from '../components/CustomInput.vue';
import { useRouter } from 'vue-router';

export default {
    data() {
        return {
            router: useRouter(),
            activeInput: 0,
            inputs: [
                { placeholder: 'LOGIN', type: 'text' },
                { placeholder: 'PASSWORD', type: 'password' },
            ],

            /** @type {CustomInput[]} */
            componentInputs: [],

            error: {
                active: true,
                text: "12sdfdsasdfdsaf sdfdsfds3 "
            }
        }
    },

    components: {
        Button,
        CustomInput
    },

    methods: {
        goToPage(link) {
            if (this.router.currentRoute.path === link)
                return;

            this.router.push(link);
        },

        setActiveInput(index) {
            this.activeInput = index;
        },

        nextInput() {
            if (this.activeInput >= this.inputs.length - 1)
                return;

            this.activeInput++;

            const input = this.componentInputs[this.activeInput];
            input.focusInput();
        },

        showError(message) {
            this.error.text = message;

            this.error.active = true;
        },

        hideError() {
            if (!this.error.active)
                return;

            this.error.active = false;
        },

        login() {
            this.hideError();
            //...
        }
    },

    mounted() {
        for (let i = 0; i < this.inputs.length; i++) {
            const input = this.$refs[`custom_input${i}`][0];

            this.componentInputs.push(input);
        }
    }
};
</script>

<template>
    <section
        class="flex place-items-start h-[calc(100vh-90px)] w-screen bg-[#242424] bg-cover bg-no-repeat bg-[url('./assets/images/registration/background.png')]">
        <div class="panel flex items-center flex-col ms-20 h-full bg-[#0D0D0D] bg-opacity-65 px-14 relative">
            <h1 class="font-witcher-alternative mt-6 text-3xl text-[#D2B47C]">Log in to account</h1>
            <img class="logo select-none mt-8" src="../assets/images/registration/logo.png" />
            <div class="inputs">
                <CustomInput v-for="(input, index) in inputs" class="custom-input" @keyup.enter="nextInput"
                    :onFocus="() => setActiveInput(index)" :ref="'custom_input' + index" :type="input.type"
                    :placeholder="input.placeholder" />
                <Button class="log-in" @click="login()" text="LOG IN" />
            </div>
            <div class="error flex justify-start items-center w-[310px] mt-[10px]" v-if="error.active">
                <img class="error-image w-[60px] h-[60px]" src="../assets/images/info.svg" />
                <text class="text-[#AB1E1E] pl-[10px] text-[18px]">{{ error.text }}</text>
            </div>
            <p class="text-[#8D8D8D] text-[24px] mt-[40px]">Don't have an account? <a @click="goToPage('/register')" class="to-sign-up text-[#D2B47C] hover:text-[#FFB500] cursor-pointer">SIGN UP</a></p>
        </div>
        <div class="image h-full" />
    </section>
</template>

<style scoped>
* :not(h1, h3) {
    font-family: "Witcher";
}

.inputs {
    display: flex;
    align-items: center;
    gap: 24px;
    flex-direction: column;
    margin-top: 120px;
}

.log-in {
    margin-top: 80px;
}

.image {
    flex-grow: 1;
    background-repeat: no-repeat;
    background-size: cover;
    background-image: url('../assets/images/registration/gwent-game.png');
}

@media screen and (max-height: 1020px) {
    .log-in {
        margin-top: 0px;
    }

    .error {
        margin-top: 10px;
    }

    .inputs {
        margin-top: 10px;
    }
}

@media screen and (max-height: 865px) {
    .error {
        max-width: 300px;

    }

    .error text {
        font-size: 14px;
    }

    .error-image {
        width: 40px;
        height: 40px;
    }

    .inputs {
        gap: 4px;
    }

    .logo {
        width: 300px;
        height: 120px;
    }

    .log-in {
        margin-top: 20px;
        width: 200px;
        height: 56px;
    }

    .log-in :deep(a) {
        font-size: 27px;
    }

    .custom-input :deep(.input-field) {
        width: 300px;
        font-size: 25px;
        padding-bottom: 23px;
        margin-top: -10px;
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

    .image {
        display: none;
    }
}
</style>
