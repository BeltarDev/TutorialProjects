import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AuthGuard } from "../auth/auth.guard";
import { CurrentTrainingComponent } from "./current-training/current-training.component";
import { NewTrainingComponent } from "./new-training/new-training.component";
import { PreviousTrainingComponent } from "./previous-training/previous-training.component";
import { TrainingComponent } from "./training.component";

const routes: Routes = [
    {path: '', component: TrainingComponent},
    {path: 'current-training', component: CurrentTrainingComponent},
    {path: 'new-training', component: NewTrainingComponent},
    {path: 'previous-training', component: PreviousTrainingComponent},
]
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class TrainingRoutingModule{

}