<script>
import { createRoom, joinRoom } from '../services/gameRooms.js';

export default {
    name: "RoomsPage",
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
            rooms: []
        }
    }
}
</script>

<template>
    <div class="flex gap-4 bg-[#222222] h-[calc(100vh-90px)] w-screen justify-start">
        <button @click="createNewRoom()">Create</button>
        <button @click="joinToRoom(rooms[0])">Join</button>
    </div>
</template>