import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { IngamePageRoutingModule } from './ingame-routing.module';

import { IngamePage } from './ingame.page';
import { PlayerTicketInfoComponent } from './player-ticket-info/player-ticket-info.component';
import { IngamePlayerComponent } from './ingame-player/ingame-player.component';
import { IngameSpectatorComponent } from './ingame-spectator/ingame-spectator.component';
import { IngameRegistrationComponent } from './ingame-registration/ingame-registration.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    IngamePageRoutingModule
  ],
  declarations: [
    IngamePage,
    IngamePlayerComponent,
    IngameRegistrationComponent,
    IngameSpectatorComponent,
    PlayerTicketInfoComponent,
  ]
})
export class IngamePageModule {
}
