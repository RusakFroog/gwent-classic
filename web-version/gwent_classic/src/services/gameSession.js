import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";

class GameSession {
    #connection;

    /**
     * 
     * @param {import("@microsoft/signalr").HubConnection} connection 
     */
    constructor(connection) {
        this.#connection = connection;

        this.roomId = localStorage.getItem('room_id');
    }

    setReady() {
        this.#connection.invoke("SetReady");
    }
}



// /**
//  * 
//  * @returns {HubConnection}
//  */
// const createConnection = async () => {
//     const connection = new HubConnectionBuilder()
//         .withUrl("http://localhost:5187/hub/room")
//         .build();

//     configureConnection(connection);

//     await connection.start();

//     return connection;
// }

// /**
//  * @param {HubConnection} connection
//  */
// const configureConnection = (connection) => {
//     connection.on("Client.JoinUser", (userId) => {
//         console.log("User joined: " + userId);
//     });

//     connection.on("Client.SendError", (error) => {
//         alert(error);

//         console.error(error);
//     });
// }
