import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';  
import { HttpHeaders } from '@angular/common/http';  
import { Observable } from 'rxjs';
import { InvestimentoDto } from '../../models';  
import { map } from 'rxjs/operators';


var httpOptions = {headers: new HttpHeaders({"Content-Type": "application/json; charset=UTF-8"})};

@Injectable({
  providedIn: 'root'
})
export class CalcularInvestimentoService {

  url = 'https://localhost:44315/Investimentos/';  

  constructor(private http: HttpClient) { }

  getAllInvestimentosAtivos(  ): Observable<InvestimentoDto[]> {  
    
    return this.http.get<InvestimentoDto[]>(this.url + "GetRecuperarInvestimentosAtivos/").pipe(
      map((res:InvestimentoDto[]) => res)
    );;
  }

  calcularInvestimento(investimento: InvestimentoDto): Observable<InvestimentoDto> {
    return this.http.post<InvestimentoDto>(this.url + "PostCalcularInvestimentos/", investimento
    , httpOptions);  
  }    

}
