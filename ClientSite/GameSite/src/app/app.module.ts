import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './Components/login/login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { IdentityService } from './services/identity.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthGuardService } from './services/auth-guard.service';
import { RankingComponent } from './Components/ranking/ranking.component';
import { TokenInterceptorService } from './services/token-interceptor.service';
import { EmailConfirmationComponent } from './Components/email-confirmation/email-confirmation.component';
import { ChangePassowrdComponent } from './Components/change-passowrd/change-passowrd.component';
import { PageNotFoundComponent } from './Components/page-not-found/page-not-found.component';
import { UserService } from './services/user.service';
import { HomeComponent } from './Components/home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MainNavComponent } from './Components/main-nav/main-nav.component';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { RegistrationComponent } from './Components/registration/registration.component';
import { PasswordForgottenComponent } from './Components/password-forgotten/password-forgotten.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RankingComponent,
    EmailConfirmationComponent,
    ChangePassowrdComponent,
    PageNotFoundComponent,
    HomeComponent,
    MainNavComponent,
    RegistrationComponent,
    PasswordForgottenComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatButtonModule,
    LayoutModule,
    MatToolbarModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule
  ],
  providers: [
    IdentityService,
    AuthGuardService,
    UserService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
