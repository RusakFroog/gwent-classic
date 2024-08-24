<script>
import Button from '../components/Button.vue';
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
            this.creatingRoom.active = !this.creatingRoom.active;
        },

        showJoiningRoom() {
            this.joiningRoom.active = !this.joiningRoom.active;
        },

        async loadRooms() {
            if (this.isLoading) 
                return;
        
            this.isLoading = true;
            
            const rooms = await getRooms(this.offset, 20);

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

                this.offset += rooms.length;
            }

            this.isLoading = false;
        },
    },

    async mounted() {
        await this.loadRooms();
    },
    
    data() {
        return {
            creatingRoom: {
                active: false,
            },

            joiningRoom: {
                active: false
            },

            rooms: [],
            offset: 0,
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
    <section class="flex h-[calc(100vh-90px)] w-screen justify-start bg-cover bg-no-repeat bg-[url('./assets/images/registration/background.png')]">
        <section class="flex flex-col items-center bg-[#0D0D0D] bg-opacity-90 w-[77vw] h-full">
            <!-- TABLE -->
            <div class="flex flex-col justify-center outline outline-2 outline-[#D2B47C] mt-10 w-[70vw] h-[71vh] bg-[#000000] bg-opacity-65">
                
                <!-- HEADER -->
                <div class="inicials-top" className="flex items-center mt-2 mx-2 w-[calc(100%-18px)] h-[8%] outline outline-2 outline-[#AA9D9D]">
                    <h1 class="text-[#D2B47C] text-3xl ml-2 w-[4.5%]">#</h1>
                    <h1 class="text-[#D2B47C] text-3xl w-[30.8%]">OWNER</h1>
                    <h1 class="text-[#D2B47C] text-3xl w-[30.8%]">ROOM</h1>
                    <h1 class="text-[#D2B47C] text-3xl w-[3.7%]">PASSWORD</h1>
                </div>

                <!-- DIVIDER -->
                <div class="outline outline-1 outline-[#D2B47C] w-[calc(100%-18px)] m-auto my-4" />

                <!-- GAME ROWS -->
                <ul>
                    <GameRow v-for="room in getRooms" 
                        :id="room.id"
                        :owner="room.owner"
                        :password="room.password"
                        :roomName="room.name"
                        :uuid="room.uuid"
                    />
                    <Button v-if="!isLoading" @click="loadRooms()" class="button-custom-load" text="LOAD MORE" />
                    <Button v-else class="button-custom-load" text="LOADING..." />
                </ul>
            </div>
            <!-- END TABLE -->
            
            <!-- BUTTONS -->
            <section class="flex items-center gap-5 m-20">
                <Button class="button-custom" @click="showCreatingRoom()" text="CREATE ROOM" />
                <Button class="button-custom" @click="showJoiningRoom()" text="JOIN TO ROOM" />
            </section>

            <!-- CREATING ROOM -->
            <CreatingRoom v-model:active="creatingRoom.active" />

            <!-- JOINING ROOM -->
            <JoiningRoom v-model:active="joiningRoom.active" />
        </section>
    </section>
</template>

<style scoped>
* {
    font-family: "Witcher";
}

.button-custom {
    width: 280px;
    height: 80px;
}

.button-custom-load {
    margin-top: 30px;
    margin-bottom: 10px;
    width: 200px;
    height: 60px;
    margin-inline: auto;
    font-size: 26px;
}

ul {
    -ms-overflow-style: none;
    scrollbar-width: none;
    width: calc(100% - 16px);
    margin-left: 8px;
    height: calc(71vh - 12%);
    padding-right: 2px;
    padding-left: 2px;
    padding-top: 2px;
    overflow-y: auto;
}
</style>