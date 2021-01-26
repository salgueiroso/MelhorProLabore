using System;
using System.Linq;

namespace ProLabore
{
    public class Simulacao
    {
        private decimal FaixaAliquota(decimal faturamento)
        {
            var faixas = Constantes.FaixasIRPF;
            var remaing = faturamento;
            var imposto = 0m;
            for (var i = 0; remaing > 0; i++)
            {
                var faixa = faixas[i];
                var _base = remaing <= faixa.Base ? remaing : faixa.Base;
                imposto += _base * faixa.Aliquota;
                remaing -= _base;
            }
            return imposto / faturamento;
        }

        private decimal brutoINSS(decimal bruto)
          => Math.Min(Constantes.TetoINSS, bruto);

        public Resultado[] Simular(ParametrosSimulacao parametros)
        {
            int qtdSimulacoes = Decimal.ToInt32(Math.Ceiling(
              parametros.Faturamento * (1m - Constantes.AliquotaSimples))
              - (parametros.Descontos ?? 0m));

            return Enumerable.Range(0, qtdSimulacoes)
            .AsParallel()
            .Select(x => Convert.ToDecimal(x))
            .Where(x => (x / parametros.Faturamento) >= 0.28m)
            .Select(x => new
            {
                bruto = x,
                _base = x * (1 - Constantes.AliquotaINSS)
            })
            .Select(x => new
            {
                x.bruto,
                x._base,
                faixaAliquota = FaixaAliquota(x._base),
                inssPatronal = (x.bruto * Constantes.AliquotaINSSPatronal)
            })
            .Select(x => new Resultado
            {
                Faturamento = parametros.Faturamento,
                Bruto = x.bruto,
                INSS = (x.bruto > Constantes.TetoINSS ? Constantes.TetoINSS : x.bruto) * Constantes.AliquotaINSS,
                IRPF = x._base * x.faixaAliquota,
                Liquido = x._base * (1 - x.faixaAliquota),
                CustoEmpresa = x.bruto - (x.inssPatronal * -1m),
                INSSPatronal = x.inssPatronal
            })
            .Where(x => parametros.IRPFMinimo == null || x.IRPF >= parametros.IRPFMinimo)
            .Where(x => parametros.IRPFMaximo == null || x.IRPF <= parametros.IRPFMaximo)
            .Where(x => parametros.INSSMinimo == null || x.INSS >= parametros.INSSMinimo)
            .Where(x => parametros.INSSMaximo == null || x.INSS <= parametros.INSSMaximo)
            .Where(x => parametros.LiquidoMinimo == null || x.Liquido >= parametros.LiquidoMinimo)
            .Where(x => parametros.LiquidoMaximo == null || x.Liquido <= parametros.LiquidoMaximo)
            .OrderBy(x => x.Liquido)
            .Where((x, i) => parametros.SaltoResultados == null || (i % parametros.SaltoResultados) == 0)
            .ToArray()
            .Take(parametros.QtdResultados ?? int.MaxValue)
            .ToArray();
        }
    }
}