import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {map} from "rxjs/operators";
import {Order} from "../../shared/order";
import {ReportCardModel} from "../models/report-card.model";

@Injectable()
export class ReportCardService {
    constructor(private http:HttpClient) {
    }

    public createReportCard(reportCard: ReportCardModel){
        return this.http.post("/api/reportCards", reportCard);
    }

    public getSchoolYears(number: number) {
        return this.http.get("http://localhost:53268/reports/reportcard/DropdownSchoolYear?schoolId=1156")
            .pipe(map(response => {
                    debugger;
                }
            ));
    }
}