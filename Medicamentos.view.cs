using System;
using Microsoft.Data.Sqlite;

namespace medicamentos.view
{
    class Medicamentos
    {
        public OperacoesSql operacoes_sql = new OperacoesSql();
        char key;
        char confirmar;        
        public void MedicamentoMenu()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("\n         ----------------------------------------------- + Gerencia de Medicamentos + -----------------------------------------------  ");
                Console.WriteLine("\n  1 - Pesquisar medicamento _ 3 - Adicionar Medicamento _ 4 - Editar Medicamento _ 5 - Excluir Medicamento _ 0 - Voltar. \n");

                operacoes_sql.exibir_todos_os_campos();

                Console.Write("\n  > "); key = Console.ReadKey(true).KeyChar;

                if(key=='0')
                    break;

                switch(key)
                {
                    case '1':
                        pesquisarMedicamento();
                    break;

                    case '3':
                        adicionarMedicamento();
                    break;

                    case '4':
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

        public void pesquisarMedicamento()
        {
            string medicamentoREF;
            int ID;
            Console.Write("\n\n  Nome do medicamento ou codigo de barras "); medicamentoREF = Console.ReadLine().ToLower();

            ID = operacoes_sql.pesquisarBD(medicamentoREF);

            if(ID != -1)
            {
                Console.WriteLine("  Medicamento Encontrado...");
            }
            else
            {
                Console.WriteLine("  Não encontrado...");
            }

            Console.ReadKey();
            
        }

        public void adicionarMedicamento()
        {
            string NomeReferencia;  string Fornecedor ; float Miligramas = 0.0f; float Preco = 0.0f; float DescontoPorcentagem = 0.0f; int QuantidadeEstoque; string CodigoDeBarras;
            
            Console.Clear();
            Console.Write("\n  Nome de referencia.......: "); NomeReferencia = Console.ReadLine().ToLower();
            //Console.Write("\n  Nome Popular.............: "); NomePopular = Console.ReadLine().ToLower();
            Console.Write("\n  Fornecedor...............: "); Fornecedor = Console.ReadLine().ToLower();
            
            Console.Write("\n  Miligramas...............: Mg "); Miligramas =  float.Parse( Console.ReadLine() ) ;
            Console.Write("\n  Preço....................: R$ "); Preco = float.Parse( Console.ReadLine() );
            Console.Write("\n  Desconto em porcentagem..: % "); DescontoPorcentagem = float.Parse ( Console.ReadLine()) ;
            
            Console.Write("\n  Quantidade...............: "); QuantidadeEstoque = Convert.ToInt32 ( Console.ReadLine() );

            Console.Write("\n  Codigo de barras.........: "); CodigoDeBarras = Console.ReadLine().ToLower();

            Console.Write("\n  Nome de referencia.......: " + NomeReferencia); 
           // Console.Write("\n  Nome Popular.............: " + NomePopular); 
            Console.Write("\n  Fornecedor...............: " + Fornecedor);  
            Console.Write("\n  Miligramas...............: {0}", Miligramas);  
            Console.Write("\n  Preço....................: {0}", Preco);  
            Console.Write("\n  Desconto em porcentagem..: {0}", DescontoPorcentagem); 
            Console.Write("\n  Quantidade...............: " + QuantidadeEstoque);  
            Console.Write("\n  Codigo de barras.........: " + CodigoDeBarras);

            Console.WriteLine("  Salvar?  S [Sim] N [Não]");
            Console.Write("  > "); confirmar =  Console.ReadKey(true).KeyChar;

            if(confirmar=='S' || confirmar == 's')
            {
                operacoes_sql.salvarBD(NomeReferencia, Fornecedor, Miligramas, Preco, DescontoPorcentagem, QuantidadeEstoque, CodigoDeBarras);
            }
            else
                Console.WriteLine("  descartado...");

            Console.ReadKey(); 

        }

        public void editarMedicamento()
        {

        }
        public void excluirMedicamento()
        {

        }
    }

    class OperacoesSql
    {
        public string chamada_sql = null;
        public string caminho_de_conexao = "Data Source = data/database.db;"; // base de dados pode ser alterada
        
        public void sucesso()
        {
            Console.WriteLine("  ok..");
        }

        public int pesquisarBD(string medicamento)
        {
            int ok = -1;

            using (SqliteConnection conexao = new SqliteConnection())
            {
                conexao.ConnectionString = caminho_de_conexao;
                conexao.Open();

                using (SqliteCommand comando = new SqliteCommand())
                {
                    comando.CommandText = "SELECT ID, NomeReferencia, Fornecedor, Miligramas, Preco, DescontoPorcentagem, QuantidadeEstoque, CodigoDeBarras  FROM Medicamentos WHERE NomeReferencia = " + "'" + medicamento + "'" + "OR CodigoDeBarras = " + "'" + medicamento + "'";
                    comando.Connection = conexao;

                    using (SqliteDataReader campos = comando.ExecuteReader())
                    {
                        if(campos.Read())
                        {
                            ok = Convert.ToInt32(campos["ID"]);
                            Console.WriteLine("\n  ID: " + campos["ID"] + " Nome de Referencia: " + campos["NomeReferencia"] + " Fornecedor: " + campos["Fornecedor"] + " Miligramas: " + campos["Miligramas"] + " Preço: " + campos["Preco"] + " Desconto Porcentagem: " + campos["DescontoPorcentagem"] + " Quantidade no estoque: " + campos["QuantidadeEstoque"] + " Codigo de barras: " + campos["CodigoDeBarras"]);
                        }
                    }
                }

                conexao.Close();
            }

            return ok;
        }

        public void salvarBD(string NomeReferencia, string Fornecedor, float Miligramas, float Preco, float DescontoPorcentagem, int QuantidadeEstoque, string CodigoDeBarras)
        {
            using (SqliteConnection conexao = new SqliteConnection())
            {
                conexao.ConnectionString = caminho_de_conexao;
                conexao.Open();

                using (SqliteCommand comando = new SqliteCommand())
                {
                    comando.CommandText = "INSERT INTO Medicamentos (NomeReferencia, Fornecedor, Miligramas, Preco, DescontoPorcentagem, QuantidadeEstoque, CodigoDeBarras) " + "VALUES (" + "" + "'" + NomeReferencia + "'" + "," + "'" + Fornecedor + "'" + "," + "'" + Miligramas + "'" + "," + "'" + Preco + "'" + "," + "'" + DescontoPorcentagem + "'" + "," + "'" + QuantidadeEstoque + "'" + "," + "'" + CodigoDeBarras + "'" + ");";
                    comando.Connection = conexao;
                    comando.ExecuteNonQuery();
                }

                conexao.Close();
            }
                
            sucesso();
        }

        public void comandoPersonalizado(string comando_personalizado)
        {
            using (SqliteConnection conexao = new SqliteConnection())
            {
                conexao.ConnectionString = caminho_de_conexao;
                conexao.Open(); //abre a conexão

                using (SqliteCommand comando = new SqliteCommand())
                {
                    comando.CommandText = comando_personalizado; // aqui vai o comando
                    comando.Connection = conexao; // a conexão com o caminho
                    comando.ExecuteNonQuery(); //executar
                }

                conexao.Close(); //fecha a conexão
            }

            sucesso();
        }

        public void exibir_todos_os_campos()
        {
            using (SqliteConnection conexao = new SqliteConnection())
            {
                conexao.ConnectionString = caminho_de_conexao;
                conexao.Open(); //abre a conexão

                using (SqliteCommand comando = new SqliteCommand())
                {
                    comando.CommandText = "Select * from Medicamentos;"; // aqui vai o comando
                    comando.Connection = conexao; // a conexão com o caminho

                    using (SqliteDataReader campos = comando.ExecuteReader())
                    {
                        while (campos.Read())
                        {
                            Console.WriteLine("\n  ID: " + campos["ID"] + " Nome de Referencia: " + campos["NomeReferencia"] + " Fornecedor: " + campos["Fornecedor"] + " Miligramas: " + campos["Miligramas"] + " Preço: " + campos["Preco"] + " Desconto Porcentagem: " + campos["DescontoPorcentagem"] + " Quantidade no estoque: " + campos["QuantidadeEstoque"] + " Codigo de barras: " + campos["CodigoDeBarras"]);
                        }
                    }
                }

                conexao.Close();
            }

        }

    }

}