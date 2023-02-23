import { NgModule } from "@angular/core";
import { AngularFirestoreModule } from "@angular/fire/compat/firestore";
import { SharedModule } from "../shared/shared.module";
import { CurrentTrainingComponent } from "./current-training/current-training.component";
import { StopTrainingComponent } from "./current-training/stop-training.component";
import { NewTrainingComponent } from "./new-training/new-training.component";
import { PreviousTrainingComponent } from "./previous-training/previous-training.component";
import { TrainingRoutingModule } from "./training-routing.module";
import { TrainingComponent } from "./training.component";

@NgModule({
    declarations: [
        TrainingComponent,
        CurrentTrainingComponent,
        NewTrainingComponent,
        PreviousTrainingComponent,
        StopTrainingComponent,

    ],
    imports: [
        SharedModule,
        TrainingRoutingModule,
        AngularFirestoreModule,
    ],
    entryComponents: [StopTrainingComponent],
    exports: []
})
export class TrainingModule {

}