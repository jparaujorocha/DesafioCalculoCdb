export class InvestimentoDto {

    public InvestimentoDto()
    {
        
    }
    Id: number = 0;
    Nome: string = '';
    PrazoResgateAplicacao: number = 0;
    ValorInicialInvestimento: number = 0;
    ValorFinalInvestimentoLiquido: number = 0;
    ValorFinalInvestimentoBruto: number = 0;
    ValorImposto: number = 0;
    ValorTaxaBanco: number = 0;
    ValorTaxaInvestimento: number = 0;
    ListInvestimentoMensalDto: InvestimentoMensalDto[] = [];
}

export class InvestimentoMensalDto
{
    NumeroMes: number = 0;
    ValorInicialMensal: number = 0;
    ValorFinalMensal: number = 0;
}