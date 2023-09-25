import { TodoAttachment } from "./TodoAttachment"
import { ToDoListItem } from "./ToDoListItem"

export interface ToDoList{
    id?: string,
    title: string,
    description: string,
    isDeleted?: boolean
    toDoItems?: ToDoItems,
    toDoAttachments?: TodoAttachments
}

export interface ToDoItems{
    $id?: string,
    $values: ToDoListItem[]
}

export interface TodoAttachments{
    $id?: string,
    $values: TodoAttachment[]
}