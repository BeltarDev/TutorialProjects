import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoadChildren } from '@angular/router';

import { AuthGuard } from './auth/auth.guard';
import { WelcomeComponent } from './welcome/welcome.component';

const routes: Routes = [
  { path: '', component: WelcomeComponent },
  
  { path: 'training',
    loadChildren: () => import('./training/training.module').then(m => m.TrainingModule),
    canLoad: [AuthGuard]
  },
  // { path: 'current-training',
  //   loadChildren: () => import('./training/current-training/current-training.component').then(m => m.CurrentTrainingComponent),
    
  // },
  // { path: 'new-training',
  //   loadChildren: () => import('./training/new-training/new-training.component').then(m => m.NewTrainingComponent),
    
  // },
  // { path: 'previous-training',
  //   loadChildren: () => import('./training/previous-training/previous-training.component').then(m => m.PreviousTrainingComponent),
    
  // },

]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [AuthGuard]
})
export class AppRoutingModule { }
