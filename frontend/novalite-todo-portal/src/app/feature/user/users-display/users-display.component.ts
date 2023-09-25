import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/core/models/User';
import { AuthenticationService } from 'src/app/core/services/authentication.service';

@Component({
  selector: 'app-users-display',
  templateUrl: './users-display.component.html',
  styleUrls: ['./users-display.component.css']
})
export class UsersDisplayComponent implements OnInit{

  users: User[] = [];
  isLoaded = false;
  constructor(private authService: AuthenticationService){

  }
  ngOnInit(): void {
    
    this.authService.getUsersWithAdmin().subscribe( result =>{
      this.users = result.$values;
      this.isLoaded = true;
    })
  }
  trackUsers(index, todo):void{
    return todo ? todo.id : undefined;
  }

  onChangeRole(user: User): void {
    // Handle the role change event here
    console.log(`User ${user.email} selected role: ${user.role}`);
    this.authService.changeRole(user.email, user.role).subscribe(res =>{
      user.role = res.role;
    })
    // You can also make any necessary API calls to update the user's role here
    // Example: this.authService.updateUserRole(user.id, user.selectedRole).subscribe(response => {
    //   console.log('Role updated successfully');
    // });
  }
}
