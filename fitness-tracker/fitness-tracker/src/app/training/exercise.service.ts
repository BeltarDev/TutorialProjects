import { Exercise } from "./exercise.model";
import { Subject } from "rxjs";
import { AngularFirestore } from "@angular/fire/compat/firestore";
import { map } from "rxjs/operators";
import { Injectable } from "@angular/core";
import { Subscription } from "rxjs";
import { UIService } from "../shared/ui.service";

@Injectable()
export class ExerciseService {
    exerciseChanged$ = new Subject<Exercise | null>();
    exercisesChanged$ = new Subject<Exercise[]>();
    finishedExercisesChanged$ = new Subject<Exercise[]>();

    private availableExercises: Exercise[] = [];
    private runningExercise: Exercise | undefined = undefined;
    private exercises: Exercise[] = [];
    private fbSubs: Subscription[] = [];

    constructor(
        private firestore: AngularFirestore,
        private uiService: UIService,) { }

    fetchAvailableExercises() {
        this.uiService.loadingStateChanged.next(true);
        this.fbSubs.push(this.firestore.collection<Exercise>('availableExercises')
            .snapshotChanges()
            .pipe(map(docArray => {
                return docArray.map(docu => {
                    const data = docu.payload.doc.data()
                    return {
                        id: docu.payload.doc.id,
                        calories: data.calories,
                        duration: data.duration,
                        name: data.name
                    } as Exercise;
                });
            }))
            .subscribe({
                next: (ex) => {
                    this.availableExercises = ex;
                    this.exercisesChanged$.next([...this.availableExercises]);
                    this.uiService.loadingStateChanged.next(false);
                },
                error: (error) => {
                    this.uiService.showSnackbar(
                        'Fetching Exercises failed, please try again',
                        null,
                        5000,
                        'center',
                        'top')
                }
            }));
    }

    getRunningExercise() {
        return { ...this.runningExercise };
    }

    fetchFinishedExercises() {
        this.fbSubs.push(this.firestore.collection('finishedExercises')
            .valueChanges()
            .subscribe((exercises: Exercise[]) => {
                this.finishedExercisesChanged$.next(exercises);
            }));
    }

    startExercise(selectedId: string) {
        // this.firestore.doc('availableExercises/' + selectedId).update({ lastSelected: new Date() });
        this.runningExercise = this.availableExercises.find(
            ex => ex.id === selectedId);
        if (this.runningExercise !== undefined) {
            this.exerciseChanged$.next({ ...this.runningExercise! });
        }
    }

    completeExercise() {
        if (this.runningExercise !== undefined) {
            this.addDataToDatabase({
                ...this.runningExercise,
                date: new Date(),
                state: 'completed'
            });
        }
        this.runningExercise = undefined;
        this.exerciseChanged$.next(null);
    }

    cancelExercise(progress: number) {
        if (this.runningExercise !== undefined || null) {
            this.addDataToDatabase({
                ...this.runningExercise,
                date: new Date(),
                state: 'cancelled',
                calories: this.runningExercise.calories * (progress / 100),
                duration: this.runningExercise.duration * (progress / 100)
            });
            this.runningExercise = undefined;
            this.exerciseChanged$.next(null);
        }
    }

    cancelSubscriptions() {
        this.fbSubs.forEach(sub => sub.unsubscribe());
    }
    private addDataToDatabase(exercise: Exercise) {
        this.firestore.collection('finishedExercises').add(exercise);
    }
}
