import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AnimalTypeComponent } from './components/animaltype/animaltype.component';
import { AuthComponent } from './components/auth/auth.component';
import { CategoryComponent } from './components/category/category.component';
import { FoodComponent } from './components/food/food.component';
import { MenuComponent } from './components/menu/menu.component';
import { ZookeeperComponent } from './components/zookeeper/zookeeper.component';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  { path: "login", component: AuthComponent },
  { path: '', component: MenuComponent, canActivate: [AuthGuard] },
  { path: "category", component: CategoryComponent, canActivate: [AuthGuard] },
  { path: "zookeeper", component: ZookeeperComponent, canActivate: [AuthGuard] },
  { path: "food", component: FoodComponent, canActivate: [AuthGuard] },
  { path: "animaltype", component: AnimalTypeComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
