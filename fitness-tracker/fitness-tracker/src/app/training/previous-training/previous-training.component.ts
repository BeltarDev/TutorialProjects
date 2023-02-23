import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource} from '@angular/material/table';
import { Exercise } from '../exercise.model';
import { ExerciseService } from '../exercise.service';
import { MatSort } from '@angular/material/sort'
import { MatPaginator } from '@angular/material/paginator'
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-previous-training',
  templateUrl: './previous-training.component.html',
  styleUrls: ['./previous-training.component.css']
})
export class PreviousTrainingComponent implements OnInit, AfterViewInit, OnDestroy {
  displayedColumns = [
    'date',
    'name',
    'duration',
    'calories',
    'state'];

  dataSource = new MatTableDataSource<Exercise>();
  private exerciseChangedSub$: Subscription;

  @ViewChild(MatSort, { static: false }) sort: MatSort;
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;

  constructor(private exerciseService: ExerciseService) {
  }

  ngOnInit() {
    this.exerciseChangedSub$ = this.exerciseService.finishedExercisesChanged$.subscribe(
      (exercises: Exercise[]) => {
      this.dataSource.data = exercises;
    })
    this.exerciseService.fetchFinishedExercises();
  }

  ngOnDestroy() {
    if (this.exerciseChangedSub$) {
      this.exerciseChangedSub$.unsubscribe();
    }
  }

  
  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

  doFilter(filterValue:string){
    this.dataSource.filter = filterValue.trim().toLocaleLowerCase();
  }
}
