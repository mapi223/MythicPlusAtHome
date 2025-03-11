import { Component } from '@angular/core';
import { IConfiguration } from '../player-list/Configuration';
import { HttpClient } from '@angular/common/http';
import { IPlayer } from '../player/player.model';

@Component({
  selector: 'app-configuration-list',
  templateUrl: './configuration-list.component.html',
  styleUrls: ['./configuration-list.component.css']
})
export class ConfigurationListComponent {

  configs: IConfiguration[] = [];
  playerList: IPlayer[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit() {
    const userid = localStorage.getItem('userId');

    this.http.get<any>('https://localhost:7174/api/Configuration/user/' + userid)
      .subscribe(data => {
        this.configs = data?.$values ? data.$values.map((config: { players: { $values: any; }; }) => ({
        ...config,
          players: config.players.map(player => ({
            playerId: player.id || null,
            playerName: player.name || '',
            numbers: Array.isArray(player.numbers) ? player.numbers : []
          })),
          userId: userid
         
        })) : [];
        this.configs.forEach(config => {
          config.players.forEach((playerData, index) => {
            if (index < this.playerList.length) {
              this.playerList[index].id = playerData.playerId;
              this.playerList[index].PlayerName = playerData.playerName;
              this.playerList[index].SpecList = playerData.specList;
            }
          });
        });
        
        console.log(this.configs);
        console.log(this.playerList);
      })
  }

}
