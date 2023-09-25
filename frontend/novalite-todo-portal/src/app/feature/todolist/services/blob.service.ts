import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BlobService {

  constructor(private http: HttpClient) { 
    
  }
  uploadFile(file: File, filename: string, sasToken: string): Observable<void>{
    const blobUrl = "https://blobstoragevuga.blob.core.windows.net/blobstorage/" + filename + '?' + sasToken;
    const headers = new HttpHeaders().set('x-ms-blob-type', 'BlockBlob'); 
    return this.http.put<void>(blobUrl, file, {headers: headers});

  }
  downloadFile(attachmentId: string, sasToken: string){
    const blobUrl = "https://blobstoragevuga.blob.core.windows.net/blobstorage/" + attachmentId + '?' + sasToken;
    const headers = new HttpHeaders().set('x-ms-blob-type', 'BlockBlob').set('ResponseType', 'blob'); 
    return this.http.get(blobUrl, { responseType: 'blob' });

  }
}
