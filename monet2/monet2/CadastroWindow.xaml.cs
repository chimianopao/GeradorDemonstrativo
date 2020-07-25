using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace monet2 {
    /// <summary>
    /// Interaction logic for CadastroWindow.xaml
    /// </summary>
    /// 

  

    public partial class CadastroWindow : Window {

        string nome = "";
        string rua = "";
        string bairro = "";
        string cidade = "";
        string uf = "";
        string cep = "";
        string desconto = "";
        string telefone = "";
        Cadastro cadastro;
        List<Cadastro> listaRevendedoras = new List<Cadastro>();
        Cadastro escolhida;
        MainWindow mainWindow;
        string pathSQL = System.IO.Path.Combine(Environment.CurrentDirectory, @"sql\", "Monetti.db");

        public CadastroWindow(MainWindow mainW) {
            InitializeComponent();
            mainWindow = mainW;
            populaListaRevendedora();
        }

        private void buttonCadastrar_Click(object sender, RoutedEventArgs e)
        {
            if (this.textBoxNome.Text.Equals(""))
                MessageBox.Show("Preencha pelo menos o nome");
            else {
            nome = this.textBoxNome.Text;
            rua = this.textBoxRua.Text;
            bairro = this.textBoxBairro.Text;
            cidade = this.textBoxCidade.Text;
            uf = this.textBoxUF.Text;
            cep = this.textBoxCEP.Text;
            desconto = this.textBoxDesconto.Text;
            telefone = this.textBoxTelefone.Text;

            cadastro = new Cadastro(nome, rua, bairro, cidade, uf, cep, desconto, telefone);

            gravarDados();
            MessageBox.Show("Revendedora cadastrada com sucesso!");
            limpaCampos();

            }
        }

        public void limpaCampos() {
            this.textBoxNome.Text = "";
            this.textBoxRua.Text = "";
            this.textBoxBairro.Text = "";
            this.textBoxCidade.Text = "";
            this.textBoxUF.Text = "RS";
            this.textBoxCEP.Text = "";
            this.textBoxDesconto.Text = "20";
            this.textBoxTelefone.Text = "";
        }

        private void buttonVoltar_Click(object sender, RoutedEventArgs e) {
          //  MessageBox.Show("Test");
            
            mainWindow = new MainWindow(); //arrumar
            this.Close();
            mainWindow.Show();
        }


     



        private void CarregaDados() {
            DataTable dt = new DataTable();
            SQLiteConnection conn = null;
            String sql = "select * from Cadastros";
            String strConn = @"Data Source="+pathSQL;
            try {
                conn = new SQLiteConnection(strConn);
                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, strConn);
                da.Fill(dt);
             //   dgvAlunos.DataSource = dt.DefaultView;
            }
            catch (Exception ex) {
                MessageBox.Show("Erro :" + ex.Message);
            }
            finally {
                if (conn.State == ConnectionState.Open) {
                    conn.Close();
                }
            }
        }


        public void lerDados() {
            SQLiteConnection cnn;
            String strConn = @"Data Source=" + pathSQL;
            cnn = new SQLiteConnection(strConn);
            cnn.Open();


            try {
                using (SQLiteCommand cmd = cnn.CreateCommand()) {
                    cmd.CommandText = @"SELECT * from Cadastro where nome='asdasd'";
                    cmd.CommandType = System.Data.CommandType.Text;

                    SQLiteDataReader reader;
                    reader = cmd.ExecuteReader();
                    if (reader.Read()) {
                        nome = Convert.ToString(reader["Nome"]);
                        rua = Convert.ToString(reader["Rua"]);
                        bairro = Convert.ToString(reader["Bairro"]);
                        cidade = Convert.ToString(reader["Cidade"]);
                        uf = Convert.ToString(reader["Uf"]);
                        cep = Convert.ToString(reader["Cep"]);
                        desconto = Convert.ToString(reader["Desconto"]);
                        telefone = Convert.ToString(reader["Telefone"]); ;
                        return;
                    }
                }
            }
            catch (Exception) {
                throw;
            }

            cnn.Close();

        }


        public void gravarDados(){
            SQLiteConnection cnn;
            String strConn = @"Data Source=" + pathSQL;
            cnn = new SQLiteConnection(strConn);
            cnn.Open();

            try{
                using (SQLiteCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Cadastro (Nome,Rua,Bairro,Cidade,Uf,Cep,Desconto,Telefone) VALUES (@nome,@rua,@bairro,@cidade,@uf,@cep,@desconto,@telefone);";
                    
                    cmd.Parameters.AddWithValue("@nome", nome.ToUpper());
                    cmd.Parameters.AddWithValue("@rua", rua.ToUpper());
                    cmd.Parameters.AddWithValue("@bairro", bairro.ToUpper());
                    cmd.Parameters.AddWithValue("@cidade", cidade.ToUpper());
                    cmd.Parameters.AddWithValue("@uf", uf.ToUpper());
                    cmd.Parameters.AddWithValue("@cep", cep);
                    cmd.Parameters.AddWithValue("@desconto", desconto);
                    cmd.Parameters.AddWithValue("@telefone", telefone);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }

            }
            catch (Exception)
            {

                throw;
            }

            cnn.Close();


        }

        private void LerButton_Click(object sender, RoutedEventArgs e)
        {
            lerDados();
            this.textBoxNome.Text = nome;
            this.textBoxRua.Text = rua;
            this.textBoxBairro.Text = bairro;
            this.textBoxCidade.Text = cidade;
            this.textBoxUF.Text = uf;
            this.textBoxDesconto.Text = desconto;
            this.textBoxTelefone.Text = telefone;
            this.textBoxCEP.Text = cep;
        }

        private void comboBoxRevendedoraAlteracao_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            foreach (Cadastro cad in listaRevendedoras) {
                if (cad.nome.Equals(comboBoxRevendedoraAlteracao.SelectedItem.ToString())) {
                    escolhida = cad;

                    this.textBoxNome.Text = escolhida.nome;
                    this.textBoxRua.Text = escolhida.rua;
                    this.textBoxBairro.Text = escolhida.bairro;
                    this.textBoxCidade.Text = escolhida.cidade;
                    this.textBoxUF.Text = escolhida.uf;
                    this.textBoxDesconto.Text = escolhida.desconto;
                    this.textBoxTelefone.Text = escolhida.telefone;
                    this.textBoxCEP.Text = escolhida.cep;


                }
            }
            this.buttonCadastrar.Visibility = System.Windows.Visibility.Hidden;
            this.buttonAlterar.Visibility = System.Windows.Visibility.Visible;
        }


        public void atualizarDados() {
            nome = this.textBoxNome.Text;
            rua = this.textBoxRua.Text;
            bairro = this.textBoxBairro.Text;
            cidade = this.textBoxCidade.Text;
            uf = this.textBoxUF.Text;
            cep = this.textBoxCEP.Text;
            desconto = this.textBoxDesconto.Text;
            telefone = this.textBoxTelefone.Text;


            SQLiteConnection cnn;
            String strConn = @"Data Source=" + pathSQL;
            cnn = new SQLiteConnection(strConn);
            cnn.Open();

            try{
                using (SQLiteCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Cadastro SET Nome = @nome, Rua = @rua, Bairro = @bairro, Cidade = @cidade, Uf = @uf, Cep = @cep, Desconto = @desconto, Telefone = @telefone WHERE Nome = @escolhida";

                    cmd.Parameters.AddWithValue("@escolhida", escolhida.nome);
                    cmd.Parameters.AddWithValue("@nome", nome.ToUpper());
                    cmd.Parameters.AddWithValue("@rua", rua.ToUpper());
                    cmd.Parameters.AddWithValue("@bairro", bairro.ToUpper());
                    cmd.Parameters.AddWithValue("@cidade", cidade.ToUpper());
                    cmd.Parameters.AddWithValue("@uf", uf.ToUpper());
                    cmd.Parameters.AddWithValue("@cep", cep);
                    cmd.Parameters.AddWithValue("@desconto", desconto);
                    cmd.Parameters.AddWithValue("@telefone", telefone);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }

            }
            catch (Exception)
            {

                throw;
            }

            cnn.Close();




        }



        public void populaListaRevendedora() {

            string fileName = "Monetti.db";
            string pathSQL = System.IO.Path.Combine(Environment.CurrentDirectory, @"sql\", fileName);



            SQLiteConnection cnn;
            String strConn = @"Data Source=" + pathSQL;
            cnn = new SQLiteConnection(strConn);
            cnn.Open();
            try {
                using (SQLiteCommand cmd = cnn.CreateCommand()) {
                    cmd.CommandText = @"SELECT * from Cadastro";
                    cmd.CommandType = System.Data.CommandType.Text;

                    SQLiteDataReader reader;
                    reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        this.comboBoxRevendedoraAlteracao.Items.Add(Convert.ToString(reader["Nome"]));


                        cadastro = new Cadastro(Convert.ToString(reader["Nome"]),
                            Convert.ToString(reader["Rua"]),
                            Convert.ToString(reader["Bairro"]),
                            Convert.ToString(reader["Cidade"]),
                            Convert.ToString(reader["Uf"]),
                            Convert.ToString(reader["Cep"]),
                            Convert.ToString(reader["Desconto"]),
                            Convert.ToString(reader["Telefone"]));
                        listaRevendedoras.Add(cadastro);

                    }
                    reader.Dispose();
                    cmd.Dispose();
                }
            }
            catch (Exception) {
                throw;
            }

            cnn.Close();
            this.comboBoxRevendedoraAlteracao.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("",
            System.ComponentModel.ListSortDirection.Ascending));
        }

        private void ButtonAlterar_Click(object sender, RoutedEventArgs e) {
            atualizarDados();
            MessageBox.Show("Revendedora atualizada com sucesso!");
            limpaCampos();
            listaRevendedoras.Clear();
            this.comboBoxRevendedoraAlteracao.Items.Clear();
            populaListaRevendedora();
            this.buttonCadastrar.Visibility = System.Windows.Visibility.Visible;
            this.buttonAlterar.Visibility = System.Windows.Visibility.Hidden;
            this.checkBoxDesconto.IsChecked = false;
        }

        private void ButtonNovo_Click(object sender, RoutedEventArgs e) {
            limpaCampos();
            listaRevendedoras.Clear();
            this.comboBoxRevendedoraAlteracao.Items.Clear();
            populaListaRevendedora();
            this.buttonCadastrar.Visibility = System.Windows.Visibility.Visible;
            this.buttonAlterar.Visibility = System.Windows.Visibility.Hidden;
        }

        private void CheckBoxDesconto_Checked(object sender, RoutedEventArgs e) {
            this.textBoxDesconto.Text = "20+5";
        }

        private void checkBoxDesconto_Unchecked(object sender, RoutedEventArgs e) {
            this.textBoxDesconto.Text = "20";
        }

       
    }
}
