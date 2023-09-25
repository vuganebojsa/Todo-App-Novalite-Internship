import { Component, OnInit } from '@angular/core';
import { TokenDecoderService } from 'src/app/core/services/token-decoder.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit{

  profileName: string = '';
  profileEmail: string = '';
  role:string = '';
  constructor(){}

  ngOnInit(): void {

    this.profileEmail = localStorage.getItem('email');
    this.profileName = localStorage.getItem('name');

    this.role = localStorage.getItem('role');
  }


}
