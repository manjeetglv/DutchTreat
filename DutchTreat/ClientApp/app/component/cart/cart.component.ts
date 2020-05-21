import {Component} from "@angular/core";
import {DataService} from "../../shared/dataService";
import {Router} from "@angular/router";

@Component({
    selector: "shopping-cart",
    templateUrl: "cart.component.html",
    styleUrls: []
})
export class CartComponent { 
    constructor(public dataService: DataService, private router: Router) {
    }

    onCheckOut() {
        if(this.dataService.loginRequired()){
            // Do Login
            this.router.navigate(["login"]);
        }else{
            // Go to checkout
            this.router.navigate(["checkout"]);
        }
    }
}