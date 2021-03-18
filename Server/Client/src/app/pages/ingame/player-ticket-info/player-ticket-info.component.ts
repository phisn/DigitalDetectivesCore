import { Component, Input, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { TurnDetectiveEvent } from 'src/app/models/bindings/turn-detective-event';
import { TurnVillianEvent } from 'src/app/models/bindings/turn-villian-event';
import { TurnType } from 'src/app/models/turn-type.enum';

@Component({
  selector: 'app-player-ticket-info',
  templateUrl: './player-ticket-info.component.html',
  styleUrls: ['./player-ticket-info.component.scss'],
})
export class PlayerTicketInfoComponent implements OnInit {
  @Input() public model: TurnDetectiveEvent | TurnVillianEvent;
  @Input() public modelType: TurnType;

  public closeModal() {
    this.modalController.dismiss();
  }

  public isDetective(): boolean {
    return this.modelType == TurnType.Detective;
  }

  public isVillian(): boolean {
    return this.modelType == TurnType.Villian;
  }

  public assertDetective(): TurnDetectiveEvent {
    console.assert(this.modelType == TurnType.Detective);
    return this.model as TurnDetectiveEvent;
  }

  public assertVillian(): TurnVillianEvent {
    console.assert(this.modelType == TurnType.Villian);
    return this.model as TurnVillianEvent;
  }

  constructor(
    private modalController: ModalController) {    
  }

  ngOnInit() {
  }
}
