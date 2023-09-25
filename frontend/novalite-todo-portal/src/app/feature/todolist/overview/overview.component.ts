import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToDoList } from 'src/app/shared/models/ToDoList';
import { TodolistService } from '../todolist.service';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.css']
})
export class OverviewComponent implements OnInit{

  todoLists: ToDoList[] = [];
  isContentLoaded = false;
  ngOnInit(): void {
    this.GetToDoLists();
  }
  constructor(private todolistService: TodolistService, private router: Router){

  }




  private GetToDoLists() {

    this.todolistService.getToDoLists().subscribe({
      next: (result) => {
        this.todoLists = result['$values'];
        this.isContentLoaded = true;
      },
      error: (err) => {
        console.log(err);
      }
    });
  }


  trackTodos(index, todo):void{
      return todo ? todo.id : undefined;
  }

  deleteTodo(todo: ToDoList):void{
    this.todolistService.deleteToDoList(todo.id).subscribe({
      next:(result) =>{
        if(result) {
          alert('Successfully deleted the ToDo list.');
          this.todoLists = this.todoLists.filter(item => item !== todo);

        }
      },
      error:(err) =>{
        console.log(err);
      }
    });
  }

  addNewToDo():void{
    this.router.navigate(['lists/new']);
  }
}
