export interface TodoAttachment{
    $id?:string,
    id?:string,
    todoListId:string,
    type?:string,
    fileName:string
}
export interface TodoAttachmentDTO{
    sasToken:string,
    attachment: TodoAttachment,
    $id?:string,
    type?:string
}