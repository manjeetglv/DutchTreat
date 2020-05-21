import { Component } from "@angular/core";
import {DataService} from "../../shared/dataService";
import {Router} from "@angular/router";
import {map} from "rxjs/operators";


@Component({
  selector: "checkout",
  templateUrl: "checkout.component.html",
  styleUrls: ['checkout.component.css']
})
export class CheckoutComponent {
  constructor(public dataService: DataService, private router:Router) {
  }
   errorMessage: string = "";

  onCompletePurchase() {
    debugger;
    this.dataService.checkOut().subscribe(success => {
        this.router.navigate(["/"]);
    }, failure => {
      debugger;
      this.errorMessage = failure.error;
    });
  }
}