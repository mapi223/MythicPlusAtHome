import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { IPlayer } from '../player/player.model';
import { IUser } from './user.model';
import { IConfiguration } from '../player-list/Configuration';
import { CLASSLIST } from '../class-list/mock-list';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent {
  userName:String = ""
  findUsers: boolean = true;
  isEditingAUser: boolean = false;
  isEditingAConfig: boolean = false;
  users: IUser[] = [];
  configs: IConfiguration[] = [];
  classes = CLASSLIST;

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
    const userid = localStorage.getItem('userId');


    this.http.get<any>('https://localhost:7174/api/user/admin/' + userid)
      .subscribe(data => {
        this.users = data?.$values ? data.$values.map((user: any): IUser => ({
          UserName: user.userName || "",
          UserId: user.uId || null

        })) : [];

      })
  }

  enterEditForUser(usrId: Number) {
    const adminId = localStorage.getItem('userId');
    this.findUsers = false;

    this.http.get<any>('https://localhost:7174/api/Configuration/admin/' + usrId + '?adminId=' + adminId)
      .subscribe(data => {
        this.configs = data?.$values ? data.$values.map((config: { players: { $values: any; }; }) => ({
          ...config,
          players: config.players?.$values.map((player: any): IPlayer => ({
            id: player.playerId || null,
            PlayerName: player.playerName || '',
            SpecList: Array.isArray(player.specList?.$values) ? player.specList?.$values : []
          })),
          userId: usrId

        })) : [];

      })

  }
  editUser() {
    this.isEditingAUser = true;
  }

  editConfig() {
    this.isEditingAConfig = true;
  }
  
  backToUserList() {
    this.findUsers = true;
  }

  sendDetails() {
    if (this.isEditingAUser) {
      this.isEditingAUser = false;
    }
    else if (this.isEditingAConfig) {
      this.isEditingAConfig = false;
    }
    else {
      console.log("how did we end up here");
    }
  }
}
