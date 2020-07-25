using FastMember;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for EtiquetasWindow.xaml
    /// </summary>
    public partial class EtiquetasWindow : Window {

        MainWindow mainWindow;
        List<Cadastro> listaRevendedoras2;
        bool isChecked;

        public EtiquetasWindow(MainWindow mainW, List<Cadastro> listaRevendedoras) {
            InitializeComponent();
            mainWindow = mainW;
            listaRevendedoras2 = listaRevendedoras;
            populaTabela();
           // seila();
        }


        private void populaTabela() {
            DataTable table = new DataTable();
            using (var reader = ObjectReader.Create(listaRevendedoras2)) {
                table.Load(reader);
            }
            this.tabela.ItemsSource = table.DefaultView;
        }

        private void tabela_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void seila() {

            List<Cadastro> listaRevendedoras3 = new List<Cadastro>();
            int aaa = 0;
            //tabela.
            foreach (DataRowView row in this.tabela.Items) {
              //  checkboxTabela.
               string bota = row.Row.Field<Object>(0).ToString();
            //    if (Convert.ToBoolean(grdDisplayAll.Rows[i].Cells[0].Value) == true)
            //{
            //    id = grdDisplayAll.Rows[i].Cells["Id"].Value;
               int numero = checkboxTabela.DisplayIndex;
               string estado = row.Row.RowState.ToString();
             //  isChecked = row.Row.Field<bool>("isChecked");
               string vamove = row.Row.Field<Object>(0).ToString();
                string ola = row.Row.Field<string>(aaa); aaa++;
                    string nome = row.Row.Field<string>("nome");
                    string rua = row.Row.Field<string>("rua");
                    string bairro = row.Row.Field<string>("bairro");
                    string cidade = row.Row.Field<string>("cidade");
                    string uf = row.Row.Field<string>("uf");
                    string cep = row.Row.Field<string>("cep");

                    Cadastro selecionada = new Cadastro(nome, rua, bairro, cidade, uf, cep);

                    listaRevendedoras3.Add(selecionada);
                
            
                //if(checkboxTabela. == true;){
                //  }
                // row.Cell[CheckBoxColumn1.Name].Value = true;
            }

        }

        private void buttonImprimir_Click(object sender, RoutedEventArgs e) {

            seila();



        }
    }
}
