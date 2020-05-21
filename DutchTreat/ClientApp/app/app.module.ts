// Modules
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule} from "@angular/common/http";
import {RouterModule} from "@angular/router";
import {FormsModule} from "@angular/forms";
import { DrumsModule } from './drums/drums.module';

// Components - Dutch Treat
import {AppComponent} from './app.component';
import {ProductComponent} from "./component/product/product.component";
import {CartComponent} from "./component/cart/cart.component";
import {ShopComponent} from "./component/shop/shop.component";
import {CheckoutComponent} from "./component/checkout/checkout.component";
import {LoginComponent} from "./login/login.component";
// Components - Drums
import {DrumsComponent} from "./drums/drums.component";

// Services
import {DataService} from "./shared/dataService";




let routes = [
    {path: "shop", component: ShopComponent},
    {path: "checkout", component: CheckoutComponent},
    {path: "login", component: LoginComponent},
    {path: "", component: DrumsComponent}
]

@NgModule({
  declarations: [
      AppComponent,
      ProductComponent,
      CartComponent,
      ShopComponent,
      CheckoutComponent,
      LoginComponent
  ],
    imports: [
        BrowserModule,
        HttpClientModule,
        RouterModule.forRoot(routes, {
            useHash: true,
            enableTracing: false // for Debugging of the Routes
        }),
        FormsModule,
        DrumsModule
    ],
  providers: [DataService],
  bootstrap: [AppComponent]
})
export class AppModule { }
