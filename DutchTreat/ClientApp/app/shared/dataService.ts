import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {map} from "rxjs/operators";
import {Observable} from "rxjs";
import {Product} from "./product";
import {Order, OrderItem} from "./order";

@Injectable()
export class DataService {
    constructor(private http:HttpClient) {
    }
    
    private token: string = "";
    private tokenExpirationTime: Date;
    
    public order: Order = new Order();
    public products: Product[]  = [];
    
    loadProducts(): Observable<boolean>{
       return this.http.get("/api/products")
           .pipe(
               map((data: any[]) => 
               {
                   this.products = data; 
                   return true;
               }
           )
        );
    }
    
    public loginRequired(): boolean{
        return this.token.length == 0 || this.tokenExpirationTime > new Date();
    }
    
    public login(creds){
       return this.http.post("/account/createtoken", creds).pipe(map((data: any) => {
            this.token = data.token;
            this.tokenExpirationTime = data.tokenExpirationTime;
        }));
    }
    
    public checkOut(){
        if(!this.order.orderNumber){
            this.order.orderNumber = Date.now().toString();
        }
        return this.http.post("/api/orders", this.order, {
            headers: new HttpHeaders().set("Authorization", "Bearer " + this.token)
        })
            .pipe(map(response => {
            this.order = new Order();
        }));
    }
    public addToOrder(product: Product){
      
        var orderItem: OrderItem = this.order.items.find(item => item.productId == product.id);
        
        if(orderItem){
         orderItem.quantity++;   
        }else {
            orderItem = new OrderItem();
            orderItem.productId = product.id;
            orderItem.productArtist = product.artist;
            orderItem.productArtId = product.artId;
            orderItem.productCategory = product.category;
            orderItem.productSize = product.size;
            orderItem.productTitle = product.title;
            orderItem.unitPrice = product.price;
            orderItem.quantity = 1;
            
            this.order.items.push(orderItem);
        }
    }
}