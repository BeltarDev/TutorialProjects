import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ProductsService } from './products.service';

@Component({
  selector: 'products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit, OnDestroy{
  title = 'Udemy-Angular';
  productName = '';
  products = [];
  isDisabled = true;
  private productsSubscription : Subscription;
  
  constructor(public productsService: ProductsService) {
    setTimeout(() => {
    this.isDisabled = false;  
    },3000)
  }
  ngOnInit(): void {
    this.products = this.productsService.getProducts();
    this.productsSubscription = this.productsService.productsUpdated.subscribe(() => {
    this.products = this.productsService.getProducts();
    });
  }
  ngOnDestroy(): void {
    this.productsSubscription.unsubscribe();
  }
  
  onAddProduct(form){
    if (form.valid){
    this.productsService.addProduct(form.value.productName)
    }
  }

  // onProductRemove(productName: string){
  // }
}
