import { Component, OnInit } from '@angular/core';

import { CalcularInvestimentoService } from '../../services' ;

import { FormBuilder, Validators, FormControl, FormGroup  } from '@angular/forms';  
import { Observable, throwError, concat, of } from 'rxjs';  
import { InvestimentoDto } from '../../models'; 
import { BrowserModule } from "@angular/platform-browser";
import { platformBrowserDynamic } from "@angular/platform-browser-dynamic";
import { map } from 'rxjs/operators';


@Component({
  selector: 'app-calcular-investimento',
  templateUrl: './calcular-investimento.component.html',
  styleUrls: ['./calcular-investimento.component.css']
})
export class CalcularInvestimentoComponent implements OnInit {

  formInvestimento: FormGroup;

  public colunasTabela: string[] = [ 'Nome', 'Prazo de Resgate', 'Valor Inicial', 'Taxa de Investimento', 
                              'Taxa do Banco', 'Valor Final Bruto', 'Valor Imposto', 'Valor Final Liquido'];
  public dadosTabela: InvestimentoDto[] = [];
  public allInvestimentosAtivos: InvestimentoDto[] = [];  
  public investimentoCalculado = {} as InvestimentoDto;
  public message: string = "";
  public loading: boolean = true;
  public tabelaCalculoInvestimentoVisible: boolean = false;

  constructor(private formbuilder: FormBuilder, private CalcularInvestimentoService : CalcularInvestimentoService){
    
    this.carregarTodosInvestimentosAtivos();
    this.formInvestimento = this.formbuilder.group({
      valorInvestimento: new FormControl(Number, [Validators.required]),
      prazoDeResgate: new FormControl(Number, [Validators.required])
    });
  }

  ngOnInit() : void {   
  }
  

  private carregarTodosInvestimentosAtivos() {  
    try{
      this.CalcularInvestimentoService.getAllInvestimentosAtivos().subscribe(resultado => {
      
        this.allInvestimentosAtivos = resultado;
    }); 
  }
  catch(err)
  {
    this.message = 'Erro durante o processo de carga de investimentos.'
  }
  } 

  private calcularInvestimento() {  
    try{
      var teste = JSON.stringify(this.investimentoCalculado);
      this.CalcularInvestimentoService.calcularInvestimento(this.investimentoCalculado).subscribe((data) => {
      this.loading = false;
      this.investimentoCalculado = data      
      this.message = '';
      this.preencherTabelaCalculoInvestimento();
    });
  }
  catch(err)
  {
    this.tabelaCalculoInvestimentoVisible = false;
    this.message = 'Erro durante o processo de cálculo de investimentos.'
  }
  }     

  limparTela() {  
    this.allInvestimentosAtivos = [];
    this.message = '';  
    this.investimentoCalculado  = {} as InvestimentoDto;
    this.tabelaCalculoInvestimentoVisible = false;
  }


private validarInformacoesParaCalculo = (formInvestimentoValue : any) : boolean => {
  if(!formInvestimentoValue || formInvestimentoValue.id <= 0)
  {
    this.message = "Necessário selecionar um investimento"
    return false;
  }
  else if(formInvestimentoValue.prazoResgateAplicacao <= 0)
  {
      this.message = "Prazo de resgate deve ser maior que zero."
      return false;    
  }
  else if(formInvestimentoValue.valorInicialInvestimento <= 0)
  {
      this.message = "Valor do investimento deve ser maior que zero."
      return false;    
  }

  this.investimentoCalculado.PrazoResgateAplicacao = Number(formInvestimentoValue.prazoDeResgate);
  this.investimentoCalculado.ValorInicialInvestimento = Number(formInvestimentoValue.valorInvestimento);

  return true;
}
public realizarCalculoInvestimento = (formInvestimentoValue : any) => {
  if (this.validarInformacoesParaCalculo(formInvestimentoValue)) {
    this.calcularInvestimento();
  }
}

private preencherTabelaCalculoInvestimento() {
  this.dadosTabela.push(this.investimentoCalculado);
}

}

