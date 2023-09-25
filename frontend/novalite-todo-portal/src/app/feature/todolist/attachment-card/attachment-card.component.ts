import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { mergeMap, tap } from 'rxjs';
import { TodoAttachment } from 'src/app/shared/models/TodoAttachment';
import { ToDoListItem } from 'src/app/shared/models/ToDoListItem';
import { BlobService } from '../services/blob.service';
import { TodolistService } from '../todolist.service';

@Component({
  selector: 'app-attachment-card',
  templateUrl: './attachment-card.component.html',
  styleUrls: ['./attachment-card.component.css']
})
export class AttachmentCardComponent implements OnInit{
  @Input() todoAttachment: TodoAttachment;
  @Input() todolistId: string;

  constructor(private todoService: TodolistService,
     private blobService: BlobService){

  }
  ngOnInit(): void {
  }
  downloadAttachment():void{
    this.todoService.getAttachmentSasTokenForDownload(this.todoAttachment.id).subscribe(res =>{
      this.blobService.downloadFile(this.todoAttachment.id, res.token).subscribe({
        next:(blob) =>{
          
          this.triggerDownload(blob, this.todoAttachment.fileName);
        
        
        },

        
      }

      )
    });
  }

  triggerDownload(blob, filename: string): void {
    const a = document.createElement('a');
    const objectUrl = URL.createObjectURL(blob);
    a.href = objectUrl;
    a.download = filename;

    a.click();
    URL.revokeObjectURL(objectUrl);
  }
  
}
