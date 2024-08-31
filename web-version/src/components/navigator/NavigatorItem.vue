<script>
export default {
    props: {
        title: {
            type: String,
            required: true
        },

        active: {
            type: Boolean,
            default: false
        },

        right: {
            type: Boolean,
            default: false
        },
    },

    computed: {
        getFloat() {
            return this.right ? "ml-auto" : "mr-5";
        },

        isActive() {
            return this.active ? "active" : "";
        },

        getValues() {
            return [this.getFloat, this.isActive];
        }
    }
};
</script>

<template>
    <li :class="getValues">
        <a>{{ title }}</a>
    </li>
</template>


<style scoped lang="scss">
@import '../../utilities/variables.scss';

li {
    transition: 0.3s;
    margin-top: 8px;
    
    cursor: pointer;
    background-image: url('../../assets/buttons/frame.svg');
    background-size: contain;
    background-repeat: no-repeat;
    width: 200px;
    height: 56px;

    a {
        display: block;
        color: $bronze;
        text-decoration: none;
        text-align: center;
        font-size: 36px;
        line-height: 40px;
        transition: 0.3s ease;
        margin-top: 8px;
        user-select: none;
    }

    &:hover {
        transition: 0.3s;
        background-image: url('../../assets/buttons/frame-hover.svg');

        a {
            transition: 0.3s;
            color: $black;
        }
    }
}

.active {
    margin-top: 0px;
    background-image: url('../../assets/buttons/selected-frame.svg');
    height: 100px;

    a {
        margin-top: 15px;
        color: $black;
    }

    &:hover {
        margin-top: 0px;
        background-image: url('../../assets/buttons/selected-frame.svg');
    }
}
</style>