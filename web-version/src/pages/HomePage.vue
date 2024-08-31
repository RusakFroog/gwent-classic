<script>
import Button from '../components/ui/Button.vue';
import GameRow from '../components/home/GameRow.vue';
import JoiningRoom from '../components/home/JoiningRoom.vue';
import CreatingRoom from '../components/home/CreatingRoom.vue';
import { getRooms } from '../services/gameRooms.js';

export default {
    name: "HomePage",
    components: {
        Button,
        GameRow,
        JoiningRoom,
        CreatingRoom
    },

    methods: {
        showCreatingRoom() {
            this.modals.creatingRoomActive = !this.modals.creatingRoomActive;
        },

        showJoiningRoom() {
            this.modals.joiningRoomActive = !this.modals.joiningRoomActive;
        },

        async loadRooms() {
            if (this.isLoading) 
                return;
        
            this.isLoading = true;
            
            const rooms = await getRooms(this.loadedItems, 20);

            if (rooms.length > 0) {
                for (const room of rooms) {
                    this.rooms.push({
                        id: this.rooms.length + 1,
                        uuid: room.uuid,
                        owner: room.owner,
                        password: room.password,
                        name: room.name
                    });
                }

                this.loadedItems += rooms.length;
            }

            this.isLoading = false;
        },
    },

    async mounted() {
        await this.loadRooms();
    },
    
    data() {
        return {
            modals: {
                creatingRoomActive: false,
                joiningRoomActive: false 
            },

            rooms: [],
            loadedItems: 0,
            isLoading: false
        }
    },

    computed: {
        getRooms() {
            return this.rooms;
        }
    }
}
</script>

<template>
    <section class="registration-container">
        <section class="registration-content">
            <div class="room-list-container">
                <div class="room-list-header">
                    <h1 class="room-list-header-id">#</h1>
                    <h1 class="room-list-header-owner">OWNER</h1>
                    <h1 class="room-list-header-room">ROOM</h1>
                    <h1 class="room-list-header-password">PASSWORD</h1>
                </div>

                <div class="divider" />

                <ul class="room-list">
                    <GameRow 
                        v-for="room in getRooms" 
                        :id="room.id"
                        :owner="room.owner"
                        :password="room.password"
                        :roomName="room.name"
                        :uuid="room.uuid"
                    />
                    <Button v-if="!isLoading" @click="loadRooms()" class="button-load" text="LOAD MORE" />
                    <Button v-else class="button-load" text="LOADING..." />
                </ul>
            </div>
            
            <section class="action-buttons">
                <Button class="button-custom" @click="showCreatingRoom()" text="CREATE ROOM" />
                <Button class="button-custom" @click="showJoiningRoom()" text="JOIN TO ROOM" />
            </section>

            <CreatingRoom v-model:active="modals.creatingRoomActive" />
            <JoiningRoom v-model:active="modals.joiningRoomActive" />
        </section>
    </section>
</template>

<style scoped lang="scss">
@import "../utilities/variables.scss";

.registration-container {
    display: flex;
    height: calc(100vh - 90px);
    width: 100vw;
    justify-content: flex-start;
    background: url('../assets/images/background.png') no-repeat center center;
    background-size: cover;
}

.registration-content {
    display: flex;
    flex-direction: column;
    align-items: center;
    background-color: rgba(13, 13, 13, 0.9);
    width: 77vw;
    height: 100%;
}

.room-list-container {
    display: flex;
    flex-direction: column;
    justify-content: center;
    margin-top: 10px;
    width: 70vw;
    height: 71vh;
    background-color: rgba(0, 0, 0, 0.65);
    border: 2px solid $bronze;
}

.room-list-header {
    display: flex;
    align-items: center;
    margin-top: 8px;
    margin-left: 8px;
    width: calc(100% - 18px);
    height: 8%;
    border: 2px solid $gray;
}

.room-list-header-id,
.room-list-header-owner,
.room-list-header-room,
.room-list-header-password {
    color: $bronze;
    font-size: 1.875rem;
}

.room-list-header-id {
    margin-left: 8px;
    width: 4.5%;
}

.room-list-header-owner,
.room-list-header-room {
    width: 30.8%;
}

.room-list-header-password {
    width: 3.7%;
}

.divider {
    width: calc(100% - 18px);
    margin: auto;
    margin-top: 16px;
    margin-bottom: 16px;
    border: 1px solid $bronze;
}

.room-list {
    width: calc(100% - 16px);
    margin-left: 8px;
    height: calc(71vh - 12%);
    padding: 2px;
    overflow-y: auto;
    scrollbar-width: none;
    -ms-overflow-style: none;

    &::-webkit-scrollbar {
        display: none;
    }
}

.button-custom {
    width: 280px;
    height: 80px;
}

.button-load {
    margin-top: 30px;
    margin-bottom: 10px;
    width: 200px;
    height: 60px;
    margin-inline: auto;
    font-size: 26px;
}

.action-buttons {
    display: flex;
    align-items: center;
    gap: 20px;
    margin: 20px;
}
</style>
