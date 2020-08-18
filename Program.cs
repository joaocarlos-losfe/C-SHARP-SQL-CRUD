using System;
using medicamentos.view;

namespace f_cli
{
    class Program
    {
         /* operações teste sqlite
            OperacoesSql operacoesSql = new OperacoesSql();
            string comando = null;
            //comando =  "CREATE TABLE Clientes( IDCliente INTEGER AUTO INCREMENT , NomeCliente TEXT NOT NULL, Telefone TEXT, Divida REAL NOT NULL, DataRegistro TEXT NOT NULL, UltimaDataQuitamento TEXT, FKMedicamento TEXT NOT NULL, PRIMARY KEY (\"NomeCliente\", \"Telefone\", \"IDCliente\"), FOREIGN KEY (FKMedicamento) REFERENCES Medicamentos (PKCodigoDebarras) );";
            //comando = "CREATE TABLE RegVendas(FKMedicamento TEXT NOT NULL, HoraVenda TEXT NOT NULL, DataVenda TEXT NOT NULL, Quantidade INTEGER, IDVenda INTEGER PRIMARY KEY AUTOINCREMENT, FOREIGN KEY (FKMedicamento) REFERENCES Medicamentos (PKCodigoDebarras) );";
            operacoesSql.comandoPersonalizado(comando);
        */
        static void Main(string[] args)
        {
            char key;
            var menu_medicamento = new Medicamentos(); // contido no namespace medicamentos.view
            
            while(true)
            {
                Console.Clear();
                Console.WriteLine("\n" + "  " + DateTime.Today.ToString("D") + " -------------------------------------- FARMA BASE MENU + ------------------------------------------------  ");
                Console.WriteLine("\n  1 - Pesquisar medicamento _ 3 - Registrar venda _ 4 - Gerencia de Medicamentos _ 5 - Gerencia de vendas _ 6 - Gerencia de Clientes  ");
                Console.Write("  > "); key = Console.ReadKey(true).KeyChar;

                switch(key)
                {
                    case '1':
                        menu_medicamento.pesquisarMedicamento();
                    break;

                    case '2':
                    break;

                    case '3':
                    break;

                    case '4':
                        menu_medicamento.MedicamentoMenu();
                    break;

                    case '5':
                    break;

                    default: 
                        Console.WriteLine("  Opção invalida");
                        Console.ReadKey();
                    break;
                }
            }
           
        }
    }
}
