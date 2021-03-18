import { Component, OnInit, ViewChild } from '@angular/core';
import { AlertController, IonSelect, LoadingController, ModalController } from '@ionic/angular';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { timer } from 'rxjs';
import { ChoosableRoute } from 'src/app/models/bindings/choosable-route';
import { TurnDetectiveEvent } from 'src/app/models/bindings/turn-detective-event';
import { TurnVillianEvent } from 'src/app/models/bindings/turn-villian-event';
import { TicketType } from 'src/app/models/ticket-type.enum';
import { TurnType } from 'src/app/models/turn-type.enum';
import { PlayerTicketInfoComponent } from './player-ticket-info/player-ticket-info.component';

@Component({
  selector: 'app-ingame',
  templateUrl: './ingame.page.html',
  styleUrls: ['./ingame.page.scss'],
})
export class IngamePage implements OnInit {
  TicketType = TicketType;

  @ViewChild("yellowSelect") private yellowSelect: IonSelect;
  @ViewChild("greenSelect") private greenSelect: IonSelect;
  @ViewChild("redSelect") private redSelect: IonSelect;
  @ViewChild("blackSelect") private blackSelect: IonSelect;

  public test: any;

  constructor(
    private alertController: AlertController,
    private loadingController: LoadingController,
    private modalController: ModalController) {
  }

  public getModel() {
    return this.model;
  }

  public isDetective() {
    return this.modelType == TurnType.Detective;
  }
  
  public isVillian() {
    return this.modelType == TurnType.Villian;
  }

  public getRoutesFor(type: TicketType): ChoosableRoute[] {
    return this.model.routes.filter(value => {
      return value.type == type;
    });
  }

  public async showTicketsModal() {
    const modal = await this.modalController.create({
      component: PlayerTicketInfoComponent,
      componentProps: {
        "model": this.model,
        "modelType": this.modelType
      }
    });

    await modal.present();
  }

  public chooseRoute(ticket: TicketType) {
    let selected = this.getSelectorFor(ticket);    

    console.log(selected.value);

    selected.selectedText = "";
    selected.value = 0;
  }

  private ingameHub: HubConnection;
  private model: TurnDetectiveEvent | TurnVillianEvent;
  private modelType: TurnType;
  
  async ngOnInit() {
    try {
      let loader = await this.loadingController.create({
        message: "Connecting to server ..."
      });

      await loader.present();

      this.ingameHub = new HubConnectionBuilder()
        .withUrl("/ingamehub")
        .build();
      
      this.ingameHub.on("OnTurnDetective", model => {
        this.model = model;
        this.modelType = TurnType.Detective;

        this.onEventArrived();
      });

      this.ingameHub.onclose(error => {
        console.error(error);
        this.model = null;
        this.HandleConnectionFailure(
          "Lost connection to server",
          "Do you want to reconnect?",
          "Reconnect");
      })

      await this.Connect();
    }
    catch (error) {
      console.log(error);
    }
  }

  private async Connect() {
    // ensure that model was null
    this.model = null;

    try {
      await this.ingameHub.start();
      await new Promise(resolve => setTimeout(resolve, 5000));

      // model should be initialized by event 
      // on connected
      if (this.model == null) {
        await this.ingameHub.stop();
        await this.HandleConnectionFailure(
          "No response from server",
          "Do you want to retry?",
          "Retry");
      }
    }
    catch (error) {
      console.error(error);
      await this.HandleConnectionFailure(
        "Failed to connect",
        "Do you want to retry?",
        "Retry");
    }
  }

  private async HandleConnectionFailure(
    header: string,
    message: string,
    option: string) {
    const alert = await this.alertController.create({
      header: header,
      message: message,
      buttons: [
        {
          text: option,
          handler: async () => {
            let loader = await this.loadingController.create({
              message: "Connecting to server ..."
            });
      
            await loader.present();
            this.Connect();
          }
        }, {
          text: "Exit",
          role: "cancel",
          handler: () => {

          }
        }
      ]
    });

    
    if (await this.loadingController.getTop() != null) {
      await this.loadingController.dismiss();
    }

    alert.present();
  }
  
  private onEventArrived() {
    this.loadingController.dismiss();
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
