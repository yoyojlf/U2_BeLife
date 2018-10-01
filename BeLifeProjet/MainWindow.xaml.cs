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
using BibliotecaNegocio;

namespace BeLifeProjet
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //crear una lista
        //List<Cliente> Clientes = new List<Cliente>();
        //variable que maneja una coleccion de clientes
        ClienteCollection clientes = new ClienteCollection();
        
        public MainWindow()
        {
            InitializeComponent();
            //Inicializacion de los componentes
            this.Limpiar();
           
        }

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
            LbRut.ItemsSource = clientes;
            LbRut.DisplayMemberPath ="Rut";
            
            MostrarClientes();
        }

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
                    Sex.IdSexo = (int) CbSexo.SelectedValue;
                    Sex.Descripcion = CbSexo.Text;
                    cliente.IdSexo = (int) CbSexo.SelectedValue;
                    cliente.Sexo = Sex;
                    Est.IdEstadoCivil = (int)CbEstadoCivil.SelectedValue;
                    Est.Descripcion = CbEstadoCivil.Text;
                    cliente.IdEstadoCivil = (int)CbEstadoCivil.SelectedValue;
                    cliente.EstadoCivil = Est;
                    //agregar cliente
                    cliente.Create();
                    MostrarClientes();
                    MessageBox.Show("Se ha registrado exitosamente!! ", "Registro Exito!", MessageBoxButton.OK, MessageBoxImage.None);
                }catch(ArgumentException ex)
                {
                    MessageBox.Show(ex.Message + "","ERROR!!");
                }
                
            }
            else
            {
                MessageBox.Show("NO Se ha registrado exitosamente!!\nCliente Ya Existe!!", "Sin Exito!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            

        }

        private void MostrarClientes()
        {
            Cliente Clie = new Cliente();
            DgClientes.ItemsSource = Clie.ReadAll();
            DgClientes.Items.Refresh();
            CargaRut();
        }

        private void DgClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = 0;
            index = DgClientes.SelectedIndex;
            if (index != -1)
            {
                DgClientes.Items[index].ToString();
                Cliente Clie = new Cliente();
                Clie = (Cliente)DgClientes.Items[index];
                this.Limpiar();
                TxtRut.Text = Clie.RutCliente;
                TxtNombre.Text = Clie.Nombres;
                TxtApellido.Text = Clie.Apellidos;
                CbSexo.SelectedValue = Clie.Sexo.IdSexo;
                CbEstadoCivil.SelectedValue = Clie.EstadoCivil.IdEstadoCivil;
                DpFechaNacimiento.SelectedDate = Clie.FechaNacimiento;
            }
        }

        private void BtnEliminarCliente_Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente = new Cliente();
            try
            {
                cliente.RutCliente = TxtRut.Text;
                cliente.Delete();
                MessageBox.Show("cliente eliminado correctamente!!!");
            }catch(Exception ex)
            {
                MessageBox.Show("cliente no eliminado correctamente!!!");
            }
           
            MostrarClientes();
            
        }

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

        private int Buscar()
        {
            int index = -1;
            int count = 0;
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

        private void BtnConsultar_Click(object sender, RoutedEventArgs e)
        {
            //int index = Buscar();
            //if (index >= 0)
            //{
            //    Limpiar();
            //    TxtRut.Text = clientes[index].RutCliente;
            //    TxtNombre.Text = clientes[index].Nombres;
            //    TxtApellido.Text = clientes[index].Apellidos;
            //    CbSexo.SelectedValue = clientes[index].Sexo;
            //    CbEstadoCivil.SelectedValue = clientes[index].EstadoCivil;
            //    DpFechaNacimiento.SelectedDate = clientes[index].FechaNacimiento;
            //    MessageBox.Show("Cliente cargado y listo!!","cargado");
            //}
            //else
            //{
            //    MessageBox.Show("cliente No encontrado!!!!!!!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
            CargarCliente(ConsultaCliente());
            LbRut.Items.Refresh();
        }

        private void BtnCarga_Click(object sender, RoutedEventArgs e)
        {
            MostrarClientes();
        }

        private void BtnTarificar_Click(object sender, RoutedEventArgs e)
        {
            Tarificador Tar;
            double Tarifa = 0;
            try
            {
                Tar = new Tarificador((DateTime)DpFechaNacimiento.SelectedDate,(int) CbSexo.SelectedValue,(int) CbEstadoCivil.SelectedValue);
                Tarifa = Tar.CalcularPrimaBase(double.Parse(TxtBase.Text));
                MessageBox.Show("La prima Calculada es de: " + Tarifa.ToString(),"Prima");
            }catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnTarificador_Click(object sender, RoutedEventArgs e)
        {
            BeLifeProjet.Tarificator TarifWindows = new Tarificator((int)CbSexo.SelectedValue, (int)CbEstadoCivil.SelectedValue, (DateTime)DpFechaNacimiento.SelectedDate);
            
            TarifWindows.Owner = this;
            TarifWindows.ShowDialog();
        }

        private void CargaRut()
        {
            LbRut.ItemsSource = DgClientes.ItemsSource;
            LbRut.DisplayMemberPath = "RutCliente";
            LbRut.SelectedValuePath = "RutCliente";
            LbRut.Items.Refresh();
        }

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
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,"Advertencia!");
                return clie;
            }
           
        }

        private void LbRut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(LbRut.SelectedIndex != -1)
            {
                TxtRut.Text = LbRut.SelectedValue.ToString();
            }
        }
    }
}
