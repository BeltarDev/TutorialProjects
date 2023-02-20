import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Exercise } from '../exercise.model';
import { ExerciseService } from '../exercise.service';
import { MatSort } from '@angular/material/sort'

@Component({
  selector: 'app-previous-training',
  templateUrl: './previous-training.component.html',
  styleUrls: ['./previous-training.component.css']
})
export class PreviousTrainingComponent implements OnInit, AfterViewInit {

  displayedColumns = [
    'date',
    'name',
    'duration',
    'calories',
    'state'];

  dataSource = new MatTableDataSource<Exercise>();

  

  constructor(private exerciseService: ExerciseService) {
  }

  ngOnInit() {
    this.dataSource.data = this.exerciseService.getPreviousExercises();
  }
  @ViewChild(MatSort) sort: MatSort = new MatSort;
  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
  }

  public doFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLocaleLowerCase();
  }
}
