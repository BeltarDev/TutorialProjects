import { Exercise } from "./exercise.model";
import { Subject } from "rxjs";

export class ExerciseService {

    exerciseChanged$ = new Subject<Exercise | null>();

    availableExercises: Exercise[] = [
        { id: 'crunches', name: 'Crunches', duration: 30, calories: 8 },
        { id: 'touch-toes', name: 'Touch Toes', duration: 180, calories: 15 },
        { id: 'side-lunges', name: 'Side Lunges', duration: 120, calories: 18 },
        { id: 'burpees', name: 'Burpees', duration: 60, calories: 8 },
        { id: 'bench-press', name: 'Bench Press', duration: 600, calories: 20 }
    ];

    private runningExercise: Exercise | undefined = undefined;
    private exercises: Exercise[] = [];

    getAvailableExercises() {
        return this.availableExercises.slice();
    }

    getRunningExercise() {
        return { ...this.runningExercise };
    }

    getPreviousExercises() {
        return this.exercises.slice();
    }

    startExercise(selectedId: string) {
        this.runningExercise = this.availableExercises.find(
            ex => ex.id === selectedId);
        if (this.runningExercise !== undefined) {
            this.exerciseChanged$.next({ ...this.runningExercise! });
        }
    }

    completeExercise() {
        if (this.runningExercise !== undefined) {
            this.exercises.push({
                ...this.runningExercise,
                date: new Date(),
                state: 'completed'
            });
        }
        this.runningExercise = undefined;
        this.exerciseChanged$.next(null);
    }

    cancelExercise(progress: number) {
        if (this.runningExercise != null) {
            this.exercises.push({
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
}
