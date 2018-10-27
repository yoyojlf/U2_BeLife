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
        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            MostrarClientes();
            Prueba();
            CargaContratos();
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
        #endregion

        #region prueba
        private void Prueba()
        {
            //string[] collection1 = new string[] { "1", "7", "4", "8", "9" };
            //string[] collection2 = new string[] { "6", "1", "7", "8" };

            //var resultSet = collection1.Intersect<string>(collection2);

            //foreach (string s in resultSet)
            //{
            //    MessageBox.Show(s);
            //}
        }
        #endregion

        #region Data Clientes
        private void MostrarClientes()
        {
            Cliente Clie = new Cliente();
            DgClientes.ItemsSource = Clie.ReadAll();
            DgClientes.Items.Refresh();
            //CargaRut();
        }

        //filtros por sexo y estado civil
        private void BtnFiltrarClientes_Click(object sender, RoutedEventArgs e)
        {
            UltimateFilter();
            //Filtro();
            //FiltroSexo();
            //FiltroEstadoCivil();
        }
        //ultima version de filtro de Sexo & Estado civil
        private void UltimateFilter()
        {
            Cliente FiltroClie = new Cliente();
            try
            {
                if (!TxtRutList.Text.Equals(string.Empty))
                {
                    DgClientes.ItemsSource = FiltroClie.ReadAll().Where(r => r.RutCliente == TxtRutList.Text);
                    MessageBox.Show("Filtro por rut");
                    MessageBox.Show("" + DgClientes.Items.Count);
                    if (DgClientes.Items.Count == 0)
                    {
                        MessageBox.Show("no existe cliente con ese rut " + TxtRutList.Text);
                        DgClientes.ItemsSource = FiltroClie.ReadAll();
                    }
                }
                else
                {
                    if ((int)CbSexoListaCli.SelectedIndex > -1 && (int)CbSexoListaCli.SelectedValue != 0 && (int)CbEstadoCivilListaCli.SelectedIndex > -1 && (int)CbEstadoCivilListaCli.SelectedValue != 0)
                    {
                        //si ambos combo box estan alterados se asigna el valor de la id de sexo y estado civil
                        //a nuestro ojeto cliente (FiltroClie)
                        FiltroClie.IdSexo = (int)CbSexoListaCli.SelectedValue;
                        FiltroClie.IdEstadoCivil = (int)CbEstadoCivilListaCli.SelectedValue;
                        /*una vez asignados se procede a usar los elementos ReadAllBySexo y ReadAllByEstadoCivil*/
                        //La siguiente linea no funciona
                        //DgClientes.ItemsSource = FiltroClie.ReadAllBySexo().Intersect(FiltroClie.ReadAllByEstadoCivil() );
                        /* a continuacion el link donde encontramos la solucion para el filtro
                         * http://www.qualityinfosolutions.com/comparar-listas-de-objetos-utilizando-linq-c/ */
                        DgClientes.ItemsSource = (from s in FiltroClie.ReadAllBySexo()
                                                  where (from e in FiltroClie.ReadAllByEstadoCivil()
                                                         select e.RutCliente).Contains(s.RutCliente)
                                                  select s).Distinct().ToList();
                        //intentare un metodo nuevo
                        // lruits1.Where(product => !fruits2Names.Contains(product.Name));
                        //DgClientes.ItemsSource = FiltroClie.ReadAllBySexo().Where(a =>  FiltroClie.ReadAllByEstadoCivil().Contains(a));
                        //DgClientes.ItemsSource = FiltroClie.ReadAllBySexo().Join(FiltroClie.ReadAllByEstadoCivil(), a = > a.RutCliente = RutCliente);
                        MessageBox.Show("Filtro por estado civil y sexo");

                    }
                    else
                    {
                        if (((int)CbEstadoCivilListaCli.SelectedIndex > -1 && (int)CbEstadoCivilListaCli.SelectedValue != 0) || ((int)CbSexoListaCli.SelectedIndex > -1 && (int)CbSexoListaCli.SelectedValue != 0))
                        {
                            if (((int)CbEstadoCivilListaCli.SelectedIndex > -1 && (int)CbEstadoCivilListaCli.SelectedValue != 0))
                            {
                               
                                FiltroClie.IdEstadoCivil = (int)CbEstadoCivilListaCli.SelectedValue;
                                DgClientes.ItemsSource = FiltroClie.ReadAllByEstadoCivil();
                                MessageBox.Show("Filtro solo estado civil");
                            }
                            if(((int)CbSexoListaCli.SelectedIndex > -1 && (int)CbSexoListaCli.SelectedValue != 0))
                            {
                                FiltroClie.IdSexo = (int)CbSexoListaCli.SelectedValue;
                                
                                DgClientes.ItemsSource = FiltroClie.ReadAllBySexo();
                                MessageBox.Show("Filtro solo sexo");
                            }
                        }
                        else
                        {
                            DgClientes.ItemsSource = FiltroClie.ReadAll();
                            MessageBox.Show("sin Filtro");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Durante el filtrado se produjo una excepsion: Detalle --> " + ex);
            }
        }
        //filtro rut//
        private void Filtro()
        {
            Cliente sour = new Cliente();
            if (!TxtRutList.Text.Equals(string.Empty))
            {
                DgClientes.ItemsSource = sour.ReadAll().Where(r => r.RutCliente == TxtRutList.Text);
                
                MessageBox.Show("" + DgClientes.Items.Count);
                if (DgClientes.Items.Count == 0)
                {
                    MessageBox.Show("no existe cliente con ese rut "+TxtRutList.Text);
                    DgClientes.ItemsSource = sour.ReadAll();
                }
            }
            else
            {
                FiltroSexo();
                FiltroEstadoCivil();
            }
        }
        //filtro sexo
        private void CbSexoListaCli_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //FiltroSexo();

        }
        private void FiltroSexo()
        {
            Cliente clieSex = new Cliente();
            List<Cliente> listaSex = new List<Cliente>();
            listaSex = clieSex.ReadAll();
            try
            {
                MessageBox.Show("" + CbSexoListaCli.SelectedIndex);
                
                if ((int)CbSexoListaCli.SelectedIndex > -1&& (int)CbSexoListaCli.SelectedValue != 0)
                {
                    MessageBox.Show("" + (int)CbSexoListaCli.SelectedValue);
                    if ((int)CbEstadoCivilListaCli.SelectedIndex > -1 && (int)CbEstadoCivilListaCli.SelectedValue != 0)
                    {
                        DgClientes.ItemsSource = listaSex.Where(e => e.IdEstadoCivil == (int)CbEstadoCivilListaCli.SelectedValue).Where(s => s.IdSexo == (int)CbSexoListaCli.SelectedValue);

                    }
                    else
                    {
                        DgClientes.ItemsSource = listaSex.Where(s => s.IdSexo == (int)CbSexoListaCli.SelectedValue);
                    }
                    
                }
                else
                {
                    if ((int)CbEstadoCivilListaCli.SelectedIndex > -1 && (int)CbEstadoCivilListaCli.SelectedValue != 0)
                    {
                        FiltroEstadoCivil();
                    }
                    else
                    {
                        DgClientes.ItemsSource = listaSex;
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                DgClientes.ItemsSource = listaSex;
            }

        }

        //filtro estado civil
        private void CbEstadoCivilListaCli_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //FiltroEstadoCivil();
        }

        private void FiltroEstadoCivil()
        {
            Cliente clieEst = new Cliente();
            List<Cliente> listaEst = new List<Cliente>();

            listaEst = clieEst.ReadAll();
            try
            {
                
                MessageBox.Show("" + (int)CbSexoListaCli.SelectedIndex);
                if ((int)CbEstadoCivilListaCli.SelectedIndex > -1 && (int)CbEstadoCivilListaCli.SelectedValue != 0)
                {
                    if ((int)CbSexoListaCli.SelectedIndex > -1 && (int)CbSexoListaCli.SelectedValue != 0)
                    {
                        DgClientes.ItemsSource = listaEst.Where(e => e.IdEstadoCivil == (int)CbEstadoCivilListaCli.SelectedValue).Where(s => s.IdSexo == (int)CbSexoListaCli.SelectedValue);
                        
                    }
                    else
                    {
                        DgClientes.ItemsSource = listaEst.Where(e => e.IdEstadoCivil == (int)CbEstadoCivilListaCli.SelectedValue);
                    }

                }
                else
                {
                    if ((int)CbSexoListaCli.SelectedIndex > -1 && (int)CbSexoListaCli.SelectedValue != 0)
                    {
                        FiltroSexo();
                    }
                    else
                    {

                        DgClientes.ItemsSource = listaEst;
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                DgClientes.ItemsSource = listaEst;
            }

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
            //cargar combo box del filtro en lista de usuarios
            //Sexo
            List<Sexo> ListSexito = new List<Sexo>();
            Sexo sexito = new Sexo();
            ListSexito = ComboSexo.ReadAll();
            sexito.IdSexo = 0;
            sexito.Descripcion = "No Filtrar";
            ListSexito.Add(sexito);
            CbSexoListaCli.ItemsSource = ListSexito;

            
            CbSexoListaCli.SelectedValuePath = "IdSexo";
            CbSexoListaCli.DisplayMemberPath = "Descripcion";
            CbSexoListaCli.SelectedValue = 0;
            //Estado civil
            List<EstadoCivil> ListEstadito = new List<EstadoCivil>();
            EstadoCivil estadito = new EstadoCivil();
            ListEstadito = ComboEstados.ReadAll();
            estadito.IdEstadoCivil = 0;
            estadito.Descripcion = "No Filtrar";
            ListEstadito.Add(estadito);
            CbEstadoCivilListaCli.ItemsSource = ListEstadito;
            CbEstadoCivilListaCli.SelectedValuePath = "IdEstadoCivil";
            CbEstadoCivilListaCli.DisplayMemberPath = "Descripcion";
            CbEstadoCivilListaCli.SelectedValue = 0;

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
            Cliente clien = new Cliente();
            clientes = clien.ReadAll();
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
            //Commit de prueba sdhbchbcshb
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

        #region Win Contrato
        private void ClearWinContrato()
        {
            TxtNumeroContrato.Text = string.Empty;
            TxtContratoRut.Text = string.Empty;
            //cargar comboBox Plan
            Plan ComboContratoPlan = new Plan();
            
            CbContratoPlanes.ItemsSource = ComboContratoPlan.ReadAll();
            CbContratoPlanes.SelectedValuePath = "IdPlan";
            CbContratoPlanes.DisplayMemberPath = "Nombre";
            CbContratoPlanes.SelectedIndex = 0;
            TxtBkContratoObserva.Text = string.Empty;
            DpContratoInicio.SelectedDate = DateTime.Today;
            LbContratoTipoPlan.Content = CbContratoPlanes.Text;
            ChBContratoEstaVigente.IsChecked = true;
            ChBContratoSalud.IsChecked = false;
        }

        //buscar contrato
        private void BuscarContrato_Click(object sender, RoutedEventArgs e)
        {
            BibliotecaNegocio.Contrato wContrato = new Contrato();
            try
            {
                if (!TxtNumeroContrato.Text.Equals(string.Empty))
                {
                    wContrato.Numero = TxtNumeroContrato.Text;
                    if (wContrato.Read())
                    {
                        CargaContrato(wContrato);
                    }
                    else
                    {
                        MessageBox.Show("El contrato que busca no existe");
                    }
                }
                else
                {
                    MessageBox.Show("No se puede buscar si no ingresa un numero de Contrato");                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
        }
        //otener contrato
        private Contrato RecuperaContratoWin()
        {
            Contrato contra;
            try
            {
                //verificar que todos esten con algun dato
                if(!TxtContratoRut.Text.Equals(string.Empty)&& !CbContratoPlanes.SelectedValue.ToString().Equals(string.Empty)&&cargaClient())
                {
                    contra = new Contrato();
                    contra.FechaCreacion = DateTime.Today;
                    contra.RutCliente = TxtContratoRut.Text;
                    contra.CodigoPlan = CbContratoPlanes.SelectedValue.ToString();
                    contra.FechaInicioVigencia = DpContratoInicio.SelectedDate.Value;
                    contra.Vigente = ChBContratoEstaVigente.IsChecked.Value;
                    contra.DeclaracionSalud = ChBContratoSalud.IsChecked.Value;
                    contra.PrimaAnual =  ((double) Math.Truncate(double.Parse(LbPrimaAnual.Content.ToString())*100))/100;
                    contra.PrimaMensual = ((double) Math.Truncate(double.Parse(LbPrimaMensual.Content.ToString())*100))/100;
                    MessageBox.Show(contra.PrimaAnual + " " + contra.PrimaMensual);
                    contra.Observaciones = TxtBkContratoObserva.Text;
                    return contra;
                }else
                {
                    return null;
                }
              
                

               
                
                
                
                
                 
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        //carga contrato
        private void CargaContrato(Contrato contrato)
        {
            
            TxtContratoRut.Text = contrato.Cliente.RutCliente;
            //cargar comboBox Plan

            CbContratoPlanes.SelectedValue = contrato.Plan.IdPlan;
            TxtBkContratoObserva.Text = contrato.Observaciones;
            DpContratoInicio.SelectedDate = contrato.FechaInicioVigencia;
            LbContratoTipoPlan.Content = CbContratoPlanes.Text;
            ChBContratoEstaVigente.IsChecked = contrato.Vigente;
            ChBContratoSalud.IsChecked = contrato.DeclaracionSalud;
        }

        //text change
        private void TxtContratoRut_TextChanged(object sender, TextChangedEventArgs e)
        {
            //MessageBox.Show(TxtContratoRut.Text);
            cargaClient();
        }
        //carga cliente contrato
        private bool cargaClient()
        {
            Cliente clin = new Cliente();
            try
            {
                
                    clin.RutCliente = TxtContratoRut.Text;
                    if (clin.Read())
                    {
                        MessageBox.Show("si existe");
                        TxtContratoNombre.Text = clin.Nombres;
                        TxtContratoApellido.Text = clin.Apellidos;
                    return true;
                    }
                    else
                    {
                    //MessageBox.Show("else");
                        TxtContratoNombre.Text = "no";
                        TxtContratoApellido.Text = "encontrado";
                    return false;
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        private void TxtContratoRut_TouchEnter(object sender, TouchEventArgs e)
        {
            Cliente clin = new Cliente();
            try
            {
                if (TxtContratoRut.Text.Equals(string.Empty))
                {
                    clin.RutCliente = TxtContratoRut.Text;
                    if (clin.Read())
                    {
                        TxtContratoNombre.Text = clin.Nombres;
                        TxtContratoApellido.Text = clin.Apellidos;
                    }
                    else
                    {
                        TxtContratoNombre.Text = "no";
                        TxtContratoApellido.Text = "encontrado";
                    }
                }
                else
                {
                    TxtContratoNombre.Text = "Esta";
                    TxtContratoApellido.Text = "vacio";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Cambio combo plan
        private void CbContratoPlanes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangePlan();
        }

        //changed plan
        private void ChangePlan()
        {
            Plan plan = new Plan();
            Cliente clie = new Cliente();
            try
            {
                clie.RutCliente = TxtContratoRut.Text;

                Tarificador tari = new Tarificador();
                plan.IdPlan = CbContratoPlanes.SelectedValue.ToString();
                plan.Read();
                LbContratoTipoPlan.Content = plan.Nombre;
                LbPoliza.Content = plan.PolizaActual;
                if (clie.Read())
                {
                    tari.cliente = clie;
                    LbPrimaAnual.Content = tari.CalcularPrimaBase(plan.PrimaBase);
                    LbPrimaMensual.Content = tari.CalcularPrimaBase(plan.PrimaBase) / 12; 
                }
                MessageBox.Show(CbContratoPlanes.Text + " " + CbContratoPlanes.SelectedValue + " " + LbContratoTipoPlan.Content);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //agregar contrato
        private void AgregarContrato_Click(object sender, RoutedEventArgs e)
        {
            Contrato contrato = new Contrato();
            if (RecuperaContratoWin() != null)
            {
                contrato = RecuperaContratoWin();
                contrato.FinVigencia();
                contrato.Create();
                MessageBox.Show("contrato agregado");
            }
            else
            {
                MessageBox.Show("Error!!!");
            }
        }
        #endregion

        #region Data Contrato
        private void CargaContratos()
        {
            Contrato con = new Contrato();
            DgContratos.ItemsSource = con.ReadAll();
            DgContratos.Items.Refresh();
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




        #region Open Win
        private void Cliente_Click(object sender, RoutedEventArgs e)
        {

            Contrato_FO.IsOpen = false;
            Cliente_FO.IsOpen = true;
            Limpiar();
            Cliente_Listar.IsOpen = false;
            Contrato_Listar.IsOpen = false;
        }

        private void ListarCliente_Click(object sender, RoutedEventArgs e)
        {

            Contrato_FO.IsOpen = false;
            Cliente_FO.IsOpen = false;
            Cliente_Listar.IsOpen = true;
            Limpiar();
            Contrato_Listar.IsOpen = false;
        }

        private void Contrato_Click(object sender, RoutedEventArgs e)
        {
            Contrato_FO.IsOpen = true;
            Cliente_FO.IsOpen = false;
            ClearWinContrato();
            Cliente_Listar.IsOpen = false;
            Contrato_Listar.IsOpen = false;
        }

        private void ListarContrato_Click(object sender, RoutedEventArgs e)
        {
            Contrato_FO.IsOpen = false;
            Cliente_FO.IsOpen = false;
            Cliente_Listar.IsOpen = false;
            Contrato_Listar.IsOpen = true;
            CargaContratos();

        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Contrato_FO.IsOpen = false;
            Cliente_FO.IsOpen = false;
            Limpiar();
            Cliente_Listar.IsOpen = false;
            Contrato_Listar.IsOpen = false;
        }
        #endregion
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
 