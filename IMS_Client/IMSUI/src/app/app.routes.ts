import { RouterModule, Routes } from '@angular/router';
import { ProductDetailsComponent } from './Core/Components/product-details/product-details.component';
import { ProductlistComponent } from './Core/Components/productlist/productlist.component';
import { ProductDashboardComponent } from './Core/Components/product-dashboard/product-dashboard.component';
import { CategoryDashboardComponent } from './Core/Components/category-dashboard/category-dashboard.component';
import { LoginComponent } from './Core/Components/login/login.component';
import { RegisterComponent } from './Core/Components/register/register.component';

import { CategoryListComponent } from './Core/Components/category-list/category-list.component';
import { AddNewProductComponent } from './Core/Components/add-new-product/add-new-product.component';
import { EditProductComponent } from './Core/Components/edit-product/edit-product.component';
import { AddCategoryComponent } from './Core/Components/add-category/add-category.component';
import { LoginTempComponent } from './Core/Components/login-temp/login-temp.component';





export const routes: Routes = [


    {
        path: 'api/ProductAPI/getAllCategories',
        component: CategoryDashboardComponent
    },
    { path: '', redirectTo: 'products', pathMatch: 'full' },
    { path: 'products', component: ProductlistComponent },
    { path: 'categories', component: CategoryListComponent },


    { path: '', component: ProductlistComponent },
    {
        path: 'manage/product/getAllCategories',
        component: CategoryDashboardComponent
    },

    {
        path: 'manage/product/edit/:productId',
        component: EditProductComponent
    },
    {
        path: 'manage/product/add',
        component: AddNewProductComponent
    },
    {
        path: 'manage/product',
        component: ProductDashboardComponent
    },
    {
        path: 'api/ProductAPI',
        component: ProductDashboardComponent
    },
    {
        path: 'login',
        component: LoginTempComponent
    },
    {
        path: 'register',
        component: RegisterComponent
    },
    {
        path: 'manage/category',
        component: CategoryDashboardComponent
    },
    {
        path: 'manage/category/addCategory',
        component: AddCategoryComponent
    },
    {
        path: 'product/:productId',
        component: ProductDetailsComponent
    }


];


