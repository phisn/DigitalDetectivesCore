import { Component, Input, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { DetectiveState } from 'src/app/models/detective-state';
import { IngameState } from 'src/app/models/ingame-state';
import { StateType } from 'src/app/models/state-type.enum';
import { VillianState } from 'src/app/models/villian-state';

@Component({
  selector: 'app-player-ticket-info',
  templateUrl: './player-ticket-info.component.html',
  styleUrls: ['./player-ticket-info.component.scss'],
})
export class PlayerTicketInfoComponent implements OnInit {
  @Input() public model: IngameState;

  public closeModal() {
    this.modalController.dismiss();
  }

  public isDetective(): boolean {
    return this.model.type == StateType.Detective;
  }

  public isVillian(): boolean {
    return this.model.type == StateType.Villian;
  }

  public assertDetective(): DetectiveState {
    console.assert(this.model.type == StateType.Detective);
    return this.model.state as DetectiveState;
  }

  public assertVillian(): VillianState {
    console.assert(this.model.type == StateType.Villian);
    return this.model.state as VillianState;
  }

  constructor(
    private modalController: ModalController) {    
  }

  ngOnInit() {
  }
}
