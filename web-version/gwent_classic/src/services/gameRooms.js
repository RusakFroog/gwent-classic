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
    connection.on("Client.UserJoined", function (userId) {
        console.log("New user joined: " + userId);
    });
}

export const createRoom = async () => {
    const connection = await createConnection();

    const roomId = uuidv4();

    await connection.invoke("CreateGame", roomId);

    return roomId;
}

export const joinRoom = async (roomId) => {
    const connection = await createConnection();

    await connection.invoke("JoinToGame", roomId);

    return connection;
}