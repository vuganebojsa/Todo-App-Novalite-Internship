import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToDoList } from 'src/app/shared/models/ToDoList';
import { TodolistService } from '../todolist.service';
import { FormControl, FormGroup, Validators} from "@angular/forms";
import { ToDoListItemDTO } from 'src/app/shared/models/ToDoListItem';
import { TodoAttachment, TodoAttachmentDTO } from 'src/app/shared/models/TodoAttachment';
import { BlobService } from '../services/blob.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit{
  
  todoList: ToDoList = null;
  isContentLoaded = false;
  isNew = false;
  showCreateReminder = false;

  detailsForm = new FormGroup({

    title: new FormControl('', [Validators.required, Validators.maxLength(255)]),
    description: new FormControl('', [Validators.required]),
  
  });

  constructor(private route: ActivatedRoute, 
    private todoListService: TodolistService, 
    private router: Router){

  }
  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];
    if(id === 'new'){this.isNew = true; return;}

    this.GetToDoListById(id);

  }

  save(): void{
    if(!this.detailsForm.valid) alert('Not all input fields were valid. Try again');
    if(this.todoList === null){
      this.todoList = {title:this.detailsForm.value.title, description:this.detailsForm.value.description};
      this.todoListService.createToDoList(this.todoList).subscribe(res =>{
        alert('Successfully created a new toDo List!');
        this.router.navigate(['lists']);
      });

      return;
    }
    this.todoList.title = this.detailsForm.value.title;
    this.todoList.description = this.detailsForm.value.description;
    this.todoListService.updateToDoList(this.todoList).subscribe(res =>{
      alert('Successfully updated the toDo list.');

      this.router.navigate(['lists']);

    });
  }
  goBack(): void{
    this.router.navigate(['lists']);
  }

  private GetToDoListById(id: any) {
    this.todoListService.getToDoListById(id).subscribe({
      next: (result: ToDoList) => {
        this.todoList = {id: result.id, title: result.title, description: result.description, toDoItems:result.toDoItems, toDoAttachments: result.toDoAttachments};
        this.detailsForm.patchValue({title:this.todoList.title, description: this.todoList.description});

        this.isContentLoaded = true;
        console.log(this.todoList)
      },
      error: (err) => {
      }
    });
  }

  handleChildSubmit(todoItem: ToDoListItemDTO){
    if(this.todoList.toDoItems.$values === null || this.todoList.toDoItems.$values === undefined) this.todoList.toDoItems.$values = [];

    this.todoList.toDoItems.$values.push({id:todoItem.id, content:todoItem.content, status:todoItem.status});
  }
  handleAttachmentSubmit(todoAttachment: TodoAttachmentDTO){
    if(this.todoList.toDoAttachments === null || this.todoList.toDoAttachments === undefined) 
    {this.todoList.toDoAttachments = {$values: []};
    }
    this.todoList.toDoAttachments.$values.push({id:todoAttachment.attachment.id, fileName:todoAttachment.attachment.fileName, todoListId:todoAttachment.attachment.todoListId});

  
  }

  trackItems(index, todo):void{
    return todo ? todo.id : undefined;
  }
  trackAttachments(index, attach):void{
  return attach ? attach.id : undefined;
  }
  createReminder():void{
    this.showCreateReminder = !this.showCreateReminder;
  }
}
