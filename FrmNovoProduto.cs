using SorveteriaPolarMarrom.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SorveteriaPolarMarrom
{
    public partial class FrmNovoProduto : Form
    {
        private List<Produto> listaProdutos;
        public FrmNovoProduto()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            SalvarProduto();
        }

        private void SalvarProduto()
        {
            // Validando se o campo nome tem valor
            if (txtNome.Text.Length == 0)
            {
                MessageBox.Show("Escreva um nome para o produto!");
                return;
            }

            // Validando se o campo valor não é zero
            if (numValor.Value == 0)
            {
                MessageBox.Show("Valor não pode ser zero!");
                return;
            }

            // Cria e salva o produto
            Produto produto = new Produto(txtNome.Text, txtDescricao.Text, numValor.Value);
            listaProdutos.Add(produto);

            MessageBox.Show("Produto salvo com sucesso!");

            // Salva os dados na lista
            ArquivosJson.ExportarProdutosJson(listaProdutos);

            DialogResult resultado = MessageBox.Show(
                "Deseja cadastrar outro produto?", 
                "Confirmação", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
            {
                // Limpa os campos
                txtNome.Clear();
                txtDescricao.Clear();
                numValor.Value = 0;

                txtNome.Focus();
            } 
            else
            {
                this.Close();
            }
        }

        private void FrmNovoProduto_Load(object sender, EventArgs e)
        {
            // Carrega a lista de produtos
            listaProdutos = (List<Produto>) ArquivosJson.ImportarProdutosJson();
        }
    }
}
