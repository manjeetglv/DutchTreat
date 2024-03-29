import {Component, OnInit} from "@angular/core"
import {DataService} from "../../shared/dataService";
import {Product} from "../../shared/product";

@Component({
    selector: "product-list",
    templateUrl: "product.component.html",
    styleUrls:["product.component.css"]
})

export class ProductComponent implements OnInit{
    constructor(private dataService: DataService) {
    }
    
    public products: Product[] = [];

    ngOnInit(): void {
        this.dataService.loadProducts()
            .subscribe(success => {
                if(success){
                    this.products = this.dataService.products;
                }
            });
    }

    addProduct(product: Product) {
        this.dataService.addToOrder(product);
    }
}