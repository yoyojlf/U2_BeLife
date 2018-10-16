using System;
using System.Collections.Generic;
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
using BibliotecaNegocio;

namespace BeLifeProjet
{
    /// <summary>
    /// Lógica de interacción para Tarificator.xaml
    /// </summary>
    public partial class Tarificator : Window
    {
        public Tarificator()
        {
            InitializeComponent();
            Init();
        }

        public Tarificator(int idSex,int idEst, DateTime fechaNac)
        {
            InitializeComponent();
            Init();
            CbSexo.SelectedValue = idSex;
            CbEstadoCivil.SelectedValue = idEst;
            DpFechaNac.SelectedDate = fechaNac;
        }

        private void Init()
        {
            //creacion de objeto sexo
            Sexo cbSex = new Sexo();
            //carga de combo box obteniendo sexo de objeto sexo con readall()
            CbSexo.ItemsSource = cbSex.ReadAll();
            //se define el valor de los items por el id de sexo
            CbSexo.SelectedValuePath = "IdSexo";
            //se define lo mostrado en pantalla por cada item como Descripcion
            CbSexo.DisplayMemberPath = "Descripcion";
            //se selecciona por defecto 
            CbSexo.SelectedValue = 1;
            //ahora se hará lo mismo para los estados civiles
            EstadoCivil cbEst = new EstadoCivil();
            CbEstadoCivil.ItemsSource = cbEst.ReadAll();
            CbEstadoCivil.SelectedValuePath = "IdEstadoCivil";
            CbEstadoCivil.DisplayMemberPath = "Descripcion";
            DpFechaNac.SelectedDate = new DateTime(1990, 01, 01);
            TxtBase.Text = "0.0";
            TxtPrima.Text = "0.0 UF";
        }

        private void BtnCalcular_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (double.Parse(TxtBase.Text)>=0)
                {
                    //BibliotecaNegocio.Tarificador Tari = new Tarificador((DateTime) DpFechaNac.SelectedDate, (int)CbSexo.SelectedValue, (int)CbEstadoCivil.SelectedValue);
                    //TxtPrima.Text = Tari.CalcularPrimaBase(double.Parse(TxtBase.Text))+" UF";
                }
                else
                {
                    throw new ArgumentException("No se aceptan valores NEGATIVOS!!");
                }
            }catch(Exception ex)
            {
                MessageBox.Show("ERROR al calcular: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
