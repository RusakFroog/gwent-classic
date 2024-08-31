<script>
export default {
    props: {
        placeholder: {
            type: String,
            default: ''
        },

        onFocus: {
            type: Function
        },

        type: {
            type: String,
            default: 'text'
        },

        valueInput: {
            type: String,
            default: ''
        },

        autocomplete: {
            type: String,
            default: 'off'
        }
    },

    methods: {
        focusInput() {
            this.$refs.input.focus();
        }
    },

    data() {
        return {
            active: false,
            inputValue: this.$props.valueInput.toString()
        };
    }
};
</script>

<template>
    <input 
        class="input-field" 
        ref="input" 
        :placeholder="placeholder" 
        :type="type" 
        :autocomplete="autocomplete"
        @focus="onFocus" 
        v-model="inputValue" 
    /> 
</template>

<style scoped lang="scss">
@import "../../utilities/variables.scss";

.input-field {
    color: $white-dark;
    width: 400px;
    height: 88px;
    
    background: url('../../assets/inputs/input.svg');
    background-repeat: no-repeat;
    background-size: contain;
    outline: none;
    
    font-size: 34px;
    text-align: left;
    padding-left: 30px;
    padding-right: 30px;
    transition: 0.3s;

    &:focus {
        background-image: url('../../assets/inputs/selected-input.svg');
    }

    &::placeholder {
        color: $gray;
    }

    &:-webkit-autofill {
        border: 1px solid $bronze;
        -webkit-text-fill-color: $black-light;
        -webkit-box-shadow: 30px 0 0 60px $gray inset;
        -webkit-box-sizing: 2px;
    }

    &:-webkit-autofill:focus {
        border: 1px solid $bronze;
        -webkit-text-fill-color: $black-light;
        -webkit-box-shadow: 0 0 0px 1000px $white-dark inset;
        transition: background-color 0.3s ease-in-out 0s;
    }
}
</style>
