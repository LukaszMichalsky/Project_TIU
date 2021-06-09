import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CategoryComponent } from './category/category.component';
import { AnimaltypeComponent } from './animaltype/animaltype.component';
import { FoodComponent } from './food/food.component';
import { FoodassociationComponent } from './foodassociation/foodassociation.component';
import { ZookeeperComponent } from './zookeeper/zookeeper.component';
import { ZookeeperassociationComponent } from './zookeeperassociation/zookeeperassociation.component';

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
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
