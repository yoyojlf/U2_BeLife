using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaNegocio
{
    public class Contrato
    {
        public string Numero { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string RutCliente { get; set; }
        public string CodigoPlan { get; set; }
        public DateTime FechaInicioVigencia { get; set; }
        public DateTime FechaFinVigencia { get; set; }
        public bool Vigente { get; set; }
        public bool DeclaracionSalud { get; set; }
        public double PrimaAnual { get; set; }
        public double PrimaMensual { get; set; }
        public string Observaciones { get; set; }

        public Cliente Cliente { get; set; }
        public Plan Plan { get; set; }

        #region Constructor
        public Contrato()
        {
            Init();
        }

        #region Iniciador
        private void Init()
        {
            Numero = String.Empty;
            FechaCreacion = new DateTime();
            CodigoPlan = String.Empty;
            FechaInicioVigencia = new DateTime();
            FechaFinVigencia = new DateTime();
            Vigente = false;
            DeclaracionSalud = false;
            PrimaAnual = 0f;
            PrimaMensual = 0f;
            Observaciones = string.Empty;
        }
        #endregion

        #endregion

        #region Metodos
        //creacion de numero contrato
        private string ObtNumContrato()
        {

            int mes = DateTime.Now.Month;
            int dia = DateTime.Now.Day;
            int hora = DateTime.Now.Hour;
            int minu = DateTime.Now.Minute;
            int seg = DateTime.Now.Second;

            string anno = DateTime.Now.Year + "";
            string mess, dias, horr, minus, segu;

            if (mes < 10) { mess = "0" + mes; } else { mess = mes + ""; }
            if (dia < 10) { dias = "0" + dia; } else { dias = dia + ""; }
            if (hora < 10) { horr = "0" + hora; } else { horr = hora + ""; }
            if (minu < 10) { minus = "0" + minu; } else { minus = minu + ""; }
            if (seg < 10) { segu = "0" + seg; } else { segu = seg + ""; }


            return anno + mess + dias + horr + minus + segu;
        }
        //Leer contrato
        public bool Read()
        {
            AccesoDato.BeLifeEntities Contexto = new AccesoDato.BeLifeEntities();
            try
            {
                AccesoDato.Contrato contrato = Contexto.Contrato.First(c => c.Numero == Numero);
                CommonBC.Syncronize(contrato, this);
                this.LeerClientePlan();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //Leer el cliente y plan asociados a este contrato
        private void LeerClientePlan()
        {
            BibliotecaNegocio.Cliente RegClie = new BibliotecaNegocio.Cliente();
            RegClie.RutCliente = this.RutCliente;
            if (RegClie.Read())
            {
                //BibliotecaNegocio.Cliente Sex = new BibliotecaNegocio.Sexo();
                //Sex.IdSexo = RegSex.IdSexo;
                //Sex.Descripcion = RegSex.Descripcion;
                //this.Sexo = Sex;
                this.Cliente = RegClie;
            }
            BibliotecaNegocio.Plan RegPlan = new BibliotecaNegocio.Plan();
            RegPlan.IdPlan = this.CodigoPlan;
            if (RegPlan.Read())
            {
                //BibliotecaNegocio.EstadoCivil Est = new BibliotecaNegocio.EstadoCivil();
                //Est.IdEstadoCivil = RegEst.IdEstadoCivil;
                //Est.Descripcion = RegEst.Descripcion;
                //this.EstadoCivil = Est;
                this.Plan = RegPlan;
            }
        }
        //Crear un nuevo contrato
        public bool Create()
        {
            AccesoDato.BeLifeEntities Contexto = new AccesoDato.BeLifeEntities();
            AccesoDato.Contrato contrato = new AccesoDato.Contrato();
            try
            {
                CommonBC.Syncronize(this, contrato);
                Contexto.Contrato.Add(contrato);
                Contexto.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Contexto.Contrato.Remove(contrato);

                return false;
            }
        }
        //Borrar contrato
        public bool Delete()
        {
            AccesoDato.BeLifeEntities Contexto = new AccesoDato.BeLifeEntities();
            try
            {
                AccesoDato.Contrato contrato = Contexto.Contrato.First(c => c.Numero == Numero);
                Contexto.Contrato.Remove(contrato);
                Contexto.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //Leer todos los contratos
        public List<Contrato> ReadAll()
        {
            AccesoDato.BeLifeEntities Contexto = new AccesoDato.BeLifeEntities();
            try
            {
                List<AccesoDato.Contrato> ListaBD = Contexto.Contrato.ToList<AccesoDato.Contrato>();
                //Lista de Salida
                List<Contrato> ListaBiblioteca = GenerarListaContratos(ListaBD);

                return ListaBiblioteca;
            }
            catch (Exception ex)
            {
                return new List<Contrato>();
            }
        }
        //genera lista de contratos
        private List<Contrato> GenerarListaContratos(List<AccesoDato.Contrato> listaBD)
        {
            List<Contrato> ListaContrato = new List<Contrato>();
            foreach (AccesoDato.Contrato Dato in listaBD)
            {
                Contrato contrato = new Contrato();
                CommonBC.Syncronize(Dato, contrato);
                contrato.LeerClientePlan();
                ListaContrato.Add(contrato);
            }
            return ListaContrato;
        }
        //Actualiza contratos
        public bool Update()
        {
            AccesoDato.BeLifeEntities Contexto = new AccesoDato.BeLifeEntities();
            try
            {
                AccesoDato.Contrato contrato = Contexto.Contrato.First(c => c.Numero == Numero);
                CommonBC.Syncronize(this, contrato);

                Contexto.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //ReadAllByNumeroContrato es lo musmo que el Read()
        //ReadAllByRut 
        public List<Contrato> ReadAllByRut()
        {
            List<BibliotecaNegocio.Contrato> contratos = new List<Contrato>();
            var consulta = this.ReadAll().Where(c => c.RutCliente == RutCliente);
            foreach(Contrato con in consulta)
            {
                Contrato contrato = new Contrato();
                CommonBC.Syncronize(con, contrato);
                contratos.Add(contrato);
            }
            return contratos;
        }
        //ReadAllByPoliza
        public List<Contrato> ReadAllByPoliza()
        {
            List<BibliotecaNegocio.Contrato> contratos = new List<Contrato>();
            var consulta = this.ReadAll().Where(c => c.CodigoPlan == CodigoPlan);
            foreach (Contrato con in consulta)
            {
                Contrato contrato = new Contrato();
                CommonBC.Syncronize(con, contrato);
                contratos.Add(contrato);
            }
            return contratos;
        }
        #endregion
    }
}
