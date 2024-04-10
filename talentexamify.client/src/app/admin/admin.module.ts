import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminnavmenuComponent } from './adminnavmenu/adminnavmenu.component';



@NgModule({
  declarations: [
    AdminnavmenuComponent
  ],
  imports: [
    CommonModule,
    
  ],
  exports: [
    AdminnavmenuComponent
  ]
})
export class AdminModule { }
