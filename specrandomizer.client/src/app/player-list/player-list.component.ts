import { Component } from '@angular/core';
import { IPlayer } from '../player/player.model';
import { HttpClient } from '@angular/common/http';
import { IConfiguration, IRoleAssignment } from './Configuration';

@Component({
  selector: 'app-player-list',
  templateUrl: './player-list.component.html',
  styleUrls: ['./player-list.component.css']
})
export class PlayerListComponent {

  RoleAssignments: any;
  players:number = 0;
  playerList: IPlayer[] = [];
  Configuration = {
    UserID: 1,
    players: this.playerList,
  }

  constructor(private http: HttpClient) { }

  addPlayer(){
    if(this.players<5){
      this.players++;
    }
  }
  removePlayer() {
    if (this.players > 0) {
      this.players--;
    }
  }

  onSubmit() {
    console.log("submit hit");
    this.http.post<IConfiguration>('https://localhost:7174/api/Configuration', this.Configuration, {
      headers: { 'Content-Type': 'application/json' }
    }).subscribe((response) => {
      response.configurationId;
      console.log("Configuration saved successfully!");
      this.http.get<any>('https://localhost:7174/group/')
        .subscribe((GetResponse) => {
          this.RoleAssignments = GetResponse['$values'] || [];
        },
          error => {
            console.log("Beep Boop: Get Error ", error);
          });
    }, error => {
      console.error("Error occurred:", error);
    });
  }
  

  get playersArray() {
    return Array(this.players).fill(0);
  }

  inputPlayerList(eventList: IPlayer) {

    this.playerList[eventList.id -1] = eventList;
    console.log(this.playerList);
  }


}
