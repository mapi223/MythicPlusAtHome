import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PlayerListComponent } from './player-list/player-list.component';
import { ConfigurationListComponent } from './configuration-list/configuration-list.component';
import { LogInComponent } from './log-in/log-in.component';
import { AuthenticationGuard } from './authentication.guard';

const routes: Routes = [
  { path: 'roulette', component: PlayerListComponent, title: "SpecRoulette - Roulette Page", canActivate: [AuthenticationGuard] },
  { path: 'configurationListings', component: ConfigurationListComponent, title: "SpecRoulette - Group Listings Page" },
  { path: 'logIn', component: LogInComponent, title: "SpecRoulette - User Log In Page" },
  { path: '', redirectTo: "/roulette", pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
