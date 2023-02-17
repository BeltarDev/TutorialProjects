import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ExerciseService } from '../exercise.service';
import { StopTrainingComponent } from './stop-training.component';

@Component({
  selector: 'app-current-training',
  templateUrl: './current-training.component.html',
  styleUrls: ['./current-training.component.css']
})
export class CurrentTrainingComponent {
 
  progress = 0;
  timer: number = 0;

  constructor(private dialog: MatDialog, private exerciseService: ExerciseService) {

  }

  ngOnInit() {
    this.startOrResumeTimer();
  }

  startOrResumeTimer() {
    const step = ((this.exerciseService.getRunningExercise().duration!) / 100) * 1000;
    this.timer = setInterval(() => {
      if (this.progress >= 100) {
        this.exerciseService.completeExercise();
        clearInterval(this.timer);
      } else {
        this.progress = this.progress + 1;
      }
    }, step)
  }

  onStop() {
    clearInterval(this.timer);
    const dialogRef = this.dialog.open(StopTrainingComponent, {
      data: {
        progress: this.progress
      }
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result === true) {
        this.exerciseService.cancelExercise(this.progress);
      }
      else {
        this.startOrResumeTimer();
      }
    })

  }
}
