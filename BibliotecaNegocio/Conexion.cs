﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDato;

namespace BibliotecaNegocio
{
    public class Conexion
    {
        private static BeLifeEntities contexto;

        public Conexion()
        {
                
        }

        public static BeLifeEntities Contexto
        {
            get
            {
                if(contexto != null)
                {
                    contexto = new BeLifeEntities();
                }
                return contexto;
            }
        }
    }
}
