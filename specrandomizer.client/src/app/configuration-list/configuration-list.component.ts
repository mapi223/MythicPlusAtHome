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
          players: config.players?.$values || [],
          userId: userid
         
        })) : [];
        this.configs.forEach(config => {
          config.players.forEach((playerData) => {
            this.playerList.push({
              id: playerData.playerId || null,
              PlayerName: playerData.playerName || "",
              SpecList: Array.isArray(playerData.specList?.$values) ? playerData.specList?.$values : []
            });
            playerData = this.playerList;
            this.playerList = [];
          });
        });
        
        console.log(this.configs);
        console.log(this.playerList);
      })
  }

}
