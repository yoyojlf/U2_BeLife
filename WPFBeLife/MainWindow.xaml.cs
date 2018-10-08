﻿using MahApps.Metro.Controls;
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
using Belife.Negocio;

namespace WPFBeLife
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
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

        public MainWindow()
        {
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
            InitializeComponent();
        }
        
        

        private void Cliente_Click(object sender, RoutedEventArgs e)
        {

            Contrato_FO.IsOpen = false;
            Cliente_FO.IsOpen = true;
            Cliente_Listar.IsOpen = false;
            Contrato_Listar.IsOpen = false;
        }

        private void ListarCliente_Click(object sender, RoutedEventArgs e)
        {

            Contrato_FO.IsOpen = false;
            Cliente_FO.IsOpen = false;
            Cliente_Listar.IsOpen = true;
            Contrato_Listar.IsOpen = false;
        }

        private void Contrato_Click(object sender, RoutedEventArgs e)
        {
            Contrato_FO.IsOpen = true;
            Cliente_FO.IsOpen = false;
            Cliente_Listar.IsOpen = false;
            Contrato_Listar.IsOpen = false;
        }

        private void ListarContrato_Click(object sender, RoutedEventArgs e)
        {
            Contrato_FO.IsOpen = false;
            Cliente_FO.IsOpen = false;
            Cliente_Listar.IsOpen = false;
            Contrato_Listar.IsOpen = true;

        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Contrato_FO.IsOpen = false;
            Cliente_FO.IsOpen = false;
            Cliente_Listar.IsOpen = false;
            Contrato_Listar.IsOpen = false;
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

            Cliente cl = new Cliente()
            {
                RutCliente = txtRut.Text,
                Nombres = txtNombres.Text,
                Apellidos = txtApellidos.Text,
                FechaNacimiento = new DateTime(),


            };
            if (cl.Create()) ///Crear metodo en la clase cliente
            {
                MessageBox.Show("Cliente creada", "Información", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {
                MessageBox.Show("Cliente no pudo ser creado", "Atención", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }


        }



    }
}
