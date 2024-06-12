<script>
import Button from '../components/Button.vue';
import GameRow from '../components/home/GameRow.vue';
import { createRoom, joinRoom } from '../services/gameRooms.js';

export default {
    name: "HomePage",
    components: {
        Button,
        GameRow
    },

    methods: {
        async createNewRoom() {
            const roomId = await createRoom();

            this.rooms.push(roomId);
        
            alert("Room created: " + roomId);
        },

        async joinToRoom(roomId) {
            try {
                const room = await joinRoom(roomId);
            } catch (error) {
                alert(error);
            }
        }
    },

    data() {
        return {
            rooms: [
                {
                    id: 1,
                    owner: "OWNER_NIGGER",
                    password: true,
                    name: "SOSI JAJCA"
                },
            ]
        }
    }
}
</script>

<template>
    <section class="flex h-[calc(100vh-90px)] w-screen justify-start bg-cover bg-no-repeat bg-[url('./assets/images/registration/background.png')]">
        <section class="flex justify-center bg-[#0D0D0D] bg-opacity-90 w-[77vw] h-full">
            <!-- TABLE -->
            <div class="flex flex-col justify-center outline outline-2 outline-[#D2B47C] mt-10 w-[70vw] h-[71vh] bg-[#000000] bg-opacity-65">
                
                <!-- HEADER -->
                <!-- <div class="inicials-top" className="flex items-center mt-2 mx-2 w-[calc(100%-18px)] h-[8%] outline outline-2 outline-[#AA9D9D]">
                    <h1 class="text-[#D2B47C] text-3xl ml-2 w-[60px]">#</h1>
                    <h1 class="text-[#D2B47C] text-3xl w-[400px]">OWNER</h1>
                    <h1 class="text-[#D2B47C] text-3xl w-[400px]">ROOM</h1>
                    <h1 class="text-[#D2B47C] text-3xl w-[50px]">PASSWORD</h1>
                </div> -->

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
                    <GameRow v-for="room in rooms" 
                        :id="room.id"
                        :owner="room.owner"
                        :password="room.password"
                        :roomName="room.name"
                    />
                </ul>
            </div>
            <!-- TABLE -->
            <section class="flex items-center">
                <!-- <Button class="button-custom" @click="createNewRoom()" text="CREATE" />
                <Button class="button-custom" @click="joinToRoom(rooms[0])" text="JOIN" /> -->
            </section>
        </section>
    </section>
</template>

<style scoped>
* {
    font-family: "Witcher";
}

.button-custom {
    width: 190px;
    height: 55px;
}

ul {
    -ms-overflow-style: none;
    scrollbar-width: none;
    width: calc(100% - 16px);
    margin-left: 8px;
    height: 92%;
    padding-right: 2px;
    padding-left: 2px;
    padding-top: 2px;
    overflow-y: auto;
}
</style>