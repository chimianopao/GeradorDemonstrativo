using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.ComponentModel;

namespace monet2 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        bool especial = false;
        double valorBruto;
        double desconto;
        double valorDescontado;
        double mostruario;
        double outros;
        double frete;
        double chequeCorreio;
        double valorLiquido;
        Cadastro cadastro;
        List<Cadastro> listaRevendedoras = new List<Cadastro>();
        Cadastro escolhida;


        public MainWindow() {
            
            InitializeComponent();
            populaListaRevendedora();
            
            //pdfando();
        }


        public void populaListaRevendedora()
        {

            string fileName = "Monetti.db";
            string pathSQL = System.IO.Path.Combine(Environment.CurrentDirectory, @"sql\", fileName);



            SQLiteConnection cnn;
            String strConn = @"Data Source=" + pathSQL;
            cnn = new SQLiteConnection(strConn);
            cnn.Open();
            try
            {
                using (SQLiteCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * from Cadastro";
                    cmd.CommandType = System.Data.CommandType.Text;

                    SQLiteDataReader reader;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        this.comboBoxRevendedora.Items.Add(Convert.ToString(reader["Nome"]));
                       
                     
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
            catch (Exception)
            {
                throw;
            }

            cnn.Close();
            this.comboBoxRevendedora.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("",
            System.ComponentModel.ListSortDirection.Ascending));
        }


        public void pdfando(){
            string fileName = escolhida.nome + ".pdf";
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, @"pdf\", fileName);


        //    System.IO.FileStream fs = new FileStream("C:\\Users\\Theilor\\Desktop\\monet2\\monet2\\pdf" + "\\" + escolhida.Nome +".pdf", FileMode.Create);
            System.IO.FileStream fs = null;
            try {	
                fs = new FileStream(path, FileMode.Create); }
            catch (Exception e) {
                
                MessageBox.Show("Feche o demonstrativo (PDF) que está aberto antes de clicar no botão calcular.", e.Message);	}




            if (fs != null) {

                // Create an instance of the document class which represents the PDF document itself.
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                // Create an instance to the PDF file by creating an instance of the PDF 
                // Writer class using the document and the filestrem in the constructor.

                PdfWriter writer = PdfWriter.GetInstance(document, fs);

                // Add meta information to the document

                document.AddAuthor("Monetti");

                document.AddCreator("Theilor");

                document.AddKeywords("Demonstrativo");

                document.AddSubject("Pedido");

                document.AddTitle("Demonstrativo de Pedido");

                int[] lista = { 1, 2, 3 };
                foreach (int item in lista.Where(n => n >= 2)) {

                }


                // Open the document to enable you to write to the document

                document.Open();

                PdfContentByte cb = writer.DirectContent;
                cb.BeginText();
                BaseFont f_cb = BaseFont.CreateFont("c:\\windows\\fonts\\arial.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                BaseFont f_cn = BaseFont.CreateFont("c:\\windows\\fonts\\arial.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb.SetFontAndSize(f_cn, 12);
                cb.SetTextMatrix(10, 800); // Left, Top

                // Cria a data
                CultureInfo culture = new CultureInfo("pt-BR");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;

                int dia = DateTime.Now.Day;
                int ano = DateTime.Now.Year;
                string mes = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(DateTime.Now.Month));
                string diasemana = culture.TextInfo.ToTitleCase(dtfi.GetDayName(DateTime.Now.DayOfWeek));
                string dataExtenso = diasemana + ", " + dia + " de " + mes + " de " + ano;

                iTextSharp.text.Paragraph parag = new iTextSharp.text.Paragraph(new Chunk("Demonstrativo de Pedido Demillus                      ", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.UNDERLINE)));
                parag.Alignment = Element.ALIGN_JUSTIFIED_ALL;
                parag.Add(dataExtenso);

                document.Add(parag);
                document.Add(new iTextSharp.text.Paragraph(escolhida.nome));
                document.Add(new iTextSharp.text.Paragraph("End. " + escolhida.rua));
                document.Add(new iTextSharp.text.Paragraph("Cid: " + escolhida.cidade + " - " + escolhida.uf));

                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Desc: " + this.textBoxDesconto.Text + "%", 570, 775, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Bairro: " + escolhida.bairro, 570, 757, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Telefone: " + escolhida.telefone, 570, 740, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "CEP: " + escolhida.cep, 325, 740, 0);


                string sValorBruto = valorBruto.ToString("n2");
                string.Format("{0:0,0.00}", sValorBruto);
                string sMostruario = mostruario.ToString("n2");
                string.Format("{0:0,0.00}", sMostruario);
                string sOutros = outros.ToString("n2");
                string.Format("{0:0,0.00}", sOutros);
                string sFrete = frete.ToString("n2");
                string.Format("{0:0,0.00}", sFrete);
                string sChequeCorreio = chequeCorreio.ToString("n2");
                string.Format("{0:0,0.00}", sChequeCorreio);
                string sValorDescontado = valorDescontado.ToString("n2");
                string.Format("{0:0,0.00}", sValorDescontado);
                string sValorLiquido = valorLiquido.ToString("n2");
                string.Format("{0:0,0.00}", sValorLiquido);

                document.Add(new iTextSharp.text.Paragraph(" ")); //soh uma linha em branco
                document.Add(new iTextSharp.text.Paragraph(new Chunk("         RESUMO DO PEDIDO", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD))));
                document.Add(new iTextSharp.text.Paragraph(" + Valor do pedido: R$ " + sValorBruto));
                document.Add(new iTextSharp.text.Paragraph(" + P.Estoque/Mostruário: R$ " + sMostruario));
                document.Add(new iTextSharp.text.Paragraph(" + Outros: R$ " + sOutros));
                document.Add(new iTextSharp.text.Paragraph(" + Taxa Postal: R$ " + sFrete));
                document.Add(new iTextSharp.text.Paragraph(" -  Cheque Correio: R$ " + sChequeCorreio));
                document.Add(new iTextSharp.text.Paragraph(" -  Desconto: R$ " + sValorDescontado));

                var redListTextFont = FontFactory.GetFont("Arial", 12, BaseColor.RED);

                var titleChunk = new Chunk(" = Líquido à Pagar: R$ " + sValorLiquido, redListTextFont);
                document.Add(new iTextSharp.text.Paragraph(titleChunk));

                int row = 704;
                cb.SetFontAndSize(f_cn, 12);
                cb.SetTextMatrix(340, 704);
                cb.ShowText("MONETTI CATÁLOGOS");

                row -= 12;
                cb.SetTextMatrix(270, 665);
                cb.ShowText("Av Assis Brasil Nº 1993 sala 201 - Porto Alegre - RS");

                cb.SetTextMatrix(290, 605);
                cb.ShowText("Telefones: (51) 3061-5028, (51) 3061-3940");

                cb.SetTextMatrix(330, 586);
                cb.ShowText("Whatsapp: (51) 99430-9436");

                cb.SetTextMatrix(10, 700);

                cb.EndText();

                OnEndPage(writer, document);
                // Close the document

                document.Close();
                // Close the writer instance

                writer.Close();
                // Always close open filehandles explicity
                fs.Close();

            }

        }

        public void OnEndPage(PdfWriter writer, Document document) {
            var content = writer.DirectContent;
            var pageBorderRect = new iTextSharp.text.Rectangle(document.PageSize);

            pageBorderRect.Left += 250;
            pageBorderRect.Right -= document.RightMargin;
            pageBorderRect.Top -= 120;
            pageBorderRect.Bottom += 570;

            content.SetColorStroke(BaseColor.BLACK);
            content.Rectangle(pageBorderRect.Left, pageBorderRect.Bottom, pageBorderRect.Width, pageBorderRect.Height);
            content.Stroke();

            //---------------------------------------------

            var content2 = writer.DirectContent;
            var pageBorderRect2 = new iTextSharp.text.Rectangle(document.PageSize);

            pageBorderRect2.Left += document.LeftMargin;
            pageBorderRect2.Right -= document.RightMargin;
            pageBorderRect2.Top -= 120;
            pageBorderRect2.Bottom += 570;

            content2.SetColorStroke(BaseColor.BLACK);
            content2.Rectangle(pageBorderRect2.Left, pageBorderRect2.Bottom, pageBorderRect2.Width, pageBorderRect2.Height);
            content2.Stroke();
        }


        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e) {
        }

        private void RichTextBox_TextChanged_1(object sender, TextChangedEventArgs e) {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {
           
        }

        private void buttonCalcular_Click(object sender, RoutedEventArgs e) {

            if (comboBoxRevendedora.SelectedItem == null)
            {
                MessageBox.Show("Selecione uma revendedora");
            }
            else
            {

                if (this.textBoxValorBruto.Text != "")
                {
                    bool boolValorBruto = double.TryParse(this.textBoxValorBruto.Text, out valorBruto);
                    if (!boolValorBruto)
                        MessageBox.Show("Valor Bruto Inválido.", "Erro");
                }
                else valorBruto = 0;

                if (this.textBoxDesconto.Text != "")
                {
                    if(this.textBoxDesconto.Text.Equals("20+5")){
                        especial = true;
                    } 
                    else{
                    bool boolDesconto = double.TryParse(this.textBoxDesconto.Text, out desconto);
                    if (!boolDesconto)
                        MessageBox.Show("Desconto Inválido.", "Erro");

                    valorDescontado = valorBruto * (desconto / 100);
                    }
                }
                else desconto = 0;

                if (this.textBoxMostruario.Text != "")
                {
                    bool boolMostruario = double.TryParse(this.textBoxMostruario.Text, out mostruario);
                    if (!boolMostruario)
                        MessageBox.Show("Valor do Mostruário Inválido.", "Erro");
                }
                else mostruario = 0;

                if (this.textBoxOutros.Text != "")
                {
                    bool boolOutros = double.TryParse(this.textBoxOutros.Text, out outros);
                    if (!boolOutros)
                        MessageBox.Show("Valor de Outros Inválido.", "Erro");
                }
                else outros = 0;

                if (this.textBoxFrete.Text != "")
                {
                    bool boolFrete = double.TryParse(this.textBoxFrete.Text, out frete);
                    if (!boolFrete)
                        MessageBox.Show("Valor do Frete Inválido.", "Erro");
                }
                else frete = 0;

                if (this.textBoxChequeCorreio.Text != "")
                {
                    bool boolChequeCorreio = double.TryParse(this.textBoxChequeCorreio.Text, out chequeCorreio);
                    if (!boolChequeCorreio)
                        MessageBox.Show("Valor do Cheque Correio Inválido.", "Erro");
                }
                else chequeCorreio = 0;

                if (especial == true) {
                    double temp = valorBruto - valorBruto * 0.2;
                    valorLiquido =  (temp - temp*0.05 ) + mostruario + outros + frete - chequeCorreio;
                }
                else if (desconto != 0)
                    valorLiquido = (valorBruto - valorBruto * (desconto / 100)) + mostruario + outros + frete - chequeCorreio;
                else
                    valorLiquido = (valorBruto + mostruario + outros + frete - chequeCorreio);

                this.textBoxValorLiquido.Text = valorLiquido.ToString();

                pdfando();
                this.buttonAbrirPDF.Visibility = System.Windows.Visibility.Visible;
                especial = false;


            }



        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e) {

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e) {

        }

        private void textBoxChequeCorreio_TextChanged(object sender, TextChangedEventArgs e) {

        }

        private void buttonAbrirPDF_Click(object sender, RoutedEventArgs e) {
            string fileName = escolhida.nome + ".pdf";
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, @"pdf\", fileName);
            System.Diagnostics.Process.Start(path);
        }

        private void comboBoxRevendedora_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            foreach (Cadastro cad in listaRevendedoras)
            {
                if (cad.nome.Equals(comboBoxRevendedora.SelectedItem.ToString()))
                {
                    escolhida = cad;
                    this.textBoxDesconto.Text = escolhida.desconto;
                    this.cepLabel.Content = escolhida.cep;
                }
            }
        }

        private void cadRevendedoraButton_Click(object sender, RoutedEventArgs e)
        {
            CadastroWindow cadastro = new CadastroWindow(this);
            this.Close();
            cadastro.ShowDialog();
        }

        private void buttonEtiquetas_Click(object sender, RoutedEventArgs e) {
            EtiquetasWindow etiquetas = new EtiquetasWindow(this, listaRevendedoras);
            this.Close();
            etiquetas.ShowDialog();
        }
    }
}
