import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { map, mergeMap, tap } from 'rxjs';
import { TodoAttachment, TodoAttachmentDTO } from 'src/app/shared/models/TodoAttachment';
import { ToDoList } from 'src/app/shared/models/ToDoList';
import { BlobService } from '../services/blob.service';
import { TodolistService } from '../todolist.service';

@Component({
  selector: 'app-add-attachment',
  templateUrl: './add-attachment.component.html',
  styleUrls: ['./add-attachment.component.css']
})
export class AddAttachmentComponent {
  hasError = false;
  errorMsg = '';
  @Input() toDoList: ToDoList;
  @Output() submitted = new EventEmitter<TodoAttachmentDTO>();
  selectedFile:File | null = null;
  attachmentForm = new FormGroup({

    fileName: new FormControl('', [Validators.required])
  
  });
  constructor(private todoService: TodolistService,
    private blobService: BlobService){

  }
  getFileExtension(filename: string): string {
    const dotIndex = filename.lastIndexOf('.');
    if (dotIndex !== -1 && dotIndex < filename.length - 1) {
      return filename.substr(dotIndex + 1).toLowerCase();
    }
    return '';
  }
  save():void{
    if(!this.attachmentForm.valid){
        this.hasError = true;
        this.errorMsg = 'The attachment form is invalid.';
        return;
    }
    if(!this.selectedFile) {
        this.hasError = true;
        this.errorMsg = 'The attachment form is invalid.';
        return;
    }


    const newFileName = this.attachmentForm.get('fileName').value +  '.' + this.getFileExtension(this.selectedFile.name);
    const modifiedFile = new File([this.selectedFile], newFileName, { type: this.selectedFile.type });
    const attachment: TodoAttachment = {fileName: this.attachmentForm.value.fileName + '.' + this.getFileExtension(this.selectedFile.name), todoListId:this.toDoList.id, type:this.selectedFile.type};
    this.todoService.createAttachment(attachment)
    .pipe(mergeMap(res => {
        this.attachmentForm.reset();
        const againModifiedFile = new File([this.selectedFile], res.attachment.id, { type: this.selectedFile.type });
        return this.blobService.uploadFile(againModifiedFile, res.attachment.id, res.sasToken).pipe(tap(_ => {this.submitted.emit(res);}));
    }))
    .subscribe();
  }
  onFileSelected(event: Event): void {
    const inputElement = event.target as HTMLInputElement;
    if (inputElement.files && inputElement.files.length > 0) {
        this.selectedFile = inputElement.files[0];
        console.log(this.selectedFile);
    }
  
}
}
