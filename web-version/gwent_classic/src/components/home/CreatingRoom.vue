<script>
import { createRoom } from '../../services/gameRooms.js';
import Button from '../Button.vue';
import CustomInput from '../CustomInput.vue';

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

            if (!roomId)
                return;

            this.closeModal();
            
            alert("Room created: " + roomId);
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
    <section v-show="active" @click="closeModal" class="flex absolute justify-center items-center w-[77vw] h-[calc(100vh-90px)] bg-[#151515] bg-opacity-90">
        <section @click.stop class="flex items-center flex-col bg-[#151515] drop-shadow-xl outline outline-1 outline-[#D2B47C] w-[30%] h-[50%]">
            <h1 class="text-[#D2B47C] mt-10 mx-auto text-center text-5xl font-witcher-alternative mb-20">CREATE ROOM</h1>
            <CustomInput class="custom-input" placeholder="ROOM NAME" ref="room_name_input" />
            <CustomInput class="custom-input" placeholder="PASSWORD" ref="password_input" />
            <Button class="button" text="CREATE" @click="createNewRoom()" />
        </section>
    </section>
</template>

<style scoped>
.custom-input {
    margin-bottom: 20px;
}

.custom-input :deep(.input-field) {
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