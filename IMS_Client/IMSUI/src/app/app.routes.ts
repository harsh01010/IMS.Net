import { RouterModule, Routes } from '@angular/router';
import { ProductDetailsComponent } from './Core/Components/product-details/product-details.component';
import { ProductlistComponent } from './Core/Components/productlist/productlist.component';
import { ProductDashboardComponent } from './Core/Components/product-dashboard/product-dashboard.component';
import { CategoryDashboardComponent } from './Core/Components/category-dashboard/category-dashboard.component';
import { CategoryListComponent } from './Core/Components/category-list/category-list.component';
import { AddNewProductComponent } from './Core/Components/add-new-product/add-new-product.component';
import { EditProductComponent } from './Core/Components/edit-product/edit-product.component';





export const routes: Routes = [
    { path: '', redirectTo: 'products', pathMatch: 'full' },
    { path: 'products', component: ProductlistComponent },
    { path: 'categories', component: CategoryListComponent },


    { path: '', component: ProductlistComponent },
    {
        path: 'api/ProductAPI/getAllCategories',
        component: CategoryDashboardComponent
    },
    {
        path: 'api/ProductAPI/edit/:productId',
        component: EditProductComponent
    },
    {
        path: 'api/ProductAPI/add',
        component: AddNewProductComponent
    },
    {
        path: 'manage/category',
        component: CategoryDashboardComponent
    },

    {
        path: 'product/:productId',
        component: ProductDetailsComponent
    },
    {
        path: 'manage/product',
        component: ProductDashboardComponent
    }

]; 4


