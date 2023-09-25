import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {  Observable } from 'rxjs';
import { ReminderDTO } from 'src/app/shared/models/Reminder';
import { TodoAttachment, TodoAttachmentDTO } from 'src/app/shared/models/TodoAttachment';
import { ToDoList } from 'src/app/shared/models/ToDoList';
import { ToDoListItemDTO } from 'src/app/shared/models/ToDoListItem';
import { TokenDTO } from 'src/app/shared/models/TokenDTO';
import { environment } from 'src/environment/environment';
@Injectable({
  providedIn: 'root'
})
export class TodolistService {

  base_url: string = environment.apiHost + 'lists';

  constructor(private http: HttpClient) { }

  getToDoLists(): Observable<ToDoList[]>{
    return this.http.get<ToDoList[]>(this.base_url);
  }

  getToDoListById(id: string): Observable<ToDoList>{
    return this.http.get<ToDoList>(this.base_url + '/' + id);
  }

  createToDoList(todoList: ToDoList): Observable<ToDoList>{
    return this.http.post<ToDoList>(this.base_url, todoList);
  }

  updateToDoList(todoList: ToDoList): Observable<ToDoList>{
    return this.http.put<ToDoList>(this.base_url, todoList);
  }

  deleteToDoList(id: string): Observable<boolean>{
    return this.http.delete<boolean>(this.base_url + '/' + id);
  }

  updateToDoListItem(todoItem: ToDoListItemDTO): Observable<ToDoListItemDTO>{
    return this.http.put<ToDoListItemDTO>(this.base_url + '/todolist-item', todoItem);
  }
  createToDoListItem(todoItem: ToDoListItemDTO): Observable<ToDoListItemDTO>{
    return this.http.post<ToDoListItemDTO>(this.base_url + '/todolist-item', todoItem);
  }

  createReminder(reminder: ReminderDTO): Observable<ReminderDTO>{
    return this.http.post<ReminderDTO>(this.base_url + '/reminder', reminder);
  }
  createAttachment(attachment: TodoAttachment): Observable<TodoAttachmentDTO>{
    return this.http.post<TodoAttachmentDTO>(this.base_url + '/attachment', attachment);
  }

  getAttachmentSasTokenForDownload(attachmentId: string): Observable<TokenDTO>{
    return this.http.get<TokenDTO>(this.base_url + '/attachment/' + attachmentId);
  }
}
