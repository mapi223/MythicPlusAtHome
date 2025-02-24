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

  player:IPlayer = {id: 0, classIds: []};

  @Output() sendPlayerDetails = new EventEmitter<IPlayer>();

  ngOnChanges(){
    if(this.playerId !== undefined){
      this.player.id = this.playerId;
    }
  }

  inputPlayerSelection(eventList:IClassDetails[]){
    if (this.player && this.player.classIds) {
      this.player.classIds = [];
    }
    for (let index = 0; index < eventList.length; index++) {
      this.player?.classIds.push(eventList[index].id);
    }
    this.sendPlayerDetails.emit(this.player);
  }


}
