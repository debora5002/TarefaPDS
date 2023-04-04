using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppContatoForm
{
    public partial class ContatoForm : Form
    {
        private MySqlConnection conexao;

        private MySqlCommand comando;

        

        public ContatoForm()
        {
            InitializeComponent();
           
            

            Conexao();
        }


        private void Conexao()
        {
            string conexaoString = "server=localhost;database=app_contato_bd;user=root;password=root;port=3360";
            conexao = new MySqlConnection(conexaoString);
            comando = conexao.CreateCommand();

            conexao.Open();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if((!rdSexo1.Checked && !rdSexo2.Checked) || txtNome.Text =="" || txtEmail.Text == ""  || dateDataNascimento.Value >= DateTime.Now || txtTelefone.Text == "")
                {
                    MessageBox.Show("Marque uma opção!");


                }
                else
                {
                    var nome = txtNome.Text;
                    var email = txtEmail.Text;
                    var data_nascimento = dateDataNascimento.Text;

                    var sexo = "feminino" ;
                    if (rdSexo1.Checked)
                    {
                        sexo = "Masculino";
                    }

                    var telefone = txtTelefone.Text;

                    string query = "INSERT INTO Contato (nome_con, email_con, data_nasc_con, sexo_con, telefone_con) VALUES (@_nome, @_email, @_data_nascimento, @_sexo, @_telefone)";
                    var comando = new MySqlCommand(query, conexao);

                    comando.Parameters.AddWithValue("@_nome", nome);
                    comando.Parameters.AddWithValue("@_email", email);
                    comando.Parameters.AddWithValue("@_data_nascimento", data_nascimento);
                    comando.Parameters.AddWithValue("@_sexo", sexo);
                    comando.Parameters.AddWithValue("@_telefone", telefone);

                    comando.ExecuteNonQuery();
                    

                    var opcao = MessageBox.Show("Salvo com sucesso! \nDeseja realizar um novo cadastro? ", "Informação",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (opcao == DialogResult.Yes)
                    {
                        LimparImputs();
                    }
                    else
                    {
                        this.Close();
                    }
                    if (opcao == DialogResult.No)
                    {
                        MenuForm form = new MenuForm(); 
                        form.ShowDialog();
                    }

                   
                }
               
            }
            catch (Exception ex)
            {
             
              MessageBox.Show($"Ocorreram erros ao tentar salvar os dados!" + $"contate o suporte do sistema. (CAD 01)");
            }

            


        }
        private void LimparImputs()
        {
           
            txtNome.Clear();
            txtTelefone.Clear();
            dateDataNascimento.Clear();
            txtEmail.Clear();
            rdSexoGroup.Clear();
            rdSexo1.Focus();
        }
        
    }
}
