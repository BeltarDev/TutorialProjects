import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ExerciseService } from './exercise.service';

@Component({
  selector: 'app-training',
  templateUrl: './training.component.html',
  styleUrls: ['./training.component.css']
})
export class TrainingComponent implements OnInit, OnDestroy {

  ongoingTraining = false;
  exerciseSubscription$: Subscription = new Subscription;

  constructor(private exerciseService: ExerciseService) { }
  
  ngOnInit() { 
    this.exerciseSubscription$ = this.exerciseService.exerciseChanged$.subscribe(exercise => {
      if (exercise) {
        this.ongoingTraining = true;
      } else {
        this.ongoingTraining = false
      }
    });
  }

  ngOnDestroy(): void {
    if (this.exerciseSubscription$) {
      this.exerciseSubscription$.unsubscribe();
    }
  }
}
