import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AuthGuardService } from './services/auth-guard.service';
import { RankingComponent } from './ranking/ranking.component';
import { EmailConfirmationComponent } from './email-confirmation/email-confirmation.component';
import { ChangePassowrdComponent } from './change-passowrd/change-passowrd.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { HomeComponent } from './home/home.component';
import { RegistrationComponent } from './registration/registration.component';


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


