import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { ToDoList } from 'src/app/shared/models/ToDoList';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css']
})
export class CardComponent {
  @Input() todoList: ToDoList;
 
  constructor(private router: Router){}
  
  showDetails():void{
    this.router.navigate(['lists/' + this.todoList.id]);
  }
}
