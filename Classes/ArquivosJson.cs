using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace SorveteriaPolarMarrom.Classes
{
    internal class ArquivosJson
    {
        private static string caminhoArquivoPedidos = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pedidos.json");
        private static string caminhoArquivoProdutos = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "produtos.json");

        private static void VerificaArquivo(string caminhoArquivo)
        {
            if (File.Exists(caminhoArquivo))
            {
                // Limpa os dados
                File.WriteAllText(caminhoArquivo, string.Empty);
            }
            else
            {
                // Cria um arquivo
                using (FileStream fileStream = File.Create(caminhoArquivo))
                {
                    fileStream.Close();
                }
            }
        }

        public static void ExportarProdutosJson(List<Produto> listaDados)
        {
            VerificaArquivo(caminhoArquivoProdutos);

            // Transforma a lista em dados JSON
            string dadosJson = JsonConvert.SerializeObject(listaDados, Formatting.Indented);

            // Escreve a lista dentro do arquivo
            File.WriteAllText(caminhoArquivoProdutos, dadosJson);
        }

        public static void ExportarPedidosJson(List<Pedido> listaDados)
        {
            VerificaArquivo(caminhoArquivoPedidos);

            // Transforma a lista em dados JSON
            string dadosJson = JsonConvert.SerializeObject(listaDados, Formatting.Indented);

            // Escreve a lista dentro do arquivo
            File.WriteAllText(caminhoArquivoPedidos, dadosJson);
        }

        public static List<Produto> ImportarProdutosJson()
        {
            if (File.Exists(caminhoArquivoProdutos))
            {
                // Le os dados do arquivo json
                string dadosJson = File.ReadAllText(caminhoArquivoProdutos);

                List<Produto> listaDados = JsonConvert.DeserializeObject<List<Produto>>(dadosJson);

                return listaDados;
            }
            else
            {
                // Caso não tenha o arquivo, retorna uma lista em branco
                return new List<Produto>();
            }
        }

        public static List<Pedido> ImportarPedidosJson()
        {
            if (File.Exists(caminhoArquivoPedidos))
            {
                // Le os dados do arquivo json
                string dadosJson = File.ReadAllText(caminhoArquivoPedidos);

                List<Pedido> listaDados = JsonConvert.DeserializeObject<List<Pedido>>(dadosJson);

                return listaDados;
            }
            else
            {
                // Caso não tenha o arquivo, retorna uma lista em branco
                return new List<Pedido>();
            }
        }
    }
}
