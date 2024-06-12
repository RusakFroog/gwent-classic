import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { v4 as uuidv4 } from "uuid";

/**
 * 
 * @returns {HubConnection}
 */
const createConnection = async () => {
    const connection = new HubConnectionBuilder()
        .withUrl("http://localhost:5187/hub/game")
        .build();

    configureConnection(connection);

    await connection.start();

    return connection;
}

/**
 * @param {HubConnection} connection
 */
const configureConnection = (connection) => {
    connection.on("Client.JoinUser", (userId) => {
        console.log("User joined: " + userId);
    });

    connection.on("Client.SendError", (error) => {
        alert(error);

        console.error(error);
    });
}

export const createRoom = async (roomName, password) => {
    const connection = await createConnection();

    const roomId = uuidv4();
    
    const result = await connection.invoke("CreateRoom", roomId, roomName, password);

    return result ? roomId : null;
}

export const joinRoom = async (roomId, password) => {
    const connection = await createConnection();

    const result = await connection.invoke("JoinToRoom", roomId, password);

    return result ? connection : null;
}