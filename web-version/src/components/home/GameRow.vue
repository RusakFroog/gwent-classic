<script>
import Button from '../ui/Button.vue';
import CustomInput from '../ui/CustomInput.vue';
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
    <li class="room-list-item">
        <h1 class="room-id">{{ id }}</h1>
        <h1 class="room-owner">{{ owner }}</h1>
        <h1 class="room-name">{{ roomName }}</h1>
        <h1 class="room-password">{{ password ? "YES" : "NO" }}</h1>
        <Button @click="showJoinRoom()" class="button-join" text="JOIN" />

        <section v-show="joinIsVisible" @click="closeModal" class="join-modal-overlay">
            <section @click.stop class="join-modal-content">
                <h1 class="join-modal-title">JOIN TO ROOM</h1>
                <CustomInput class="custom-input" placeholder="PASSWORD" ref="password_input" />
                <Button class="button" text="JOIN" @click="joinToRoom()" />
            </section>
        </section>
    </li>
</template>

<style scoped lang="scss">
@import '../../utilities/variables.scss';

.room-list-item {
    display: flex;
    justify-content: center;
    align-items: center;
    outline: 2px solid $gray;
    margin-bottom: 8px;
    height: 50px;
}

.room-id,
.room-owner,
.room-name,
.room-password {
    color: $bronze;
    font-size: 2rem;
}

.room-id {
    margin-left: 8px;
    width: 4.5%;
}

.room-owner,
.room-name {
    width: 30.8%;
}

.room-password {
    width: 3.7%;
}

.button-join {
    margin-left: auto;
    margin-right: 8px;
    width: 130px;
    height: 38px;
    font-size: 26px;
    margin-top: 2px;
    margin-bottom: 0;
}

.join-modal-overlay {
    display: flex;
    justify-content: center;
    align-items: center;
    position: absolute;
    margin-top: 20%;
    width: 77vw;
    height: calc(100vh - 90px);
}

.join-modal-content {
    display: flex;
    flex-direction: column;
    justify-content: center;
    background-color: $black-light;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    outline: 1px solid $bronze;
    width: 20vw;
    height: calc(60vh - 90px);
}

.join-modal-title {
    color: $bronze;
    margin-top: 10px;
    margin-bottom: 20px;
    font-size: 3rem;
    text-align: center;
    font-family: 'Witcher Alternative', sans-serif;
}
</style>