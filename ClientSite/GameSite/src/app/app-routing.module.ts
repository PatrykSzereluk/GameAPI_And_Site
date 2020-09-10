import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AuthGuardService } from './services/auth-guard.service';
import { RankingComponent } from './ranking/ranking.component';
import { EmailConfirmationComponent } from './email-confirmation/email-confirmation.component';


const routes: Routes = [
  { path: 'login', component: LoginComponent},
  { path: 'ranking', component: RankingComponent},
  { path: 'emailconfirmation/:id', component: EmailConfirmationComponent}
  //{ path: 'register', component: LoginComponent, canActivate: [AuthGuardService]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }


