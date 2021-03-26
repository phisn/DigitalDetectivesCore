import { Component, OnInit } from '@angular/core';
import { AlertController, LoadingController, ModalController } from '@ionic/angular';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { TicketType } from 'src/app/models/game/ticket-type.enum';
import { PlayerTicketInfoComponent } from './player-ticket-info/player-ticket-info.component';
import { StateType } from 'src/app/models/state-type.enum';
import { IngameState } from 'src/app/models/ingame-state';
import { IngameHubService } from 'src/app/services/ingame-hub/ingame-hub.service';
import { RouteOption } from 'src/app/models/route-option';
import { IngameRegistrationComponent } from './ingame-registration/ingame-registration.component';

@Component({
  selector: 'app-ingame',
  templateUrl: './ingame.page.html',
  styleUrls: ['./ingame.page.scss'],
})
export class IngamePage implements OnInit {
  StateType = StateType;
  public model: IngameState;
  
  constructor(
    private ingameHubService: IngameHubService,
    private alertController: AlertController,
    private loadingController: LoadingController,
    private modalController: ModalController) {
  }
  
  async ngOnInit() {
    let loader = await this.loadingController.create({
      message: "Connecting to server ..."
    });

    await loader.present();
    
    this.ingameHubService.onclose.subscribe(async error => {
      console.error(error); 

      if (await this.modalController.getTop() != null) {
        await this.modalController.dismiss();
      }

      this.model = null;

      await this.HandleConnectionFailure(
        "Lost connection to server",
        "Do you want to reconnect?",
        "Reconnect");
    });

    console.log(this.ingameHubService);

    this.ingameHubService.state.subscribe(state => {
      console.log("STATE");
      this.model = state;
    });
    
    await this.connect();
  }

  private async connect() {
    // ensure that model was null
    this.model = null;

    try {
      await this.ingameHubService.connect();

      let modal = await this.modalController.create({
        component: IngameRegistrationComponent,
        backdropDismiss: false
      });

      this.loadingController.dismiss();
      await modal.present();
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
            this.connect();
          }
        }
      ]
    });

    if (await this.loadingController.getTop() != null) {
      await this.loadingController.dismiss();
    }

    alert.present();
  }
}
