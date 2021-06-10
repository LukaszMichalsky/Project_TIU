import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AnimaltypeComponent } from './animaltype/animaltype.component';
import { CategoryComponent } from './category/category.component';
import { FoodComponent } from './food/food.component';
import { ZookeeperComponent } from './zookeeper/zookeeper.component';

const routes: Routes = [
  {path: "category", component: CategoryComponent},
  {path: "zookeeper",component: ZookeeperComponent},
  {path: "food",component: FoodComponent},
  {path: "animaltype", component: AnimaltypeComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
