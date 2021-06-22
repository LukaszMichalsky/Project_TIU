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
import { AnimalTypeFormComponent } from './components/forms/animal-type/animal-type.component';
import { CategoryFormComponent } from './components/forms/category/category.component';
import { ZookeeperFormComponent } from './components/forms/zookeeper/zookeeper.component';
import { FoodFormComponent } from './components/forms/food/food.component';
import { ModalComponent } from './components/modal/modal.component';
import { FormsModule } from '@angular/forms';
import { MenuComponent } from './components/menu/menu.component';
import { MenuItemComponent } from './components/menu-item/menu-item.component';

@NgModule({
  declarations: [
    AppComponent,
    CategoryComponent,
    AnimalTypeComponent,
    FoodComponent,
    ZookeeperComponent,
    AnimalSpecimenFormComponent,
    AnimalTypeFormComponent,
    ModalComponent,
    MenuComponent,
    CategoryFormComponent,
    ZookeeperFormComponent,
    FoodFormComponent,
    MenuItemComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
