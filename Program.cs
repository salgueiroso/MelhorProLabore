using System;
using System.Linq;

namespace ProLabore
{
    class Program
    {
        static void Main(string[] args)
        {
            var parametros = new ParametrosSimulacao
            {
                Faturamento = 10000,
                LiquidoMinimo = 3500
            };


            var bestsConfs = new Simulacao().Simular(parametros);

            var bestConf = bestsConfs.FirstOrDefault();
            if (bestConf != null)
            {
                Console.WriteLine("::::: Melhor Pro-Labore :::::");
                Console.WriteLine($"Faturamento R$ {bestConf.Faturamento.ToString("0.00")}");
                Console.WriteLine("");
                Console.WriteLine("* Pro-Labore:");
                Console.WriteLine($" - Pro-Labore Bruto R$ {bestConf.Bruto.ToString("0.00")}");
                Console.WriteLine($" - Desc. INSS R$ {bestConf.INSS.ToString("0.00")}");
                Console.WriteLine($" - Desc. IRRF R$ {bestConf.IRPF.ToString("0.00")}");
                Console.WriteLine($" - Liquido R$ {bestConf.Liquido.ToString("0.00")}");

                Console.WriteLine("");
                Console.WriteLine("* Empresa:");
                Console.WriteLine($" - Custo para a empresa R$ {bestConf.CustoEmpresa.ToString("0.00")}");
                Console.WriteLine($" - Desc. INSS Patronal R$ {bestConf.INSSPatronal.ToString("0.00")}");
                Console.WriteLine($" - Lucro/Caixa R$ {(parametros.Faturamento - bestConf.CustoEmpresa).ToString("0.00")}");
            }

            //Console.WriteLine(bestsConfs.Where(x => (x.Bruto % 100) == 0).ToArray());

        }
    }


}
