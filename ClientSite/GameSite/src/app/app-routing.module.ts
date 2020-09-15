import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AuthGuardService } from './services/auth-guard.service';
import { RankingComponent } from './ranking/ranking.component';
import { EmailConfirmationComponent } from './email-confirmation/email-confirmation.component';
import { ChangePassowrdComponent } from './change-passowrd/change-passowrd.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';


const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full'},
  { path: 'login', component: LoginComponent},
  { path: 'ranking', component: RankingComponent},
  { path: 'emailconfirmation/:id/confirm/:playerHash', component: EmailConfirmationComponent},
  { path: 'changepassword/:id/change/:playerHash', component: ChangePassowrdComponent},
  { path: '**', component: PageNotFoundComponent }
  // { path: 'register', component: LoginComponent, canActivate: [AuthGuardService]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }


