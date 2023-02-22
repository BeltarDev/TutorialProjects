import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs'
import { AuthData } from "./auth-data.model";
import { User } from "./user.model";
import { AngularFireAuth } from '@angular/fire/compat/auth';
import { ExerciseService } from '../training/exercise.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UIService } from '../shared/ui.service';

@Injectable()
export class AuthService {
    authChange = new Subject<boolean>();
    private isAuthenticated = false;

    constructor(
        private router: Router,
        private afAuth: AngularFireAuth,
        private exService: ExerciseService,
        private _snackBar: MatSnackBar,
        private uiService: UIService,
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
        this.uiService.loadingStateChanged.next(true);
        this.afAuth.createUserWithEmailAndPassword(
            authData.email,
            authData.password
        ).then(result => {
            this.uiService.loadingStateChanged.next(false);
        })
            .catch(error => {
                this.uiService.loadingStateChanged.next(false);
                this._snackBar.open(error.message, null, {
                    duration: 5000,
                    horizontalPosition: 'center',
                    verticalPosition: 'top',
                });
            });
    }

    login(authData: AuthData) {
        this.uiService.loadingStateChanged.next(true);
        this.afAuth.signInWithEmailAndPassword(
            authData.email,
            authData.password)
            .then(result => {
                this.uiService.loadingStateChanged.next(false);

            })
            .catch(error => {
                this.uiService.loadingStateChanged.next(false);
                this._snackBar.open(error.message, null, {
                    duration: 5000,
                    horizontalPosition: 'center',
                    verticalPosition: 'top',
                });
            });
    }

    logout() {
        this.afAuth.signOut();
    }


    isAuth() {
        return this.isAuthenticated;
    }

}