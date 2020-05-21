import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateReportCardComponent } from './create-report-card/create-report-card.component';
import { DrumsComponent } from './drums.component';
import {FormsModule} from "@angular/forms";
import {ReportCardService} from "./services/report-card.service";
import {FormSubmitAlertComponent} from "./shared/form-submit-alert/form-submit-alert.component";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {ToastrModule} from "ngx-toastr";
import {NotificationService} from "./shared/notification.service";


@NgModule({
  declarations: [CreateReportCardComponent, DrumsComponent, FormSubmitAlertComponent],
    imports: [
        CommonModule,
        FormsModule,
        BrowserAnimationsModule,
        ToastrModule.forRoot()
    ],
    exports:[], 
    providers:[ReportCardService, NotificationService]
})
export class DrumsModule { }
