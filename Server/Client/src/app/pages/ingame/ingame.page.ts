import { Component, OnInit } from '@angular/core';
import { AlertController } from '@ionic/angular';

@Component({
  selector: 'app-ingame',
  templateUrl: './ingame.page.html',
  styleUrls: ['./ingame.page.scss'],
})
export class IngamePage implements OnInit {
  constructor(public alertController: AlertController) { }

  ngOnInit() {
  }

  public async showTurnMessage() {
    const alert = await this.alertController.create({
      header: "Your turn",
      message: "It is your turn now",
      buttons: ['Continue']
    });

    await alert.present();
  }
}
