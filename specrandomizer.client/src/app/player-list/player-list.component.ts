import { Component } from '@angular/core';
import { IPlayer } from '../player/player.model';

@Component({
  selector: 'app-player-list',
  templateUrl: './player-list.component.html',
  styleUrls: ['./player-list.component.css']
})
export class PlayerListComponent {

  players:number = 0;
  playerList:IPlayer[] = [];

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

  onSubmit(){

  }

  get playersArray() {
    return Array(this.players).fill(0);
  }

  inputPlayerList(eventList:IPlayer){
    this.playerList.push(eventList);
    console.log(this.playerList);
  }


}
