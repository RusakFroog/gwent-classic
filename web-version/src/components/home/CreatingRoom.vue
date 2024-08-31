<script>
import { createRoom } from '../../services/gameRooms.js';
import Button from '../ui/Button.vue';
import CustomInput from '../ui/CustomInput.vue';

export default {
    components: {
        CustomInput,
        Button
    },

    props: {
        active: {
            type: Boolean,
            default: false
        }
    },

    data() {
        return {
            fields: {
                roomName: null,
                roomPassword: null
            }
        }
    },
    
    methods: {
        async createNewRoom() {
            const roomName = this.fields.roomName.inputValue;
            const roomPassword = this.fields.roomPassword.inputValue;

            const roomId = await createRoom(roomName, roomPassword);

            if (roomId) {
                this.closeModal();
                
                localStorage.setItem('room_id', roomId);
                
                alert("Room created: " + roomId);
            }
        },

        closeModal() {
            this.$emit('update:active', false);
        },
    },

    mounted() {
        this.fields.roomName = this.$refs['room_name_input'];
        this.fields.roomPassword = this.$refs['password_input'];
    }
};
</script>

<template>
    <section v-show="active" @click="closeModal" class="modal-overlay">
        <section @click.stop class="modal-content">
            <h1 class="modal-title">CREATE ROOM</h1>
            <CustomInput class="custom-input" placeholder="ROOM NAME" ref="room_name_input" />
            <CustomInput class="custom-input" placeholder="PASSWORD" ref="password_input" />
            <Button class="button" text="CREATE" @click="createNewRoom()" />
        </section>
    </section>
</template>

<style scoped lang="scss">
@import '../../utilities/variables.scss';

.modal-overlay {
    display: flex;
    position: absolute;
    justify-content: center;
    align-items: center;
    width: 77vw;
    height: calc(100vh - 90px);
    background-color: $black-light;
    opacity: 0.9;
}

.modal-content {
    display: flex;
    flex-direction: column;
    align-items: center;
    background-color: $black-light;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    outline: 1px solid $bronze;
    width: 20vw;
    height: 30vh;
}

.modal-title {
    color: $bronze;
    margin-top: 10px;
    margin-bottom: 20px;
    font-size: 3rem;
    text-align: center;
    font-family: 'Witcher Alternative', sans-serif;
}

.custom-input {
    margin-bottom: 20px;
    width: 280px;
    height: 60px;
    padding-right: 18px;
    padding-left: 15px;
    font-size: 22px;
}

.button {
    width: 250px;
    height: 70px;
    margin-top: 20px;
}
</style>
