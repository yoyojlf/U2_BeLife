using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDato;

namespace BibliotecaNegocio
{
    public class Cliente
    {

        #region Parametros
        string rut;
        string nombre;
        string apellido;
        DateTime fechaNacimiento;
        #endregion

        #region Propiedades
        public string RutCliente
        {
            set
            {
                if(value != string.Empty)
                {
                    rut = value;
                }
                else
                {
                    throw new ArgumentException("El rut no puede estar vacio culero!!!!");
                }
            }
            get
            {
                return rut;
            }
        }
        
        public string Nombres
        {
            get
            {
                return nombre;
            }
            set
            {
                if(value != string.Empty)
                {
                    nombre = value;
                }
                else
                {
                    throw new ArgumentException("El nombre no puede estar vacio culero!!!");
                }
            }
        }
        
        public string Apellidos
        {
            get
            {
                return apellido;
            }
            set
            {
                if(value != string.Empty)
                {
                    apellido = value;
                }
                else
                {
                    throw new ArgumentException("El apellido no puede estar vacio Culero!!!");
                }
            }
        }
        
        public DateTime FechaNacimiento
        {
            get
            {
                return fechaNacimiento;
            }
            set
            {
                TimeSpan a = DateTime.Today.Subtract(value); //estima el tiempo que ha trascurrido desde nacimiento
                double b = a.TotalDays / 365.25;
                int edad = Convert.ToInt32(b);
                DateTime fechamenos = DateTime.Today.AddYears(-18); //estimo la fecha de hoy hace 18 años atras
                TimeSpan diasMedad = DateTime.Today.Subtract(fechamenos); //estimo lapsus de tiempo que seria de mayor de edad
                                                                          //int anios = (int)(a.TotalDays/365);
                #region calculaEdad
                TimeSpan dif = DateTime.Now - value;
                int anios = (int)(Math.Truncate(dif.TotalDays/365.25));
                #endregion
                //if(edad >= 18)
                if (a.Days >= diasMedad.Days) //verifico que el tiempo transcurrido desde su nacimiento sea mayor o igual a 18 años, comparando la cantidad de dias 
                {
                    fechaNacimiento = value;
                    //throw new ArgumentException("la edad del wey es de: " + edad);
                    //throw new ArgumentException("La edad del wey es: "+anios);
                    //throw new ArgumentException("edad: " + Math.Truncate(b));
                    //throw new ArgumentException(anios + "");
                }
                else
                {
                    //throw new ArgumentException("La edad del cliente tiene que ser mayor o igual a 18 años!!! "); //+edad);
                    //throw new ArgumentException("la edad del wey es menor, edad: " + anios+" b: "+Math.Truncate(b));
                    throw new ArgumentException("NO REGISTRADO!!, El cliente tiene que ser mayor de 18 años!!!\nEdad: "+anios);
                }
            }
        }

        public int IdSexo { get; set; }
        public int IdEstadoCivil { get; set; }

        public Sexo Sexo { get; set; }

        public EstadoCivil EstadoCivil { get; set; }
        #endregion

        #region Constructor
        public Cliente()
        {
            this.Init();
        }
        #endregion

        #region inicializador
        private void Init()
        {
            nombre = string.Empty;
            apellido = string.Empty;
            rut = string.Empty;
            fechaNacimiento = new DateTime(1990, 1, 1);
            //Sexo = Sexo.Femenino;
            //EstadoCivil = EstadoCivil.Soltero;
        }
        #endregion

        #region Metodos
        public bool Read()
        {
            AccesoDato.BeLifeEntities Contexto = new AccesoDato.BeLifeEntities();
            try
            {
                AccesoDato.Cliente Clien = Contexto.Cliente.First(b => b.RutCliente == RutCliente);
                CommonBC.Syncronize(Clien, this);
                this.LeerDescripcion();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void LeerDescripcion()
        {
            BibliotecaNegocio.Sexo RegSex = new BibliotecaNegocio.Sexo();
            RegSex.IdSexo = this.IdSexo;
            if (RegSex.Read())
            {
                BibliotecaNegocio.Sexo Sex = new BibliotecaNegocio.Sexo();
                Sex.IdSexo = RegSex.IdSexo;
                Sex.Descripcion = RegSex.Descripcion;
                this.Sexo = Sex;
            }
            BibliotecaNegocio.EstadoCivil RegEst = new BibliotecaNegocio.EstadoCivil();
            RegEst.IdEstadoCivil = this.IdEstadoCivil;
            if (RegEst.Read())
            {
                BibliotecaNegocio.EstadoCivil Est = new BibliotecaNegocio.EstadoCivil();
                Est.IdEstadoCivil = RegEst.IdEstadoCivil;
                Est.Descripcion = RegEst.Descripcion;
                this.EstadoCivil = Est;
            }
        }

        public bool Create()
        {
            AccesoDato.BeLifeEntities Contexto = new AccesoDato.BeLifeEntities();
            AccesoDato.Cliente Clien = new AccesoDato.Cliente();
            try
            {
                CommonBC.Syncronize(this, Clien);
                Contexto.Cliente.Add(Clien);
                Contexto.SaveChanges();

                return true;
            }catch(Exception ex)
            {
                Contexto.Cliente.Remove(Clien);

                return false;
            }
        }

        public bool Delete()
        {
            AccesoDato.BeLifeEntities Contexto = new AccesoDato.BeLifeEntities();
            try
            {
                AccesoDato.Cliente Clien = Contexto.Cliente.First(b => b.RutCliente == RutCliente);
                Contexto.Cliente.Remove(Clien);
                Contexto.SaveChanges();

                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

        public List<Cliente> ReadAll()
        {
            AccesoDato.BeLifeEntities Contexto = new AccesoDato.BeLifeEntities();
            try
            {
                List<AccesoDato.Cliente> ListaBD = Contexto.Cliente.ToList<AccesoDato.Cliente>();
                //Lista de Salida
                List<Cliente> ListaBiblioteca = GenerarListaClientes(ListaBD);

                return ListaBiblioteca;
            }catch(Exception ex)
            {
                return new List<Cliente>();
            }
        }

        private List<Cliente> GenerarListaClientes(List<AccesoDato.Cliente> listaBD)
        {
            List<Cliente> ListaClie = new List<Cliente>();
            foreach(AccesoDato.Cliente Dato in listaBD)
            {
                Cliente Clien = new Cliente();
                CommonBC.Syncronize(Dato, Clien);
                Clien.LeerDescripcion();
                ListaClie.Add(Clien);
            }
            return ListaClie;
        }

        public bool Update()
        {
            AccesoDato.BeLifeEntities Contexto = new AccesoDato.BeLifeEntities();
            try
            {
                AccesoDato.Cliente Clien = Contexto.Cliente.First(b => b.RutCliente == RutCliente);
                CommonBC.Syncronize(this, Clien);

                Contexto.SaveChanges();

                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }
        #endregion

    }
}
