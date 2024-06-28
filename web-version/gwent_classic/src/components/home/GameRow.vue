<script>
import Button from '../Button.vue';
import CustomInput from '../CustomInput.vue';
import { joinRoom } from '../../services/gameRooms.js';

export default {
    components: {
        Button,
        CustomInput,
    },

    props: {
        id: {
            type: Number,
            required: true
        },

        owner: {
            type: String,
            required: true
        },
        
        roomName: {
            type: String,
            required: true
        },

        uuid: {
            type: String,
            required: true
        },
        
        password: {
            type: Boolean,
            required: true
        }
    },

    data() {
        return {
            joinIsVisible: false,

            fields: {
                roomPassword: null
            }
        }
    },

    methods: {
        async joinToRoom() {
            const roomPassword = this.fields.roomPassword.inputValue ?? "";

            const room = await joinRoom(this.$props.uuid, roomPassword);            

            if (!room)
                return;

            this.closeModal();
        },

        async showJoinRoom() {
            if (this.$props.password == false)
                return await this.joinToRoom();

            this.joinIsVisible = true;
        },

        closeModal() {
            this.joinIsVisible = false;
        }
    },

    mounted() {
        this.fields.roomPassword = this.$refs['password_input'];
    }
}
</script>

<template>
    <li class="flex outline outline-2 outline-[#AA9D9D] items-center mb-2 h-[50px]">
        <h1 class="text-[#D2B47C] text-2xl ml-2 w-[4.5%]">{{ id }}</h1>
        <h1 class="text-[#D2B47C] text-2xl w-[30.8%]">{{ owner }}</h1>
        <h1 class="text-[#D2B47C] text-2xl w-[30.8%]">{{ roomName }}</h1>
        <h1 class="text-[#D2B47C] text-2xl w-[3.7%]">{{ password ? "YES" : "NO" }}</h1>
        <Button @click="showJoinRoom()" class="button-join" text="JOIN" />

        <section v-show="joinIsVisible" @click="closeModal" class="flex absolute justify-center items-center mt-[20%] w-[77vw] h-[calc(100vh-90px)]">
            <section @click.stop class="flex items-center flex-col bg-[#151515] drop-shadow-xl outline outline-1 outline-[#D2B47C] w-[20vw] h-[calc(60vh-90px)]">
                <h1 class="text-[#D2B47C] mt-10 mx-auto text-center text-5xl font-witcher-alternative mb-20">JOIN TO ROOM</h1>
                <CustomInput class="custom-input" placeholder="PASSWORD" ref="password_input" />
                <Button class="button" text="JOIN" @click="joinToRoom()" />
            </section>
        </section>
    </li>
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

.button-join {
    margin-left: auto;
    margin-right: 8px;
    width: 130px;
    height: 38px;
    margin-top: 2px;
}

.button-join :deep(a) {
    font-size: 26px;
    margin-bottom: 0px;
}
</style>