import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CategoryComponent } from './components/category/category.component';
import { AnimalTypeComponent } from './components/animaltype/animaltype.component';
import { FoodComponent } from './components/food/food.component';
import { ZookeeperComponent } from './components/zookeeper/zookeeper.component';
import { HttpClientModule } from "@angular/common/http";
import { AnimalSpecimenFormComponent } from './components/forms/animal-specimen/animal-specimen.component';

@NgModule({
  declarations: [
    AppComponent,
    CategoryComponent,
    AnimalTypeComponent,
    FoodComponent,
    ZookeeperComponent,
    AnimalSpecimenFormComponent
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
