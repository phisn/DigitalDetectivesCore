import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { IngamePageRoutingModule } from './ingame-routing.module';

import { IngamePage } from './ingame.page';
import { PlayerTicketInfoComponent } from './player-ticket-info/player-ticket-info.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    IngamePageRoutingModule
  ],
  declarations: [
    IngamePage,
    PlayerTicketInfoComponent
  ]
})
export class IngamePageModule {}
