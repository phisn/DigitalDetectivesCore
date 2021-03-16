import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class GameHubServiceService {
  private gameHub: HubConnection;

  constructor() {
  }

  public async connect() {
    this.gameHub = new HubConnectionBuilder()
      .withUrl("/gamehub")
      .build();
  }
}
