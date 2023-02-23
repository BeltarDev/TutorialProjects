import { Component, OnDestroy, OnInit } from '@angular/core';
import { AngularFirestore } from '@angular/fire/compat/firestore';
import { NgForm } from '@angular/forms';
import { tap, Subscription } from 'rxjs';
import { UIService } from 'src/app/shared/ui.service';
import { Exercise } from '../exercise.model';
import { ExerciseService } from '../exercise.service';



@Component({
  selector: 'app-new-training',
  templateUrl: './new-training.component.html',
  styleUrls: ['./new-training.component.css']
})
export class NewTrainingComponent implements OnInit, OnDestroy {
  exercises: Exercise[];
  isLoading = false;

  private loadingSub$: Subscription;
  private exercisesSubscription$: Subscription;

  constructor(
    private exerciseService: ExerciseService,
    private firestore: AngularFirestore,
    private uiService: UIService) { }

  ngOnInit() {
    this.loadingSub$ = this.uiService.loadingStateChanged.subscribe(loadingState => {
      this.isLoading = loadingState;
    });
    this.exercisesSubscription$ = this.exerciseService.exercisesChanged$
      .subscribe((exercises: Exercise[]) => {
        this.exercises = exercises;
      });
    this.exerciseService.fetchAvailableExercises();

  }

  ngOnDestroy(): void {
    if (this.loadingSub$) {
      this.loadingSub$.unsubscribe();
    } if (this.exercisesSubscription$) {
      this.exercisesSubscription$.unsubscribe();
    }
  }
  onStartTraining(form: NgForm) {
    this.exerciseService.startExercise(form.value.exercise);
  }
}
