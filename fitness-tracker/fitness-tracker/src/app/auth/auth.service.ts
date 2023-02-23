import { Injectable } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/compat/auth';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Subject } from 'rxjs';
import { UIService } from '../shared/ui.service';
import { ExerciseService } from '../training/exercise.service';
import { AuthData } from './auth-data.model';
import * as fromApp from '../app.reducer';

@Injectable()
export class AuthService {
    authChange = new Subject<boolean>();
    private isAuthenticated = false;

    constructor(
        private router: Router,
        private afAuth: AngularFireAuth,
        private exService: ExerciseService,
        private uiService: UIService,
        private store: Store<{ui: fromApp.State}>,

        ) { }

    initAuthListener() {
        this.afAuth.authState.subscribe(user => {
            if (user) {
                this.isAuthenticated = true;
                this.authChange.next(true);
                this.router.navigate(['/training']);
            } else {
                this.exService.cancelSubscriptions();
                this.authChange.next(false);
                this.router.navigate(['/login']);
                this.isAuthenticated = false;
            }
        });
    }

    registerUser(authData: AuthData) {
        // this.uiService.loadingStateChanged.next(true);
        this.store.dispatch({ type: 'START_LOADING' });
        this.afAuth.createUserWithEmailAndPassword(
            authData.email,
            authData.password
        ).then(result => {
            // this.uiService.loadingStateChanged.next(false);
            this.store.dispatch({ type: 'STOP_LOADING' });
        })
            .catch(error => {
                this.store.dispatch({type: 'STOP_LOADING'});
                // this.uiService.loadingStateChanged.next(false);
                this.uiService.showSnackbar(error.message, null, 5000, 'center', 'top');
            });
    }

    login(authData: AuthData) {
        // this.uiService.loadingStateChanged.next(true);
        this.store.dispatch({ type: 'START_LOADING' });

        this.afAuth.signInWithEmailAndPassword(
            authData.email,
            authData.password)
            .then(result => {
                // this.uiService.loadingStateChanged.next(false);
            this.store.dispatch({ type: 'STOP_LOADING' });
            })
            .catch(error => {
                // this.uiService.loadingStateChanged.next(false);
            this.store.dispatch({ type: 'STOP_LOADING' });

                this.uiService.showSnackbar(error.message, null, 5000, 'center', 'top');
            });
    }

    logout() {
        this.afAuth.signOut();
    }


    isAuth() {
        return this.isAuthenticated;
    }

}