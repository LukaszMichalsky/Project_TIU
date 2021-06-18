import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CategoryComponent } from './components/category/category.component';
import { AnimaltypeComponent } from './components/animaltype/animaltype.component';
import { FoodComponent } from './components/food/food.component';
import { FoodassociationComponent } from './components/foodassociation/foodassociation.component';
import { ZookeeperComponent } from './components/zookeeper/zookeeper.component';
import { ZookeeperassociationComponent } from './components/zookeeperassociation/zookeeperassociation.component';
import { HttpClientModule } from "@angular/common/http"

@NgModule({
  declarations: [
    AppComponent,
    CategoryComponent,
    AnimaltypeComponent,
    FoodComponent,
    FoodassociationComponent,
    ZookeeperComponent,
    ZookeeperassociationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
