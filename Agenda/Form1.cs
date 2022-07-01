using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agenda
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void LimparCampos()
        {
            textnome.Clear();
            textMail.Clear();
            maskTelefone.Clear();
            btnAdicionar.Text = "Adicionar";
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {

            //Verrificar se o nome esta preenchido

            if (textnome.Text.Length == 0 )
            {
                MessageBox.Show("O nome é um campo obrigatorio!", "Nome", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                textnome.Focus();
                return;

            }
            //verificar email preenchido

            if (textMail.Text.Length == 0)
            {
                MessageBox.Show("O Email é um campo obrigatorio!", "Nome", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textMail.Focus();
                return;

            }

            if (btnAdicionar.Text.Equals("Actualizar"))
            {
                listNome.Items[listNome.SelectedIndex] = textnome.Text;
                listEmail.Items[listEmail.SelectedIndex] = textMail.Text;
                listTelefone.Items[listTelefone.SelectedIndex] = maskTelefone.Text;
                LimparCampos();
                return;
            }
            // adicionar os dados as listas

            listNome.Items.Add(textnome.Text);
            listEmail.Items.Add(textMail.Text);
            listTelefone.Items.Add(maskTelefone.Text);
            btnAlterar.Enabled = true;
            btnEliminar.Enabled = true;
            btnImprimir.Enabled = true;
            LimparCampos();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }



        private void listNome_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Sicronizar 
            listEmail.SelectedIndex = listNome.SelectedIndex;
            listTelefone.SelectedIndex = listNome.SelectedIndex;
        }

        private void listEmail_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Sicronizar 
            listNome.SelectedIndex = listEmail.SelectedIndex;
            listTelefone.SelectedIndex = listEmail.SelectedIndex;
        }

        private void listTelefone_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Sicronizar 
            listNome.SelectedIndex = listTelefone.SelectedIndex;
            listEmail.SelectedIndex = listTelefone.SelectedIndex;
        }
        private void VerificarLista()
        {
            if (listNome.Items.Count == 0)
            {
                btnAlterar.Enabled = false;
                btnEliminar.Enabled = false;
                btnImprimir.Enabled = false;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            VerificarLista();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if ( listNome.SelectedIndex < 0 )
            {
                MessageBox.Show("Nao selecionou nenhum item", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            textnome.Text = listNome.SelectedItem.ToString();
            textMail.Text = listEmail.SelectedItem.ToString();
            maskTelefone.Text = listTelefone.SelectedItem.ToString();
            btnAdicionar.Text = "Actualizar";
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (listNome.SelectedIndex < 0)
            {
                MessageBox.Show("Nao selecionou nenhum item", "Erro", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult Confirmar = MessageBox.Show("Confirma eliminar?", "Eliminar Contacto",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (Confirmar == DialogResult.Yes)
            {

                int posicao = listNome.SelectedIndex;
                listNome.Items.RemoveAt(posicao);
                listEmail.Items.RemoveAt(posicao);
                listTelefone.Items.RemoveAt(posicao);
                VerificarLista();
            }

            
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            //imprimir texto personalizado

            Font fonte1 = new Font("Verdana", 18);
            Font fonte2 = new Font("Verdana", 10);

            e.Graphics.DrawString("Lista de contactos", fonte1, new SolidBrush(Color.Blue), 100, 100);

            e.Graphics.DrawString("Nome", fonte2, new SolidBrush(Color.Black), 100, 150);
            e.Graphics.DrawString("Email", fonte2, new SolidBrush(Color.Black), 300, 150);
            e.Graphics.DrawString("Telefone", fonte2, new SolidBrush(Color.Black), 500, 150);

            int y = 180;
            for(int linha = 0; linha < listNome.Items.Count;linha++)
            {
                e.Graphics.DrawString(listNome.Items[linha].ToString(), fonte2, new SolidBrush(Color.Black), 100, y);
                e.Graphics.DrawString(listEmail.Items[linha].ToString(), fonte2, new SolidBrush(Color.Black), 300, y);
                e.Graphics.DrawString(listTelefone.Items[linha].ToString(), fonte2, new SolidBrush(Color.Black), 500, y);

                y += 15;
            }

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
