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
    <div>
        <input ref="input" :placeholder="placeholder" :type="type" @focus="onFocus" class="input-field" v-model="inputValue" /> 
    </div>
</template>

<style scoped>
.input-field:focus {
    transition: 0.3s;
    background-image: url(../assets/images/registration/selected-input.svg);
}

.input-field {
    font-family: "Witcher";
    color: #e6e6e6;
    padding-right: 30px;
    width: 400px;
    height: 88px;
    background: url(../assets/images/registration/input.svg);
    background-repeat: no-repeat;
    background-size: contain;
    outline: none;
    font-size: 34px;
    text-align: left;
    padding-left: 30px;
}

.input-field::placeholder {
    color: #8D8D8D;
}
</style>
