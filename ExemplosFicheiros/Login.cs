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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpar();
        }

        private void Limpar()
        {
            txtUser.ResetText();
            txtPass.ResetText();
            txtUser.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {



            //verificar as credenciais
            try
            {
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

            //LEITURA DO FICHEIRO
            bool autenticado = false;
            try
            {
                string path = Directory.GetCurrentDirectory() + "\\backup";

                //se a pasta não existir vamos criá-la
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                
                string caminho = path + "\\utilizadores.txt";
                //se o ficheiro náo existir podemos criá-lo
                //no entanto ele ficará vazio (sem informação)
                if (!File.Exists(caminho))
                {
                    using (FileStream ficheironovo = File.Create(caminho)) 
                    { 
                        ficheironovo.WriteByte(0);
                        ficheironovo.Close();
                    }
                }

                //continuamos o processo de autenticação
                Stream ficheiro = File.Open(caminho, FileMode.Open, FileAccess.Read);
                StreamReader registo = new StreamReader(ficheiro);

                string linha = registo.ReadLine();
                string[] dados = linha.Split(';');

                while (linha !=null)
                {
                    if (txtUser.Text == dados[0] && txtPass.Text == dados[1])
                    {
                        autenticado = true;
                        break;
                    }
                    linha = registo.ReadLine();
                    dados = linha.Split(';');
                }

                registo.Close();
                ficheiro.Close();
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Pasta de utilizadores não existe.",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Ficheiro de utilizadores não encontrado.", 
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (autenticado)
            {
                MessageBox.Show("Autenticado com sucesso.", "Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Credenciais fornecidas não são válidas.", "Login",
                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //FIM

        }
    }
}
