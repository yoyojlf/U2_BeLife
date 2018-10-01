using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDato;

namespace BibliotecaNegocio
{
    public class Sexo
    {
        #region Propiedades
        public int IdSexo { get; set; }
        public string Descripcion { get; set; }
        #endregion

        #region Metodos
        public bool Read()
        {
            AccesoDato.BeLifeEntities Contexto = new AccesoDato.BeLifeEntities();
            try
            {
                AccesoDato.Sexo sexo = Contexto.Sexo.First(b => b.IdSexo == IdSexo);
                CommonBC.Syncronize(sexo, this);

                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

        public List<Sexo> ReadAll()
        {
            AccesoDato.BeLifeEntities Contexto = new AccesoDato.BeLifeEntities();
            try
            {
                List<AccesoDato.Sexo> ListaSexBD = Contexto.Sexo.ToList<AccesoDato.Sexo>();
                //Lista salida
                List<Sexo> ListaSexBB = GenerarListaSexos(ListaSexBD);

                return ListaSexBB;
            }catch(Exception ex)
            {
                return new List<Sexo>();
            }
        }

        private List<Sexo> GenerarListaSexos(List<AccesoDato.Sexo> listaSexBD)
        {
            List<Sexo> ListaBB = new List<Sexo>();

            foreach(AccesoDato.Sexo Datos in listaSexBD)
            {
                Sexo Sex = new Sexo();
                CommonBC.Syncronize(Datos, Sex);
                ListaBB.Add(Sex);
            }

            return ListaBB;
        }
        #endregion
    }
}
