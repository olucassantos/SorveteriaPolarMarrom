using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace SorveteriaPolarMarrom.Classes
{
    internal class Item
    {
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorTotal
        {
            get
            {
                return Quantidade * Valor;
            }
        }

        public Item(Produto produto)
        {
            Produto = produto;
        }

        public override string ToString()
        {
            return $"{Produto.Nome} - {Quantidade} - {Valor.ToString("C")}";
        }
    }
}
