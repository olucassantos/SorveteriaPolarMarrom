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
    public partial class FrmMenu : Form
    {
        private Pedido pedidoAtual;
        private List<Produto> listaProdutos;
        List<Produto> produtosFiltrados;

        public FrmMenu()
        {
            InitializeComponent();
        }

        private void cadastrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmNovoProduto frmNovoProduto = new FrmNovoProduto();
            frmNovoProduto.Show();
        }

        private void sairToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "Deseja realmente sair?", 
                "Sair do sistema", 
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information
            );

            if (resultado == DialogResult.Yes )
            {
                Application.Exit();
            }
        }

        private void FrmMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            listaProdutos = ArquivosJson.ImportarProdutosJson();
            pedidoAtual = new Pedido();
        }

        private void txtBuscaProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Busca apenas a partir de 2 caracteres
            if (txtBuscaProduto.Text.Length < 2) return;

            produtosFiltrados = listaProdutos
            .Where(p => p.Nome.Contains(txtBuscaProduto.Text))
            .ToList();

            CarregaResultadosList(produtosFiltrados);
        }

        private void CarregaResultadosList(List<Produto> listaProdutos)
        {
            // Limpa o ListView antes de adicionar novos itens
            ltvResultadosBusca.Items.Clear();

            foreach (var produto in listaProdutos)
            {
                // Cria um novo ListViewItem
                ListViewItem item = new ListViewItem(produto.Nome);
                item.SubItems.Add(produto.Valor.ToString("C")); // Adiciona o preço como subitem

                // Adiciona o item ao ListView
                ltvResultadosBusca.Items.Add(item);
            }
        }

        private void btnAdicionarItem_Click(object sender, EventArgs e)
        {
            // Pega o item selecionado da busca
            if (ltvResultadosBusca.SelectedItems.Count > 0)
            {
                // Obtém o índice do item selecionado no ListView
                int selectedIndex = ltvResultadosBusca.SelectedItems[0].Index;

                // Acessa o produto correspondente na lista filtrada usando o índice
                Produto produtoSelecionado = produtosFiltrados[selectedIndex];

                CriaItemPedido(produtoSelecionado);
            }
        }

        private void CriaItemPedido(Produto produtoSelecionado)
        {
            Item item = new Item(produtoSelecionado);
            item.Quantidade = (int)numQuantidadeItem.Value;
            item.Valor = produtoSelecionado.Valor;

            // Adiciona o item no pedido
            pedidoAtual.Items.Add(item);

            AtualizaListaItensPedido();
        }

        private void AtualizaListaItensPedido()
        {
            lsbItensPedido.Items.Clear();
            lsbItensPedido.Items.AddRange(pedidoAtual.Items.ToArray());
        }
    }
}
