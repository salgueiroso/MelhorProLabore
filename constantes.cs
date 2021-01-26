namespace ProLabore
{
    public static class Constantes
    {
        public static readonly Faixa[] FaixasIRPF = {
          new Faixa {Base=1903.98m, Aliquota=0m},
          new Faixa {Base=922.67m , Aliquota=0.075m},
          new Faixa {Base=924.40m, Aliquota=0.15m},
          new Faixa {Base=913.63m, Aliquota=0.225m},
          new Faixa {Base=decimal.MaxValue , Aliquota=0.275m}
        };


        public const decimal AliquotaSimples = 0.06m;
        public const decimal AliquotaINSS = 0.11m;
        public const decimal TetoINSS = 6351m;
        public const decimal AliquotaINSSPatronal = 0.2m;
    }
}