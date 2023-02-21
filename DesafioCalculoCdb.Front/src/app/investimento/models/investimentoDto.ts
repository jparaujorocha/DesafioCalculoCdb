export interface InvestimentoDto {
    id: number;
    nome: string;
    prazoResgateAplicacao: number;
    valorInicialInvestimento: number;
    valorFinalInvestimentoLiquido: number;
    valorFinalInvestimentoBruto: number;
    valorImposto: number
    valorTaxaBanco: number;
    valorTaxaInvestimento: number;
    listInvestimentoMensalDto: InvestimentoMensalDto[];
}

export interface InvestimentoMensalDto
{
    numeroMes: number;
    valorInicialMensal: number;
    valorFinalMensal: number;
}