using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;


namespace ABD_MDL_Proyecto_Equipo2
{
    class Program
    {


        static void Main(string[] args)
        {
            // 
            // *Se necesita tener una base de datos con el nombre ejemplo , el manejo del comando use database no nos pertenece a nosotros, por lo que la podemos crear de manera manual en sql server 
            //
            // los comandos son sensibles a mayusculas y minusculas, todos los comandos se deberan escribir en miniscula o no seran detectados
            //
            //
            //  Ejemplo Sintaxis: insertar en alumno nombre,apellido,edad valores('nombre1','apellido1','22');  en caso de especificar columnas
            //
            //  Ejemplo Sintaxis: insertar en alumno valores('nombre1,''apellido1,'22');    en caso de insertar en todas las columnas
            //
            //  se usan comillas simples para valores ''
            //  lleva ; al final de cada comando


            //---------------------------------------------------------------------------------------
            //                  Errores y Cosas por hacer
            //  El flujo del programa no esta bien hecho, a veces muestra comando no detectado aunque se haya ejecutado la accion.
            //  Probablemente todavia no se han manejado todas las exepciones possibles            
            //  Faltan commandos de Modificar y Lista
            //
            //
            //
            //
            //  
            //string input; ignorar


            Operaciones op = new Operaciones();

            //string entrada;



            //Ciclo infinito para el metodo de capturar entrada
            while (true)
            {

                op.capturar_Entrada();

            }

        }


    }
}
