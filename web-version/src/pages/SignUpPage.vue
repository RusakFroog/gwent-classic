<script>
import Button from '../components/ui/Button.vue';
import CustomInput from '../components/ui/CustomInput.vue';
import Notification from '../components/ui/Notification.vue';
import router from '../router/index.js';
import { createAccount } from '../services/accounts.js';

export default {
    data() {
        return {
            activeInput: 0,
            inputs: [
                { placeholder: 'LOGIN', type: 'text', autocomplete: 'username' },
                { placeholder: 'EMAIL', type: 'email', autocomplete: 'email' },
                { placeholder: 'PASSWORD', type: 'password', autocomplete: 'password' },
                { placeholder: 'REPEAT PASSWORD', type: 'password', autocomplete: 'off' },
            ],

            /** @type {CustomInput[]} */
            componentInputs: [],

            error: {
                text: ""
            }
        }
    },

    components: {
        Button,
        CustomInput,
        Notification
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
        },

        hideError() {
            this.error.text = '';
        },

        signUp() {
            this.hideError();

            const login = this.componentInputs[0].inputValue;
            const email = this.componentInputs[1].inputValue;
            const password = this.componentInputs[2].inputValue;
            const repeatPassword = this.componentInputs[3].inputValue;

            if (password !== repeatPassword) {
                return this.showError('Passwords do not match');
            }
            
            setTimeout(async () => {
                const result = await createAccount(login, email, password);
                
                if (result.error != null) {
                    return this.showError(result.error);
                }
                
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
    <Notification :text="error.text" :onCloseClick="() => this.error.text = undefined" type="error" />

    <section class="registration-section">
        <div class="panel">
            <h1>Create account</h1>
            <img class="logo" src="/logo.png" />
            <div class="inputs">
                <CustomInput 
                    v-for="(input, index) in inputs" 
                    class="custom-input" 
                    @keyup.enter="nextInput"
                    :onFocus="() => setActiveInput(index)" 
                    :ref="'custom_input' + index" 
                    :type="input.type"
                    :placeholder="input.placeholder" 
                    :autocomplete="input.autocomplete"
                />
                <Button class="sign-up" @click="signUp()" text="SIGN UP" />
            </div>
            <div class="error" v-if="error.active">
                <img class="error-image" src="/info.svg" />
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
@import '../utilities/variables.scss';

.registration-section {
    display: flex;
    align-items: flex-start;
    height: 100vh;
    width: 100vw;
    background-color: $black-light;
    background-image: url('../assets/images/background.png');
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
        font-family: 'Witcher Alternative';
        font-size: 1.875rem;
        color: $bronze;
        margin-top: 24px;
    }

    .logo {
        margin-top: 32px;
        user-select: none;
    }

    p {
        color: $gray;
        font-size: 1.5rem;
        margin-top: 40px;
        user-select: none;

        .login-link {
            color: $bronze;
            cursor: pointer;

            &:hover {
                color: $bronze-light;
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
    background-image: url('../assets/images/gwent-game.png');
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
