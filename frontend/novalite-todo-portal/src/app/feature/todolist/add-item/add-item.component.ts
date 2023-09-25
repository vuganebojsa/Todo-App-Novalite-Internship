import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ToDoList } from 'src/app/shared/models/ToDoList';
import { ToDoListItemDTO } from 'src/app/shared/models/ToDoListItem';
import { TodolistService } from '../todolist.service';

@Component({
  selector: 'app-add-item',
  templateUrl: './add-item.component.html',
  styleUrls: ['./add-item.component.css']
})
export class AddItemComponent {

  hasError = false;
  errorMsg = '';
  @Input() toDoList: ToDoList;
  @Output() submitted = new EventEmitter<ToDoListItemDTO>();
  itemForm = new FormGroup({

    item: new FormControl('', [Validators.required])
  
  });
  constructor(private todoService: TodolistService){

  }
  save():void{
    if(!this.itemForm.valid){
        this.hasError = true;
        this.errorMsg = 'The item form is invalid.';
    }
    const tdItem = {status:0, content: this.itemForm.value.item, toDoListId:this.toDoList.id};
    this.todoService.createToDoListItem(tdItem).subscribe({
      next:(res) =>{
        this.itemForm.reset();

        this.submitted.emit(res);

        
      },
      error:(err) =>{
        console.log(err);
      }

    })
  }
}
