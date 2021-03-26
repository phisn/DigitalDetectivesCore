import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, HubConnectionState, LogLevel } from '@microsoft/signalr';
import { BehaviorSubject, Subject } from 'rxjs';
import { StateType } from '../../models/state-type.enum';
import { TicketType } from '../../models/game/ticket-type.enum';
import { IngameState } from 'src/app/models/ingame-state';
import { PlayerColor } from 'src/app/models/game/player-color.enum';
import { DetectiveState } from 'src/app/models/detective-state';
import { VillianState } from 'src/app/models/villian-state';
import { MatchInfo } from '../../models/match-info';

@Injectable({
  providedIn: 'root'
})
export class IngameHubService {
  public onclose: Subject<Error> = new Subject();
  public matchInfo: BehaviorSubject<MatchInfo> = new BehaviorSubject(null);
  public state: BehaviorSubject<IngameState> = new BehaviorSubject(null);

  constructor() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl("/ingamehub")
      .configureLogging(LogLevel.Warning)
      .build();
    
    this.hubConnection.onclose(error => {
      this.matchInfo.next(null);
      this.state.next(null);
      this.onclose.next(error);
    });

    this.hubConnection.on("UpdateStateVillian", (state: VillianState) => {
      this.state.next(new IngameState(
        StateType.Detective,
        state
      ));
    });

    this.hubConnection.on("UpdateStateDetective", (state: DetectiveState) => {
      this.state.next(new IngameState(
        StateType.Villian,
        state
      ));
    });

    this.hubConnection.on("MatchStartedEvent", (match: MatchInfo) => {
      this.matchInfo.next(match);
    });

    this.hubConnection.on("NewRoundEvent", () => {
      let matchInfo = this.matchInfo.value;
      matchInfo.round += 1;
      this.matchInfo.next(matchInfo);
    });

    this.hubConnection.on("PlayerLeftEvent", (color: PlayerColor) => {
      let matchInfo = this.matchInfo.value;
      matchInfo.availableColors = matchInfo.availableColors.concat([color]);
      this.matchInfo.next(matchInfo);
    });

    this.hubConnection.on("PlayerJoinedEvent", (color: PlayerColor) => {
      let matchInfo = this.matchInfo.value;
      matchInfo.availableColors = matchInfo.availableColors.filter(c => c != color);
      this.matchInfo.next(matchInfo);
    });
  }

  public async connect(): Promise<void> {
    try {
      await this.hubConnection.start();
      await this.synchronizeMatchInfo();
    }
    catch (error) {
      if (this.hubConnection.state != HubConnectionState.Disconnected) {
        await this.hubConnection.stop();
      }

      throw error;
    }
  }

  public async registerPlayer(color: PlayerColor): Promise<void> {
    try {
      return this.hubConnection.invoke("RegisterAsPlayer", color);
    }
    catch (error) {
      console.error(error);
      await this.synchronizeMatchInfo();
      throw error;
    }
  }

  public async makeTurn(position: number, type: TicketType, useDoubleTicket: boolean): Promise<void> {
    try {
      return this.hubConnection.invoke("MakeTurn", position, type, useDoubleTicket);
    }
    catch (error) {
      console.error(error);
      await this.synchronizeState();
      throw error;
    }    
  }

  private async synchronizeMatchInfo() {
    this.matchInfo.next(
      await this.hubConnection.invoke<MatchInfo>("GetMatchInfo"));
  }

  private async synchronizeState() {
    this.state.next(
      await this.hubConnection.invoke<IngameState>("GetState"));
  }

  private hubConnection: HubConnection;
}
