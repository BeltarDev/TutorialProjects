import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { SignupComponent } from './auth/signup/signup.component';
import { TrainingComponent } from './training/training.component';
import { CurrentTrainingComponent } from './training/current-training/current-training.component';
import { NewTrainingComponent } from './training/new-training/new-training.component';
import { PreviousTrainingComponent } from './training/previous-training/previous-training.component';

import { WelcomeComponent } from './welcome/welcome.component';
import { AuthGuard } from './auth/auth.guard';

const routes: Routes = [
  {path: '', component: WelcomeComponent},
  {path: 'signup', component: SignupComponent},
  {path: 'login', component: LoginComponent},
  {path: 'training', component: TrainingComponent, canActivate: [AuthGuard]},
  {path: 'current-training', component: CurrentTrainingComponent, canActivate: [AuthGuard]},
  {path: 'new-training', component: NewTrainingComponent, canActivate: [AuthGuard]},
  {path: 'previous-training', component: PreviousTrainingComponent, canActivate: [AuthGuard]},


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AuthGuard]
})
export class AppRoutingModule { }
