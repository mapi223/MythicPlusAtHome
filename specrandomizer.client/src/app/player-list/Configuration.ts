import { IPlayer } from "../player/player.model";


export interface IConfiguration {
  configurationId: number;
  userId: number;
  players: IPlayer[];
}
export interface IRoleAssignment {
  PlayerID: number;
  PlayerName: string;
  Spec: string;
}

export interface ISpecialization {
  ClassId: number;
  Role: IRole;
  SpecName: string;
}

export enum IRole {
  Tank,
  Healer,
  Damage,
  INVALID
}
