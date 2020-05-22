import { Component, OnInit } from '@angular/core';
import {ReportCardModel} from "../models/report-card.model";
import {ReportCardService} from "../services/report-card.service";
import {NotificationService} from "../shared/notification.service";

@Component({
  selector: 'create-report-card',
  templateUrl: 'create-report-card.component.html',
  styleUrls: ['create-report-card.component.css']
})
export class CreateReportCardComponent implements OnInit{
    constructor(private reportCardService: ReportCardService,private notifyService : NotificationService) {
    }
   
    public reportCard:ReportCardModel = new ReportCardModel();
    public showMask:boolean;
    public submitMessage:string;
    public schoolYears: string[];
    
    createReportCard(reportCard: ReportCardModel){
        this.showMask = true;
        this.submitMessage = "Creating Report Card..."
        this.reportCardService.createReportCard(reportCard).subscribe(success => {
            this.showMask = false;
            this.notifyService.showSuccess("", "Report card is created successfully!");
        }, failure => {
            this.showMask = false;
            debugger;
            this.notifyService.showError("", ""+failure.error.errors.toString());
        });
    }
    
    ngOnInit(): void {
        this.reportCardService.getSchoolYears(1156).subscribe(success => {
            this.schoolYears = Object.values(success);
            debugger;
        }, failure =>{
            debugger;
        });
    }
}
