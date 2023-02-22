import { Component, OnDestroy, OnInit } from '@angular/core';
import { AngularFirestore } from '@angular/fire/compat/firestore';
import { NgForm } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Exercise } from '../exercise.model';
import { ExerciseService } from '../exercise.service';



@Component({
  selector: 'app-new-training',
  templateUrl: './new-training.component.html',
  styleUrls: ['./new-training.component.css']
})
export class NewTrainingComponent implements OnInit, OnDestroy {
  exercises: Exercise[];
  exercisesSubscription$: Subscription;

  constructor(private exerciseService: ExerciseService, private firestore: AngularFirestore) { }

  ngOnInit() {
    this.exercisesSubscription$ = this.exerciseService.exercisesChanged$
      .subscribe((exercises: Exercise[]) => this.exercises = exercises);
    this.exerciseService.fetchAvailableExercises();
  }

  ngOnDestroy(): void {
    this.exercisesSubscription$.unsubscribe();
  }
  onStartTraining(form: NgForm) {
    this.exerciseService.startExercise(form.value.exercise);
  }
}
