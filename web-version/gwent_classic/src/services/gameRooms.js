import { v4 as uuidv4 } from "uuid";
import { HTTP_SERVER } from './data/constants.js'; 

export const createRoom = async (roomName, password) => {
    const roomId = uuidv4();

    const response = await fetch(`${HTTP_SERVER}/rooms/create`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: {
            id: roomId,
            name: roomName,
            password: password
        },
        credentials: 'include'
    });

    if (!response.ok) {
        alert(await response.text());

        return false;
    }
    
    localStorage.setItem('room_id', roomId);

    return true;
}

export const joinRoom = async (roomId, password) => {
    const response = await fetch(`${HTTP_SERVER}/rooms/join/${roomId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: {
            password: password
        },
        credentials: 'include'
    });

    if (!response.ok) {
        alert(await response.text());
        
        return false;
    }

    localStorage.setItem('room_id', roomId);
    
    return true;
}

export const setReady = async (roomId, state) => {
    const response = await fetch(`${HTTP_SERVER}/rooms/setready/${roomId}/?state=${state}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: {
            password: password
        },
        credentials: 'include'
    });

    if (!response.ok)
        alert(await response.text());
        
    return response.ok;
}

export const getRooms = async (loaded, needLoad) => {
    const response = await fetch(`${HTTP_SERVER}/rooms/getrooms/?LoadedCount=${loaded}&NeedLoad=${needLoad}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        },
        credentials: 'include'
    });

    const responseData = await response.json();

    return responseData.rooms;
}