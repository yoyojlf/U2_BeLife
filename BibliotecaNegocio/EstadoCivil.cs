using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDato;

namespace BibliotecaNegocio
{
    public class EstadoCivil
    {
        #region Propiedades
        public int IdEstadoCivil { get; set; }
        public string Descripcion { get; set; }
        #endregion

        #region Metodos
        public bool Read()
        {
            
            try
            {
                AccesoDato.EstadoCivil estado = Conexion.Contexto.EstadoCivil.First(b => b.IdEstadoCivil == IdEstadoCivil);
                CommonBC.Syncronize(estado, this);

                return true;

            }catch(Exception ex)
            {
                return false;
            }
        }

        public List<EstadoCivil> ReadAll()
        {
            
            try
            {
                List<AccesoDato.EstadoCivil> ListaBD = Conexion.Contexto.EstadoCivil.ToList<AccesoDato.EstadoCivil>();
                //Lista Salida
                List<EstadoCivil> ListaBiblioteca = GenerarListaEstados(ListaBD);

                return ListaBiblioteca;
            }catch(Exception ex)
            {
                return new List<EstadoCivil>();
            }
        }

        private List<EstadoCivil> GenerarListaEstados(List<AccesoDato.EstadoCivil> listaBD)
        {
            List<EstadoCivil> ListaEstados = new List<EstadoCivil>();

            foreach(AccesoDato.EstadoCivil Datos in listaBD)
            {
                EstadoCivil estado = new EstadoCivil();
                CommonBC.Syncronize(Datos, estado);
                ListaEstados.Add(estado);
            }

            return ListaEstados;
        }
        #endregion
    }
}
