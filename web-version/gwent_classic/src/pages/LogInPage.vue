<script>
import Button from '../components/Button.vue';
import CustomInput from '../components/CustomInput.vue';
import router from '../router/index.js';
import { loginToAccount } from '../services/accounts.js';

export default {
    data() {
        return {
            activeInput: 0,
            inputs: [
                { placeholder: 'LOGIN', type: 'text' },
                { placeholder: 'PASSWORD', type: 'password' },
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

        login() {
            this.hideError();
            
            const login = this.componentInputs[0].inputValue;
            const password = this.componentInputs[1].inputValue;

            if (login.length < 5 || password < 6 || login.includes(" ") || password.includes(" "))
                return this.showError("ERROR: Login or Password are invalid");

            setTimeout(async () => {
                const result = await loginToAccount(login, password);

                if (result.error != null)
                    return this.showError("ERROR: " + result.error);

                this.goToPage('/home');

                sessionStorage.setItem('new_session', 'true');
            }, 500);
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
    <section class="login-section">
        <div class="panel">
            <h1>Log in to account</h1>
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
                <Button class="log-in" @click="login()" text="LOG IN" />
            </div>
            <div class="error" v-if="error.active">
                <img class="error-image" src="../assets/images/info.svg" />
                <text>{{ error.text }}</text>
            </div>
            <p>Don't have an account? 
                <a @click="goToPage('/register')" class="signup-link">SIGN UP</a>
            </p>
        </div>
        <div class="image"></div>
    </section>
</template>

<style scoped lang="scss">
* {
    font-family: "Witcher";
}

.login-section {
    display: flex;
    align-items: start;
    height: calc(100vh - 90px);
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
    height: 100%;
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

        .signup-link {
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
    margin-top: 120px;
}

.log-in {
    margin-top: 80px;
}

.image {
    flex-grow: 1;
    background-image: url('../assets/images/registration/gwent-game.png');
    background-repeat: no-repeat;
    background-size: cover;
}

.error {
    display: flex;
    justify-content: flex-start;
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

@media screen and (max-height: 1020px) {
    .log-in {
        margin-top: 0px;
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

    .custom-input {
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