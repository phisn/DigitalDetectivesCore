import { Component, OnInit } from '@angular/core';
import { AlertController, LoadingController, ModalController } from '@ionic/angular';
import { PlayerColor } from 'src/app/models/game/player-color.enum';
import { MatchInfo } from 'src/app/models/match-info';
import { IngameHubService } from 'src/app/services/ingame-hub/ingame-hub.service';

@Component({
  selector: 'app-ingame-registration',
  templateUrl: './ingame-registration.component.html',
  styleUrls: ['./ingame-registration.component.scss'],
})
export class IngameRegistrationComponent implements OnInit {
  public matchInfo: MatchInfo;

  constructor(
    private ingameHubService: IngameHubService,
    private alertController: AlertController,
    private loadingController: LoadingController,
    private modalController: ModalController) {
  }

  ngOnInit() {
    this.ingameHubService.matchInfo.subscribe(matchInfo => {
      this.matchInfo = matchInfo;
    });
  }

  public async registerAsPlayer(color: PlayerColor) {
    let loader = await this.loadingController.create({
      message: "Joining game ..."
    });

    await loader.present();

    try {
      await this.ingameHubService.registerPlayer(color);
      await this.loadingController.dismiss();
      await this.modalController.dismiss();
    }
    catch (error) {
      console.error(error);

      let alert = await this.alertController.create({
        message: "Failed to register as player",
        buttons: [
          { text: "OK" }
        ]
      });

      await this.loadingController.dismiss();
      await alert.present();
    }
  }
}
