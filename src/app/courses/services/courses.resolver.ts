import { Injectable } from "@angular/core";
import { map } from "rxjs/operators";
import { CourseEntityService } from "./course-entity.service";

@Injectable()
export class CoursesResolver{

    constructor(private ces: CourseEntityService) {
        
    }
    resolve() {
        return this.ces.getAll().pipe(
            map(courses => !!courses));
        
    }
}