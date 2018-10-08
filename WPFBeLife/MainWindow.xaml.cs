using MahApps.Metro.Controls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;
using BibliotecaNegocio;

namespace WPFBeLife
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            MostrarClientes();
            opciones = new List<String>();
            opciones.Add("Plan 1");
            opciones.Add("Plan 2");
            opciones.Add("Plan 3");


            sexo = new List<String>();
            sexo.Add("Otro");
            sexo.Add("Femenino");
            sexo.Add("Masculino");

            estadocivil = new List<String>();
            estadocivil.Add("Soltero");
            estadocivil.Add("Casado");
            estadocivil.Add("Viudo");
            estadocivil.Add("Divorciado");

        }

        #region Data Clientes
        private void MostrarClientes()
        {
            Cliente Clie = new Cliente();
            //DgClientes.ItemsSource = Clie.ReadAll();
            //DgClientes.Items.Refresh();
            //CargaRut();
        }
        #endregion

        #region Win Cliente
        private void Limpiar()
        {
            TxtRut.Text = string.Empty;
            TxtNombre.Text = string.Empty;
            TxtApellido.Text = string.Empty;
            //lenado ComboBox
            //CbSexo.ItemsSource = Enum.GetValues(typeof(Sexo));
            Sexo ComboSexo = new Sexo();
            CbSexo.ItemsSource = ComboSexo.ReadAll();
            //CbEstadoCivil.ItemsSource = Enum.GetValues(typeof(EstadoCivil));
            EstadoCivil ComboEstados = new EstadoCivil();
            CbEstadoCivil.ItemsSource = ComboEstados.ReadAll();
            //CbSexo.SelectedValue = Sexo.Femenino;
            CbSexo.SelectedValuePath = "IdSexo";
            CbSexo.DisplayMemberPath = "Descripcion";
            //CbSexo.SelectedIndex = 0;
            CbSexo.SelectedValue = 1;
            //CbEstadoCivil.SelectedValue = EstadoCivil.Soltero;
            CbEstadoCivil.SelectedValuePath = "IdEstadoCivil";
            CbEstadoCivil.DisplayMemberPath = "Descripcion";
            CbEstadoCivil.SelectedValue = 1;
            DpFechaNacimiento.SelectedDate = new DateTime(1990, 1, 1);
            //MessageBox.Show(DateTime.Today.AddYears(-18)+"");
            //LbRut.ItemsSource = clientes;
            //LbRut.DisplayMemberPath = "Rut";

            MostrarClientes();
        }

        //boton de agregar Cliente
        private void BtnRegistrarCliente_Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente = new Cliente();
            Sexo Sex = new Sexo();
            EstadoCivil Est = new EstadoCivil();

            if (Buscar() == -1)
            {
                try
                {
                    cliente.RutCliente = TxtRut.Text;
                    cliente.Nombres = TxtNombre.Text;
                    cliente.Apellidos = TxtApellido.Text;
                    cliente.FechaNacimiento = (DateTime)DpFechaNacimiento.SelectedDate;
                    Sex.IdSexo = (int)CbSexo.SelectedValue;
                    Sex.Descripcion = CbSexo.Text;
                    cliente.IdSexo = (int)CbSexo.SelectedValue;
                    cliente.Sexo = Sex;
                    Est.IdEstadoCivil = (int)CbEstadoCivil.SelectedValue;
                    Est.Descripcion = CbEstadoCivil.Text;
                    cliente.IdEstadoCivil = (int)CbEstadoCivil.SelectedValue;
                    cliente.EstadoCivil = Est;
                    //agregar cliente
                    cliente.Create();
                    MostrarClientes();
                    MessageBox.Show("Se ha registrado exitosamente!! ", "Registro Exito!", MessageBoxButton.OK, MessageBoxImage.None);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message + "", "ERROR!!");
                }

            }
            else
            {
                MessageBox.Show("NO Se ha registrado exitosamente!!\nCliente Ya Existe!!", "Sin Exito!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private int Buscar()
        {
            int index = -1;
            int count = 0;
            List<Cliente> clientes = new List<Cliente>();
            
            foreach (Cliente x in clientes)
            {
                if (x.RutCliente.Equals(TxtRut.Text))
                {
                    index = count;
                    break;
                }
                count++;
            }
            return index;
        }

        private void BtnEliminarCliente_Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente = new Cliente();
            try
            {
                cliente.RutCliente = TxtRut.Text;
                cliente.Delete();
                MessageBox.Show("cliente eliminado correctamente!!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("cliente no eliminado correctamente!!!");
            }

            MostrarClientes();

        }

        //boton para consultar cliente y cargar datos si es que existe
        private void BtnConsultar_Click(object sender, RoutedEventArgs e)
        {
            CargarCliente(ConsultaCliente());
        }

        //consuta cliente TxtRut contra la base de datos
        private Cliente ConsultaCliente()
        {
            Cliente clie = new Cliente();
            try
            {
                if (!TxtRut.Text.Equals(string.Empty))
                {
                    clie.RutCliente = TxtRut.Text;
                    clie.Read();
                    return clie;
                }
                else
                {
                    throw new ArgumentException("Debe ingresar un rut para consultar!!!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Advertencia!");
                return clie;
            }

        }

        //cargar datos de cliente clie
        private void CargarCliente(Cliente Cli)
        {
            if (!Cli.RutCliente.Equals(string.Empty))
            {
                TxtRut.Text = Cli.RutCliente;
                TxtNombre.Text = Cli.Nombres;
                TxtApellido.Text = Cli.Apellidos;
                DpFechaNacimiento.SelectedDate = Cli.FechaNacimiento;
                CbSexo.SelectedValue = Cli.IdSexo;
                CbEstadoCivil.SelectedValue = Cli.IdEstadoCivil;
            }
            else
            {
                Limpiar();
            }
        }

        //boton actualiza
        private void BtnActualizarCliente_Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente = new Cliente();
            Sexo Sex = new Sexo();
            EstadoCivil Est = new EstadoCivil();

            try
            {
                cliente.RutCliente = TxtRut.Text;
                cliente.Nombres = TxtNombre.Text;
                cliente.Apellidos = TxtApellido.Text;
                cliente.FechaNacimiento = (DateTime)DpFechaNacimiento.SelectedDate;
                Sex.IdSexo = (int)CbSexo.SelectedValue;
                Sex.Descripcion = CbSexo.Text;
                cliente.IdSexo = (int)CbSexo.SelectedValue;
                cliente.Sexo = Sex;
                Est.IdEstadoCivil = (int)CbEstadoCivil.SelectedValue;
                Est.Descripcion = CbEstadoCivil.Text;
                cliente.IdEstadoCivil = (int)CbEstadoCivil.SelectedValue;
                cliente.EstadoCivil = Est;
                cliente.Update();
                MessageBox.Show("cliente rut: " + cliente.RutCliente + " actualizado correctamente!!!");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message + "");
            }


            MostrarClientes();
        }

        #endregion


        public List<String> opciones
        {
            get; set;
        }

        public List<String> sexo
        {
            get; set;
        }

        public List<String> estadocivil
        {
            get; set;
        }

        



        private void Cliente_Click(object sender, RoutedEventArgs e)
        {

            //Contrato_FO.IsOpen = false;
            Cliente_FO.IsOpen = true;
            Limpiar();
            //Cliente_Listar.IsOpen = false;
            //Contrato_Listar.IsOpen = false;
        }

        private void ListarCliente_Click(object sender, RoutedEventArgs e)
        {

            //Contrato_FO.IsOpen = false;
            Cliente_FO.IsOpen = false;
            //Cliente_Listar.IsOpen = true;
            //Contrato_Listar.IsOpen = false;
        }

        private void Contrato_Click(object sender, RoutedEventArgs e)
        {
            //Contrato_FO.IsOpen = true;
            Cliente_FO.IsOpen = false;
            //Cliente_Listar.IsOpen = false;
            //Contrato_Listar.IsOpen = false;
        }

        private void ListarContrato_Click(object sender, RoutedEventArgs e)
        {
            //Contrato_FO.IsOpen = false;
            Cliente_FO.IsOpen = false;
            //Cliente_Listar.IsOpen = false;
            //Contrato_Listar.IsOpen = true;

        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            //Contrato_FO.IsOpen = false;
            Cliente_FO.IsOpen = false;
            Limpiar();
            //Cliente_Listar.IsOpen = false;
            //Contrato_Listar.IsOpen = false;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CPlan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        { 



        }

    private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void AgregarCliente_Click(object sender, RoutedEventArgs e)
    {

        //Cliente cl = new Cliente()
        //{
        //    RutCliente = txtRut.Text,
        //    Nombres = txtNombres.Text,
        //    Apellidos = txtApellidos.Text,
        //    FechaNacimiento = new DateTime(),


        //}
        //if (cl.Create()) ///Crear metodo en la clase cliente
        //{
        //    MessageBox.Show("Cliente creada", "Información", MessageBoxButton.OK, MessageBoxImage.Information);

        //}
        //else
        //{
        //    MessageBox.Show("Cliente no pudo ser creado", "Atención", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //}


    }

        
    }

}