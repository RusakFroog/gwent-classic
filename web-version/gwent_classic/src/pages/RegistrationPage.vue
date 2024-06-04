<script>
import Button from '../components/Button.vue';
import CustomInput from '../components/CustomInput.vue';

export default {
    data() {
        return {
            activeInput: 0,
            inputs: [
                { placeholder: 'LOGIN', type: 'text' },
                { placeholder: 'EMAIL', type: 'email' },
                { placeholder: 'PASSWORD', type: 'password' },
                { placeholder: 'REPEAT PASSWORD', type: 'password' },
            ],

            /** @type {CustomInput[]} */
            componentInputs: [],

            error: {
                active: false,
                text: ""
            }
        }
    },
    
    components: {
        Button,
        CustomInput
    },

    methods: {
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
            this.error.active = false;
        },

        signUp() {
            console.log(this.componentInputs[2].inputValue);

            if (this.componentInputs[2].inputValue !== this.componentInputs[3].inputValue)
                return this.showError('ERROR: Passwords do not match');
            
            this.hideError();
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
    <section class="flex place-items-start h-screen w-screen bg-[#242424] bg-cover bg-no-repeat bg-[url('./assets/images/registration/background.png')]">
        <div class="panel flex items-center flex-col ms-20 h-screen bg-[#0D0D0D] bg-opacity-65 px-14 relative">
            <h1 class="font-witcher-alternative mt-6 text-3xl text-[#D2B47C]">Create account</h1>
            <img class="logo select-none mt-8" src="../assets/images/registration/logo.png" />
            <div class="inputs">
                <CustomInput v-for="(input, index) in inputs"
                    class="custom-input"
                    @keyup.enter="nextInput"
                    :onFocus="() => setActiveInput(index)"
                    :ref="'custom_input' + index"
                    :type="input.type"
                    :placeholder="input.placeholder" 
                />
                <Button class="sign-up" @click="signUp()" text="SIGN UP" />
                <div class="error flex justify-center items-center w-[440px] mt-[30px]" v-if="error.active">
                    <img class="w-[60px] h-[60px]" src="../assets/images/info.svg"/> 
                    <text class="text-[#AB1E1E] pl-[10px] text-[18px]">{{ error.text }}</text>
                </div>
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
    gap: 24px;
    flex-direction: column;
    margin-top: 20px;
}

.sign-up {
    margin-top: 35px;
}

.image {
    flex-grow: 1;
    height: 100vh;
    background-repeat: no-repeat;
    background-size: cover;
    background-image: url('../assets/images/registration/gwent-game.png');
}

@media screen and (max-height: 905px) {
    .error {
        margin-top: 50px;
    }

    .sign-up {
        margin-top: 30px;
    }
}

@media screen and (max-height: 870px) {
    .sign-up {
        margin-top: 0px;
    }
    
    .error {
        margin-top: 10px;
    }

    .inputs {
        margin-top: 10px;
    }
}

@media screen and (max-height: 770px) {
    .inputs {
        gap: 4px;
    }

    .logo {
        width: 300px;
        height: 120px;
    }

    .sign-up {
        margin-top: 30px;
        width: 200px;
        height: 56px;
    }
 
    .sign-up :deep(a) {
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
