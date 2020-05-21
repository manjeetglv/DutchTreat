import {LayoutSettingModel} from "./layout-setting.model";
import {ReportCardContentSettingModel} from "./report-card-content-setting.model";
import {GradeSpecificSettingModel} from "./grade-specific-setting.model";

export class ReportCardModel {
    id: number;
    
    constructor(
        public schoolYear: string= "",
        public category: string= "",
        public period: string= "",
        public grade: string= "",
        public studentFullName: string= "",
        public reportType: string= "",
        public layoutSettings: LayoutSettingModel[] =  [new LayoutSettingModel("test",true), new LayoutSettingModel("test2",false)],
        public reportCardContentSettings: ReportCardContentSettingModel[] =  [new LayoutSettingModel("test3",false), new LayoutSettingModel("test4",true)],
        public gradeSpecificSettings: GradeSpecificSettingModel[] =  [new LayoutSettingModel("test5",false)]) {
    }
}