import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-form-submit-alert',
  templateUrl: "form-submit-alert.component.html",
  styleUrls: ["form-submit-alert.component.css"]
})
export class FormSubmitAlertComponent implements OnInit {
  @Input() showProgress: boolean = false;
  @Input() progressMessage: string = "Loading..."
  constructor() { }

  ngOnInit(): void {
  }

}
