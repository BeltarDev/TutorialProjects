import { Component, OnDestroy, OnInit} from '@angular/core';
import { NgForm } from '@angular/forms';
import { UIService } from 'src/app/shared/ui.service';
import { AuthService } from '../auth.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit, OnDestroy {
  maxDate: Date;
  isLoading = false;
  private loadingSub$: Subscription;
  
  constructor(
    private authService: AuthService,
    private uiService: UIService) {
    
  }

  ngOnInit() {
    this.loadingSub$ = this.uiService.loadingStateChanged.subscribe(loadingStateChanged => {
      this.isLoading = loadingStateChanged;
    });
    this.maxDate = new Date();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18);
  }

  ngOnDestroy() {
    if (this.loadingSub$) {
      this.loadingSub$.unsubscribe();
    }
  }

  onSubmit(form: NgForm){
    this.authService.registerUser({
      email: form.value.email,
      password: form.value.password
    });
    
  }

}
