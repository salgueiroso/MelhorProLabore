namespace ProLabore
{
    public class Faixa
    {
        public decimal Base { get; set; }
        public decimal Aliquota { get; set; }
    }

    public class ParametrosSimulacao
    {
        public decimal Faturamento { get; set; }
        public decimal? IRPFMinimo { get; set; }
        public decimal? IRPFMaximo { get; set; }
        public decimal? INSSMinimo { get; set; }
        public decimal? INSSMaximo { get; set; }
        public decimal? LiquidoMinimo { get; set; }
        public decimal? LiquidoMaximo { get; set; }
        public int? QtdResultados { get; set; }
        public int? SaltoResultados { get; set; }
        public decimal? Descontos { get; set; }
    }

    public class Resultado
    {
        public decimal Faturamento { get; set; }
        public decimal Bruto { get; set; }
        public decimal IRPF { get; set; }
        public decimal Liquido { get; set; }
        public decimal INSS { get; set; }
        public decimal CustoEmpresa { get; set; }
        public decimal INSSPatronal { get; set; }
    }
}