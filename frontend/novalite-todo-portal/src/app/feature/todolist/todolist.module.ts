import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { TodolistRoutingModule } from './todolist-routing.module';
import { OverviewComponent } from './overview/overview.component';
import { DetailsComponent } from './details/details.component';
import { CardComponent } from './card/card.component';
import { AddItemComponent } from './add-item/add-item.component';
import { ItemCardComponent } from './item-card/item-card.component';
import { CreateReminderComponent } from './create-reminder/create-reminder.component';
import { AddAttachmentComponent } from './add-attachment/add-attachment.component';
import { AttachmentCardComponent } from './attachment-card/attachment-card.component';


@NgModule({
  declarations: [
    OverviewComponent,
    DetailsComponent,
    CardComponent,
    AddItemComponent,
    ItemCardComponent,
    CreateReminderComponent,
    AddAttachmentComponent,
    AttachmentCardComponent
  ],
  imports: [
    CommonModule,
    TodolistRoutingModule,
    FormsModule,
    ReactiveFormsModule],
  exports: []
})
export class TodolistModule { }
