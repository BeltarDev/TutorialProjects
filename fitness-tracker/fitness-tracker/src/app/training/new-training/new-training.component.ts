import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Exercise } from '../exercise.model';
import { ExerciseService } from '../exercise.service';
import { AngularFirestore } from '@angular/fire/compat/firestore';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';



@Component({
  selector: 'app-new-training',
  templateUrl: './new-training.component.html',
  styleUrls: ['./new-training.component.css']
})
export class NewTrainingComponent implements OnInit {

  exercises: Observable<Exercise[]> = new Observable;

  constructor(private exerciseService: ExerciseService, private firestore: AngularFirestore) { }

  ngOnInit() {
    // .valueChanges() extract metadata (id etc)
      this.exercises = this.firestore.collection<Exercise>('availableExercises')
      .snapshotChanges()
      .pipe(map(docArray => {
        return docArray.map(docu => {
          const data = docu.payload.doc.data()
         return {
          id: docu.payload.doc.id,
          calories: data.calories,
          duration: data.duration,
          name: data.name
         };
        }); 
      }))
  }

  onStartTraining(form: NgForm) {
    this.exerciseService.startExercise(form.value.exercise);
  }
}
