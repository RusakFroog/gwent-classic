<script>
export default {
    name: "Notification",

    props: {
        text: {
            type: String,
            required: false,
            default: ""
        },

        type: {
            type: String,
            required: false,
            default: "info"
        },

        onCloseClick: {
            type: Function,
            required: true
        }
    },

    data() {
        return {
            active: false
        }
    },

    computed: {
        getTypeImage() {
            return `/${this.$props.type}.svg`;
        }
    },

    methods: {
        close() {
            this.active = false;

            this.onCloseClick();
        }
    },

    watch: {
        text() {
            if (this.text !== undefined && this.text !== "") {
                this.active = true;
            } else {
                this.active = false;
            }
        }
    }
}
</script>

<template>
<div class="main-container" v-if="active">
    <div class="type">
        <img class="image" :src="getTypeImage" /> 
        <div class="text">
            {{ type.toUpperCase() }}
        </div>
        <div class="cross" @click="close" />
    </div>
    <div :class="'message-text ' + type">
        {{ text }}
    </div>
</div>
</template>

<style scoped lang="scss">
@import '../../utilities/variables.scss';

$error-text-color: red;
$warn-text-color: orange;
$info-text-color: $bronze;

.main-container {
    position: absolute;
    display: flex;
    flex-direction: column;
    min-height: 80px;
    max-height: 190px;
    width: 250px;
    
    right: 40px;
    bottom: 20px;
    
    background-color: $black-light;
    border: 1px solid rgb(117, 115, 115);
    
    animation: popup 0.2s;
    z-index: 100;
    transition: 0.1s;

    &:hover {
        border: 1px solid rgb(170, 168, 168);
    }
    
    .message-text {
        font-size: 1.1rem;
        margin-top: 5px;
        margin-left: 10px;
        padding-right: 10px;    
        
        &.info {
            color: $info-text-color;
        }

        &.warn {
            color: $warn-text-color;
        }

        &.error {
            color: $error-text-color;
        }
    }
}

@keyframes popup {
    0% {
        transform: scale(0.8);
        opacity: 0.5;
    }

    100% {
        transform: scale(1);
        opacity: 1;
    }
}

.type {
    display: flex;

    .image {
        width: 25px;
        height: 25px;
        margin-top: 5px;
        margin-left: 5px;
    }

    .cross {
        width: 25px;
        height: 25px;
        margin-left: auto;
        margin-right: 5px;
        margin-top: 5px;

        background-repeat: no-repeat;
        background-size: contain;
        background-image: url('/cross.svg');
        transition: 0.3s;

        &:hover {
            cursor: pointer;
            background-image: url('/cross-hover.svg');
        }
    }

    .text {
        color: $white;
        font-size: 1.2rem;

        margin-left: 5px;
        margin-top: 2px;
    }
}

</style>