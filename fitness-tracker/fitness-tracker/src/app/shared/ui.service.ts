import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subject } from 'rxjs';

@Injectable()
export class UIService {

    loadingStateChanged = new Subject<boolean>();

    constructor(private snackbar: MatSnackBar) { }
    
    showSnackbar(message, action, duration, horizon, vertical) {
        this.snackbar.open(message, action, {
            duration: duration,
            horizontalPosition: horizon,
            verticalPosition: vertical
        });
    }
    
}