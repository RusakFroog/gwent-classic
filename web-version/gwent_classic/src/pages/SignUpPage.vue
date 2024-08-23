<script>
import Button from '../components/Button.vue';
import CustomInput from '../components/CustomInput.vue';
import router from '../router/index.js';
import { createAccount } from '../services/accounts.js';

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
        goToPage(link) {
            if (router.currentRoute.value === link)
                return;

            router.push(link);
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

        signUp() {
            this.hideError();

            const login = this.componentInputs[0].inputValue;
            const email = this.componentInputs[1].inputValue;
            const password = this.componentInputs[2].inputValue;
            const repeatPassword = this.componentInputs[3].inputValue;

            if (password !== repeatPassword)
                return this.showError('ERROR: Passwords do not match');
            
            setTimeout(async () => {
                const result = await createAccount(login, email, password);
                
                if (result.error != null) 
                    return this.showError("ERROR: " + result.error);
                
                this.goToPage('/home');

                sessionStorage.setItem('new_session', 'true');
            }, 300);
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
    <section class="registration-section">
        <div class="panel">
            <h1>Create account</h1>
            <img class="logo" src="../assets/images/registration/logo.png" />
            <div class="inputs">
                <CustomInput 
                    v-for="(input, index) in inputs" 
                    class="custom-input" 
                    @keyup.enter="nextInput"
                    :onFocus="() => setActiveInput(index)" 
                    :ref="'custom_input' + index" 
                    :type="input.type"
                    :placeholder="input.placeholder" 
                />
                <Button class="sign-up" @click="signUp()" text="SIGN UP" />
            </div>
            <div class="error" v-if="error.active">
                <img class="error-image" src="../assets/images/info.svg" />
                <text>{{ error.text }}</text>
            </div>
            <p>Already have an account? 
                <a @click="goToPage('/login')" class="login-link">LOG IN</a>
            </p>
        </div>
        <div class="image"></div>
    </section>
</template>

<style scoped lang="scss">
* {
    font-family: "Witcher";
}

.registration-section {
    display: flex;
    align-items: flex-start;
    height: 100vh;
    width: 100vw;
    background-color: #242424;
    background-image: url('../assets/images/registration/background.png');
    background-size: cover;
    background-repeat: no-repeat;
}

.panel {
    display: flex;
    align-items: center;
    flex-direction: column;
    margin-left: 80px;
    height: 100vh;
    background-color: rgba(13, 13, 13, 0.65);
    padding: 0 56px;
    position: relative;

    h1 {
        font-family: 'WitcherAlternative';
        font-size: 1.875rem;
        color: #D2B47C;
        margin-top: 24px;
    }

    .logo {
        margin-top: 32px;
        user-select: none;
    }

    p {
        color: #8D8D8D;
        font-size: 1.5rem;
        margin-top: 40px;
        user-select: none;

        .login-link {
            color: #D2B47C;
            cursor: pointer;

            &:hover {
                color: #FFB500;
            }
        }
    }
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

.error {
    display: flex;
    justify-content: center;
    align-items: center;
    width: 310px;
    margin-top: 10px;

    .error-image {
        width: 60px;
        height: 60px;
    }

    text {
        color: #AB1E1E;
        padding-left: 10px;
        font-size: 18px;
    }
}

@media screen and (max-height: 950px) {
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

    .sign-up {
        margin-top: 30px;
        width: 200px;
        height: 56px;
    }

    .sign-up :deep(a) {
        font-size: 27px;
    }

    .custom-input {
        width: 300px;
        font-size: 25px;
        padding-bottom: 23px;
        margin-top: -10px;
    }
}

@media screen and (max-height: 770px) {
    .custom-input {
        width: 230px;
        height: 50px;
        font-size: 20px;
        padding-bottom: 0px;
        margin-top: 0px;
    }

    .inputs {
        gap: 16.4px;
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
