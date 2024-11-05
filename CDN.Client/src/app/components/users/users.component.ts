import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/user.model';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css'],
})
export class UsersComponent implements OnInit {
  userList$?: Observable<User[]>;
  constructor(private readonly userService: UserService) {}

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers() {
    this.userList$ = this.userService.getAll();
  }

  addUser() {}

  viewUser(item: any) {}

  editUser(item: any){}

  deleteUser(item: any){}
}
