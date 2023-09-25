import { Component, Input } from '@angular/core';
import { ErrorReturn } from 'src/app/shared/models/ErrorReturn';
import { TodolistService } from '../todolist.service';

@Component({
  selector: 'app-create-reminder',
  templateUrl: './create-reminder.component.html',
  styleUrls: ['./create-reminder.component.css']
})
export class CreateReminderComponent {

  @Input() todoListId: string;
  timestamp: Date;
  hasError = false;
  errorMsg = '';

  constructor(private todoService: TodolistService){

  }

  addReminder():void{
    if(this.timestamp == null || this.timestamp === undefined){
      this.hasError = true;
      this.errorMsg = 'Please enter a date and time.';
      return;
      
    }else if(this.timestamp < new Date()){
      this.hasError = true;
      this.errorMsg = 'Please enter a date and time which is not in the past.';
      return;
    }
    this.hasError = false;
    this.todoService.createReminder({todoListId: this.todoListId, timestamp:this.timestamp}).subscribe({
      next:(result) =>{
          alert('Successfully created a reminder at time: ' + this.timestamp.toString());
      },
      error:(err) =>{
        console.log(err)
        this.hasError = true;
         this.errorMsg = err.error;

      }
    })
  }
}
