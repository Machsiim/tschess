import process from "node:process";
import * as SignalR from "@microsoft/signalr";

/**
 * SignalRService
 * A facade for the server/client communication over the SignalR protocol.
 */
class SignalRService {
  constructor() {
    this.connected = false;
  }
  async connectWithToken(token) {
    const host =
      process.env.NODE_ENV == "production"
        ? "/chessHub"
        : "https://localhost:5001/chessHub";
    // See https://learn.microsoft.com/en-us/javascript/api/@microsoft/signalr/hubconnectionbuilder?view=signalr-js-latest
    this.connection = new SignalR.HubConnectionBuilder()
      .withUrl(`${host}?token=${token}`)
      .configureLogging(SignalR.LogLevel.Information)
      .build();
    // Reconnect after server has closed the connection.
    this.connection.onclose(async () => {
      await this.connection.start();
    });
    await this.connection.start();
    this.connected = true;
  }
  subscribeEvent(type, callback) {
    this.connection.on(type, callback);
  }
  unsubscribeEvent(type, callback) {
    if (typeof callback === "undefined") this.connection.off(type);
    else this.connection.off(type, callback);
  }

  async enterWaitingroom() {
    if (!this.connected) {
      throw new Error("Invalid state. Not connected.");
    }
    await this.connection.invoke("EnterWaitingroom");
  }

  async challenge(playername) {
    if (!this.connected) {
      throw new Error("Invalid state. Not connected.");
    }
    await this.connection.invoke("ChallengeUser", playername);
  }

  async sendMessage(message) {
    if (!this.connected) {
      throw new Error("Invalid state. Not connected.");
    }
    // SendMessage is corresponding to the C# Method in ChessHub.
    await this.connection.invoke("SendMessage", message);
  }

  async leaveWaitingroom() {
    if (!this.connected) {
      throw new Error("Invalid state. Not connected.");
    }
    await this.connection.invoke("LeaveWaitingroom");
  }

  async startGame(player2) {
    if (!this.connected) {
      throw new Error("Invalid state. Not connected.");
    }
    await this.connection.invoke("StartGame", player2);
  }

  async getGameState(gameGuid) {
    if (!this.connected) {
      throw new Error("Invalid state. Not connected.");
    }
    return await this.connection.invoke("GetGameState", gameGuid);
  }

  async setGameState(gameGuid, gameState) {
    if (!this.connected) {
      throw new Error("Invalid state. Not connected.");
    }
    this.connection.invoke("SetGameState", gameGuid, gameState);
  }

  async getColor(gameGuid, username) {
    if (!this.connected) {
      throw new Error("Invalid state. Not connected.");
    }
    return await this.connection.invoke("GetColor", gameGuid, username);
  }
}

// Export a singleton (only 1 instance in the spa to make state management easier)
const signalRSerivce = new SignalRService();
export default signalRSerivce;
