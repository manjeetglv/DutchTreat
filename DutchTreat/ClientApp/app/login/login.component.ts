import {Component} from "@angular/core";
import {DataService} from "../shared/dataService";
import {Router} from "@angular/router";

@Component({
    selector: "login",
    templateUrl: "login.component.html",
    styleUrls: []
})
export class LoginComponent {
    constructor(private dataService: DataService, private router: Router) {
    }
    public creds = {
        username: "",
        password: ""
    };
    errorMessage: string = "";
    onLogin(){
        // Call the login service
        this.dataService.login(this.creds).subscribe(success => {
                if(this.dataService.order.items.length == 0){
                    this.router.navigate([""]);
                }else{
                    this.router.navigate(["checkout"]);
                }
            
        }, failure => {
            this.errorMessage = failure.error;
        });
    }
}