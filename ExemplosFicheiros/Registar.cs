using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ExemplosFicheiros
{
    public partial class Registar : Form
    {
        public Registar()
        {
            InitializeComponent();
        }

        private void Registar_Load(object sender, EventArgs e)
        {

        }

        private void txtNome_Click(object sender, EventArgs e)
        {
            txtNome.ForeColor= Color.Black;
            txtNome.Text= string.Empty;

        }

        private void btnRegistar_Click(object sender, EventArgs e)
        {
            //verificar os dados
            try
            {
                //ver se o nome tem 3 ou mais caracteres
                if (txtNome.Equals("") || txtNome.Text.Length<3)
                {
                    throw new Exception("Digitar nome com " +
                        "pelo menos 3 caracteres.");
                    
                }
                //ver se o username tem 3 ou mais caracteres
                if (txtUser.Equals("") || txtUser.Text.Length < 3)
                {
                    throw new Exception("Digitar Username com " +
                        "pelo menos 3 caracteres.");

                }
                //ver se a password tem 3 ou mais caracteres
                if (txtPass.Equals("") || txtPass.Text.Length < 3)
                {
                    throw new Exception("Digitar a password com " +
                        "pelo menos 3 caracteres.");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", 
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            //GRAVAR O REGISTO EM FICHEIRO
            try
            {
                Stream ficheiro = File.Open("utilizadores.txt", FileMode.Append, FileAccess.Write);
                StreamWriter registo = new StreamWriter(ficheiro);

                /*
                 registo.WriteLine(txtNome.Text);
                registo.WriteLine(txtEmail.Text);
                registo.WriteLine(txtUser.Text);
                registo.WriteLine(txtPass.Text);
                */

                registo.WriteLine(txtNome.Text + ";" + txtEmail.Text + ";" + txtUser.Text + ";" + txtPass.Text + ";");
                registo.Close();
                ficheiro.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
    
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpar();
        }

        private void Limpar()
        {
            txtNome.ResetText();
            txtUser.ResetText();    
            txtPass.ResetText();
            txtEmail.ResetText();
            txtNome.Focus();
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
