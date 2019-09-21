import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './components/home/home.component';
import { ImportDataComponent } from './components/import-data/import-data.component';
import { AdminappComponent } from './adminapp.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { AdminHeaderComponent } from './components/admin-header/admin-header.component';
import { RouterModule } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatRippleModule, MatOptionModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatTooltipModule } from '@angular/material/tooltip';
import { AdminService } from './services/admin.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { AddSaleComponent } from './components/add-sale/add-sale.component';
import { EditNewsComponent } from './components/edit-news/edit-news.component';
import { NewsListComponent } from './components/news-list/news-list.component';

@NgModule({
  declarations: [
    HomeComponent,
    ImportDataComponent,
    AdminappComponent,
    NavMenuComponent,
    AdminHeaderComponent,
    AddSaleComponent,
    EditNewsComponent,
    NewsListComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,

    MatRippleModule,
    MatSidenavModule,
    MatListModule,
    MatIconModule,
    MatMenuModule,
    MatToolbarModule,
    MatButtonModule,
    MatCardModule,
    MatFormFieldModule,
    MatOptionModule,
    MatSelectModule,
    MatAutocompleteModule,
    MatCheckboxModule,
    MatInputModule,
    MatRadioModule,
    MatSlideToggleModule,
    MatTooltipModule,
    MatSnackBarModule,
    RouterModule.forRoot([
      {
        path: 'Admin', component: AdminappComponent, children: [
          { path: '', redirectTo: 'Home', pathMatch: 'full' },
          { path: 'Home', component: HomeComponent },
          { path: 'ImportData', component: ImportDataComponent },
          { path: 'AddSale', component: AddSaleComponent },
          { path: 'EditSale/:id', component: EditNewsComponent },
          { path: 'SaleList', component: NewsListComponent },

          { path: '**', redirectTo: 'Home' },
        ]
      },



    ])
  ],
  providers: [
    AdminService,
    AdminappComponent
  ]
})
export class AdminAppModule { }
