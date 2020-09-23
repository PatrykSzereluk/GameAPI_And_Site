import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './Components/login/login.component';
import { AuthGuardService } from './services/auth-guard.service';
import { RankingComponent } from './Components/ranking/ranking.component';
import { EmailConfirmationComponent } from './Components/email-confirmation/email-confirmation.component';
import { ChangePassowrdComponent } from './Components/change-passowrd/change-passowrd.component';
import { PageNotFoundComponent } from './Components/page-not-found/page-not-found.component';
import { HomeComponent } from './Components/home/home.component';
import { RegistrationComponent } from './Components/registration/registration.component';


const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full'},
  { path: 'login', component: LoginComponent},
  { path: 'register', component: RegistrationComponent},
  { path: 'home', component: HomeComponent, canActivate: [AuthGuardService]},
  { path: 'ranking', component: RankingComponent},
  { path: 'emailconfirmation/:id/confirm/:playerHash', component: EmailConfirmationComponent},
  { path: 'changepassword/:id/change/:playerHash', component: ChangePassowrdComponent},
  { path: '**', component: PageNotFoundComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }


