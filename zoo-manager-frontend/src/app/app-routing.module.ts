import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AnimalTypeComponent } from './components/animaltype/animaltype.component';
import { CategoryComponent } from './components/category/category.component';
import { FoodComponent } from './components/food/food.component';
import { ZookeeperComponent } from './components/zookeeper/zookeeper.component';

const routes: Routes = [
  { path: "category", component: CategoryComponent },
  { path: "zookeeper", component: ZookeeperComponent },
  { path: "food", component: FoodComponent },
  { path: "animaltype", component: AnimalTypeComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
