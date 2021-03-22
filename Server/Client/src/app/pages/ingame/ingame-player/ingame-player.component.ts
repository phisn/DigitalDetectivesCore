import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { AlertController, IonSelect, LoadingController, ModalController } from '@ionic/angular';
import { HubConnection } from '@microsoft/signalr';
import { IngameState } from 'src/app/models/ingame-state';
import { StateType } from 'src/app/models/state-type.enum';
import { TicketType } from 'src/app/models/game/ticket-type.enum';
import { IngameHubService } from 'src/app/services/ingame-hub/ingame-hub.service';
import { RouteOption } from 'src/app/models/route-option';
import { PlayerTicketInfoComponent } from '../player-ticket-info/player-ticket-info.component';

@Component({
  selector: 'app-ingame-player',
  templateUrl: './ingame-player.component.html',
  styleUrls: ['./ingame-player.component.scss'],
})
export class IngamePlayerComponent implements OnInit {
  TicketType = TicketType;

  @ViewChild("yellowSelect") private yellowSelect: IonSelect;
  @ViewChild("greenSelect") private greenSelect: IonSelect;
  @ViewChild("redSelect") private redSelect: IonSelect;
  @ViewChild("blackSelect") private blackSelect: IonSelect;

  public model: IngameState;
  
  constructor(
    private alertController: AlertController,
    private loadingController: LoadingController,
    private modalController: ModalController,
    private ingameHubService: IngameHubService) {
  }

  ngOnInit() {
    this.ingameHubService.state.subscribe(state => {
      this.model = state;
    });
  }

  public getModel() {
    return this.model;
  }

  public isDetective() {
    return this.model.type == StateType.Detective;
  }
  
  public isVillian() {
    return this.model.type == StateType.Villian;
  }

  public async chooseRoute(ticket: TicketType) {
    let selected = this.getSelectorFor(ticket);

    let position = selected.value;

    selected.selectedText = "";
    selected.value = 0;

    let loader = await this.loadingController.create({
       message: "Submitting move ..." 
    });

    await loader.present();
    
    try {
      await this.ingameHubService.makeTurn(position, ticket, false);
    }
    catch (error) {
      let alert = await this.alertController.create({
        message: "Failed to make turn",
        buttons: [
          { text: "OK" }
        ]
      });

      await this.loadingController.dismiss();
      await alert.present();
    }
  }

  public getRoutesFor(type: TicketType): RouteOption[] {
    return this.model.state.routes.filter(value => {
      return value.type == type;
    });
  }

  public async showTicketsModal() {
    const modal = await this.modalController.create({
      component: PlayerTicketInfoComponent,
      componentProps: {
        "model": this.model
      }
    });

    await modal.present();
  }

  private getSelectorFor(type: TicketType): IonSelect {
    switch (type) {
      case TicketType.Yellow: return this.yellowSelect;
      case TicketType.Green: return this.greenSelect;
      case TicketType.Red: return this.redSelect;
      case TicketType.Black: return this.blackSelect;
    }
  }
}
