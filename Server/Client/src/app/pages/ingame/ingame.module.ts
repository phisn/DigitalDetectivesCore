import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { IonicModule } from '@ionic/angular';

import { IngamePageRoutingModule } from './ingame-routing.module';

import { IngamePage } from './ingame.page';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    IngamePageRoutingModule
  ],
  declarations: [IngamePage]
})
export class IngamePageModule {}
