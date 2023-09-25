export interface ToDoListItem{
    $id?:string,
    id?:string,
    content: string,
    status: number | Status
}

export enum Status{
    Open, Active, Closed
}
export interface ToDoListItemDTO{
    $id?:string,
    id?:string,
    content:string,
    status:number,
    toDoListId:string
}
