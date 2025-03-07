import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IClassDetails } from '../class-list/classDetails';
import { IPlayer } from './player.model';

@Component({
  selector: 'app-player',
  templateUrl: './player.component.html',
  styleUrls: ['./player.component.css']
})
export class PlayerComponent {

  @Input() playerId!: number;

  player:IPlayer = {id: 0, SpecList: [], PlayerName: ""};

  @Output() sendPlayerDetails = new EventEmitter<IPlayer>();

  ngOnChanges(){
    if(this.playerId !== undefined){
      this.player.id = this.playerId;
      this.player.PlayerName = "Player " + this.player.id;
    }
  }

  inputPlayerSelection(eventList:IClassDetails[]){
    if (this.player && this.player.SpecList) {
      this.player.SpecList = [];
    }
    for (let index = 0; index < eventList.length; index++) {
      this.player?.SpecList.push(eventList[index].id);
    }
    this.sendPlayerDetails.emit(this.player);
  }


}
