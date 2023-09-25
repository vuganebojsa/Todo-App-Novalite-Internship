import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ToDoListItem } from 'src/app/shared/models/ToDoListItem';
import { TodolistService } from '../todolist.service';

@Component({
  selector: 'app-item-card',
  templateUrl: './item-card.component.html',
  styleUrls: ['./item-card.component.css']
})
export class ItemCardComponent implements OnInit{

  @Input() toDoListItem: ToDoListItem;
  @Input() toDoListId: string;
  @Output() submitted = new EventEmitter<ToDoListItem>();
  itemForm = new FormGroup({

    item: new FormControl('', [Validators.required]),
    status: new FormControl('', [Validators.required])
  
  });
  constructor(private todoService: TodolistService){

  }
  ngOnInit(): void {
    this.itemForm.patchValue({item:this.toDoListItem.content, status: this.toDoListItem.status.toString()})
  }
  save():void{
    if(!this.itemForm.valid){
        return;
    }
    const tdItem = {status:Number(this.itemForm.value.status), content: this.itemForm.value.item, toDoListId:this.toDoListId, id:this.toDoListItem.id};
    this.todoService.updateToDoListItem(tdItem).subscribe({
      next:(res) =>{
        alert('Successfully added a new item.');
        
        this.submitted.emit(res);

        
      },
      error:(err) =>{
        console.log(err);
      }

    })
  }
}
