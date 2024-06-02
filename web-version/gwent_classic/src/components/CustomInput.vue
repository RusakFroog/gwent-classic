<script>
export default {
    props: {
        placeholder: {
            type: String,
            default: ''
        },

        width: {
            type: String,
            default: '400px'
        },

        height: {
            type: String,
            default: '88px'
        }
    },

    data() {
        return {
            active: false,
            inputValue: ''
        };
    }
};
</script>

<template>
    <div class="custom-input">
        <input :placeholder="placeholder" class="input-field" v-bind:value="inputValue" />
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
    width: 400px;
    height: 88px;
    padding-right: 30px;
    background: url(../assets/images/registration/input.svg);
    background-repeat: no-repeat;    
    background-size: contain;
    outline: none;
    font-size: 40px;
    text-align: left;
    padding-left: 30px;
}

.input-field::placeholder {
    color: #8D8D8D;
}
</style>
