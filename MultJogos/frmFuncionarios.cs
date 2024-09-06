using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MosaicoSolutions.ViaCep;

namespace MultJogos
{
    public partial class frmFuncionarios : Form
    {
        //Criando variáveis para controle do menu
        const int MF_BYCOMMAND = 0X400;
        [DllImport("user32")]
        static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);
        [DllImport("user32")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32")]
        static extern int GetMenuItemCount(IntPtr hWnd);

        public frmFuncionarios()
        {
            InitializeComponent();
            desabilitarCampos();
        }

        private void frmFuncionarios_Load(object sender, EventArgs e)
        {
            IntPtr hMenu = GetSystemMenu(this.Handle, false);
            int MenuCount = GetMenuItemCount(hMenu) - 1;
            RemoveMenu(hMenu, MenuCount, MF_BYCOMMAND);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void gpbFuncionarios_Enter(object sender, EventArgs e)
        {

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            frmPesquisar abrir = new frmPesquisar();
            abrir.Show();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            frmMenuPrincipal abrir = new frmMenuPrincipal();
            abrir.Show();
            this.Hide();
        }

        //método para desabilitar os campos e botões;
        public void desabilitarCampos()
        {
            txtCodigo.Enabled = false;
            txtEndereco.Enabled = false;
            txtNome.Enabled = false;
            txtEmail.Enabled = false;
            txtBairro.Enabled = false;
            txtCidade.Enabled = false;
            txtNum.Enabled = false;
            mskCEP.Enabled = false;
            mskCPF.Enabled = false;
            mskTelefone.Enabled = false;
            cbbEstado.Enabled = false;
            btnAlterar.Enabled = false;
            btnCadastrar.Enabled = false;
            btnExcluir.Enabled = false;
            btnLimpar.Enabled = false;
        }
        public void habilitarCampos()
        {
            txtCodigo.Enabled = true;
            txtNome.Enabled = true;
            txtEmail.Enabled = true;
            mskCEP.Enabled = true;
            mskCPF.Enabled = true;
            mskTelefone.Enabled = true;
            btnAlterar.Enabled = true;
            btnCadastrar.Enabled = true;
            btnExcluir.Enabled = true;
            btnLimpar.Enabled = true;

            txtNome.Focus();
        }

        public void limparCampos()
        {
            txtCodigo.Clear();
            txtEndereco.Clear();
            txtNome.Clear();
            txtEmail.Clear();
            txtBairro.Clear();
            txtCidade.Clear();
            txtNum.Clear();
            mskCEP.Clear();
            mskCPF.Clear();
            mskTelefone.Clear();
            cbbEstado.Text = "";
            btnAlterar.Enabled = false;
            btnCadastrar.Enabled = false;
            btnExcluir.Enabled = false;
            btnLimpar.Enabled = false;
        }

        public void buscaCep(string cep)
        {
            try
            {
                var viaCepService = ViaCepService.Default();

                var endereco = viaCepService.ObterEndereco(cep);

                txtEndereco.Text = endereco.Logradouro;
                txtBairro.Text = endereco.Bairro;
                txtCidade.Text = endereco.Localidade;
                cbbEstado.Text = endereco.UF;

                txtNum.Enabled = true;
            }
            catch
            {
                MessageBox.Show("CEP não encontrado !",
                    "Sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtNum.Enabled = true;
                txtCidade.Enabled = true;
                txtEndereco.Enabled = true;
                txtBairro.Enabled = true;
                cbbEstado.Enabled = true;
            }
        }
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            limparCampos();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if(txtNome.Text.Equals("") || txtEndereco.Text.Equals("") || txtEmail.Text.Equals("") || txtCidade.Text.Equals("") || txtBairro.Text.Equals("") || txtCodigo.Text.Equals("") || mskCEP.Text.Equals("     -") || mskCPF.Text.Equals("   .   .   -") || mskTelefone.Text.Equals("(  )     -") || cbbEstado.Text.Equals("") )
            {
                MessageBox.Show("Preencha todos os campos",
                    "Sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show("Cadastrado com sucesso !",
                    "Sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            habilitarCampos();
            btnNovo.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
        }

        private void mskCEP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscaCep(mskCEP.Text);
                txtNum.Focus();
            }
        }
    }
}
