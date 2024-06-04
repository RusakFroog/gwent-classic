<script>
export default {
    props: {
        text: {
            type: String,
            required: true
        }
        width: {
            type: String,
            default: '320px'
        },
        height: {
            type: String,
            default: '100px'
        }
    },

    computed: {
        getStyles() {
            return {
                width: this.width,
                height: this.height
            }
        }
    }
};
</script>

<template>
    <div :style="getStyles">
        <p>{{ text }}</p>
    </div>
</template>