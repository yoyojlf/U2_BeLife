using Microsoft.VisualStudio.TestTools.UnitTesting;
using BibliotecaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaNegocio.Tests
{
    [TestClass()]
    public class TarificadorTests
    {
        Tarificador tarificador;
        [TestMethod()]
        public void TarificadorTest_Ingreso_decimales()//verificar que se pueden ingresar valores decimales
        {
            try
            {
                //  arrange
                double Base = 1.1d;
                tarificador = new Tarificador();

                //  act
                tarificador.CalcularPrimaBase(Base);

            }catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TarificadorTest_Valor_base_fuera_rango()//verifica que se impida ingresar un valor base fuera de rango
        {
            //  arrange
            tarificador = new Tarificador();
            double Base = -1.1;

            // act
            tarificador.CalcularPrimaBase(Base);

            // assert is handled by ExpectedException
        }

        [TestMethod()]
        public void TarificadorTest_FechaFuera_rango_control_exce1()
        {
            try
            {
                tarificador = new Tarificador();
                tarificador.EstadoCivil.IdEstadoCivil = 1;
                tarificador.Sexo.IdSexo = 1;
                tarificador.FechaNacimiento = new DateTime(2008, 1, 1);

            }catch(ArgumentException ex)
            {
                
            }
            catch(Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void TarificadorTest_FechaFuera_rango_control_exce2()
        {
            //  arrange
            tarificador = new Tarificador(); //inician con los valores por defecto

            //  act
            tarificador.EstadoCivil.IdEstadoCivil = 1;
            tarificador.Sexo.IdSexo = 1;
            tarificador.FechaNacimiento = new DateTime(2008, 1, 1); //modifico la edad para provocar la excepcion verificar que provoque excepcion con edad fuera de rango
        }

        [TestMethod()]
        public void CalcularPrimaTest_1()
        {
            //  arrange
            double Bass = 5.5d; //  valor base a ingresar
            double Realidad;
            double Espectativa = 6.2d; //   el valor que deveria retornar es 6.2
            tarificador = new Tarificador(new DateTime(1996, 6, 7),1,2); //inicializamos el tarificador con esas 22 años sexo hombre y estado civil casado

            //  act
            Realidad = tarificador.CalcularPrimaBase(Bass);

            //  assert
            Assert.AreEqual(Espectativa,Realidad);
        }

        [TestMethod()]
        public void CalcularPrimaTest_2()
        {

            //  arrange
            double Bass = 5.5d; //  valor base a ingresar
            double Realidad;
            double Espectativa = 6.1d; //   el valor que deveria retornar es 6.2
            tarificador = new Tarificador(new DateTime(1996, 6, 7), 2, 2); //inicializamos el tarificador con esas 22 años sexo Mujer y estado civil divorciado

            //  act
            Realidad = tarificador.CalcularPrimaBase(Bass);

            //  assert
            Assert.AreEqual(Espectativa, Realidad);
        }
    }
}