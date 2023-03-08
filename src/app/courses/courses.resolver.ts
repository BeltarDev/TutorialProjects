import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { select, Store } from "@ngrx/store";
import { Observable } from "rxjs";
import { filter, finalize, first, map, tap } from "rxjs/operators";
import { AppState } from "../reducers/app-reducer";
import { loadAllCourses } from "./course.actions";
import { areCoursesLoaded } from "./courses.selectors";
import { CourseEntityService } from "./services/course-entity.service";

@Injectable()
export class CoursesResolver implements Resolve<boolean>{

    constructor(private coursesService: CourseEntityService) { }
    
    resolve(route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<boolean> {
        
        return this.coursesService.loaded$.pipe(
            tap(loaded => {
                if (!loaded) {
                    this.coursesService.getAll();
                }
            }),
            filter(loaded => !!loaded),
            first()
        );
    }
}