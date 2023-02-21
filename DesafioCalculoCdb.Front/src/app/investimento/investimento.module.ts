import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CalcularInvestimentoComponent } from './components';
import { CalcularInvestimentoService } from './services';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule  } from '@angular/material/card';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatInputModule } from '@angular/material/input';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatToolbarModule   } from '@angular/material/toolbar';
import {MatSelectModule} from '@angular/material/select';
import { MatTableModule } from '@angular/material/table'  

   
import { Pipe } from "@angular/core";
import {
  ReactiveFormsModule,
  FormsModule
} from "@angular/forms";
import { CurrencyMaskModule } from "ng2-currency-mask";



@NgModule({
  declarations: [
    CalcularInvestimentoComponent
  ],
  imports: [
    CommonModule ,
    BrowserAnimationsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatMenuModule,
    MatDatepickerModule,
    MatIconModule,
    MatCardModule,
    MatSidenavModule,
    MatTooltipModule,
    MatToolbarModule,
    CurrencyMaskModule,
    ReactiveFormsModule,
    FormsModule,
    MatSelectModule,
    MatTableModule
  ],
  exports: [CalcularInvestimentoComponent, MatTableModule],
  providers: [CalcularInvestimentoService]
})
export class InvestimentoModule { }
