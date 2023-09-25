import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './core/components/login/login.component';
import { LoginGuard } from './core/security/login.guard';
import { HomeComponent } from './shared/layout/home/home.component';
import { RouteGuard } from './core/security/route.guard';

const routes: Routes = [
  { path:'login',  component: LoginComponent, canActivate: [LoginGuard]},
  { path: 'lists', loadChildren: () => import('./feature/todolist/todolist.module').then(m => m.TodolistModule), canActivate: [RouteGuard],},
  { path: 'profile', loadChildren: () => import('./feature/user/user.module').then(m => m.UserModule), canActivate: [RouteGuard],},
  { path: '', redirectTo: '', pathMatch: 'full' , component: HomeComponent},
  { path: '**', redirectTo: '', component: HomeComponent},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
