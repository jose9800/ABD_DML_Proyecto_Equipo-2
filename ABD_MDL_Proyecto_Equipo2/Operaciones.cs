using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace ABD_MDL_Proyecto_Equipo2
{
    class Operaciones
    {
        string entrada;
        //String de conexion, indica que se esta usando la base de datos con nombre ejemplo
        string conn_S = "Data Source=localhost;Initial Catalog =ejemplo ;Integrated Security=True";
        Errores E = new Errores();


        //Metodo que capura entrada y clasifica si es una entrada(insert) [con columnas especificas o en todas las columnas], una eliminacion(delete), modificacion(update) o lista(select)
        public void capturar_Entrada()
        {
            entrada = Console.ReadLine();

            bool y = false, o = false, regular = false;
            //Verifica si el texto de entrada comienza con "insertar en" y "valores" , para verificar la sintaxis

            if (entrada.StartsWith("insertar en") & entrada.Contains("valores"))
            {
                // se identifica si se utilizan columnas especificas o no, separando y contando sentencias en la entrada capturada
                // para ello se utiliza el metodo split 
                // referencias de manipulacion de texto:
                //  https://docs.microsoft.com/en-us/dotnet/api/system.string.split?view=netframework-4.8
                //  https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/strings/
                //
                //
                //

                try
                {
                    int contador = 0;

                    // arreglo que define donde se cortara el texto
                    string[] separador_cC = { "insertar en", "valores", " " };
                    //arreglo donde se insertaran los textos cortados
                    string[] subtextos_cC = entrada.Split(separador_cC, StringSplitOptions.RemoveEmptyEntries);

                    Console.WriteLine(" ");
                    foreach (var item in subtextos_cC) // se cuenta cuantos textos tien el arreglo
                    {
                        // se imprimira en consola el split para visualizar mejor
                        item.Trim();

                        Console.WriteLine(item.ToString());
                        contador = contador + 1;


                    }
                    if (contador == 3) // se utilizaron columnas especificas en el comando de entrada
                    {
                        // metodo para entrada con columnas
                        insertar_con_Columnas(subtextos_cC[0], subtextos_cC[1], subtextos_cC[2]);


                    }

                    else
                    {
                        // se vuelve a repetir el mismo proceso


                        contador = 0;
                        // arreglo que define donde se cortara el texto , este caso es diferente al separador anterior, no se incluye " " (espacio en blanco)
                        string[] separador_sC = { "insertar en", "valores" };
                        //arreglo donde se insertaran los textos cortados
                        string[] subtextos_sC = entrada.Split(separador_sC, StringSplitOptions.RemoveEmptyEntries);

                        Console.WriteLine(" ");
                        foreach (var item in subtextos_sC) // se cuenta cuantos textos tien el arreglo
                        {
                            // se imprimira en consola el split para visualizar mejor
                            // puede imprimerse 2 veces ya que se vuelve a repetir el proceso

                            item.Trim();

                            Console.WriteLine(item.ToString());
                            contador = contador + 1;


                        }

                        if (contador == 2) // no se utilizaron columnas especificas(se insertara en todas)
                        {
                            // metodo para entrada sin columnas
                            insertar_sin_Columnas(subtextos_sC[0], subtextos_sC[1]);

                        }



                    }



                }
                catch (Exception e)
                {




                }






            }


            // LOGICA PARA BORRAR

            // separador y

            if (entrada.StartsWith("borrar") & entrada.Contains("donde") & entrada.Contains(" y ") & !entrada.Contains(" o "))
            {


                string[] separador_y = new string[] { "borrar en", "donde", " y " };
                y = true;
                borrar(separador_y);


            }
            // separador o
            else if (entrada.StartsWith("borrar") & entrada.Contains("donde") & entrada.Contains(" o ") & !entrada.Contains(" y "))
            {
                //Console.WriteLine(!entrada.Contains(" y "));

                string[] separador_o = new string[] { "borrar en", "donde", " o " };
                o = true;
                borrar(separador_o);

                //Console.WriteLine(o.ToString() + "o" + y.ToString() + "y" + regular.ToString() + "regular" );
            }
            else if (entrada.StartsWith("borrar") & entrada.Contains("donde") & !entrada.Contains(" o ") & !entrada.Contains(" y "))
            {
                string[] separador_r = new string[] { "borrar en", "donde" };
                regular = true;
                borrar(separador_r);


            }

            else
            {
                Console.WriteLine("Comando no detectado ");


            }

            //
            // METODO PARA BORRAR
            // 


            void borrar(string[] s)
            {
                string[] separador = s;

                String[] subtextos_y = entrada.Split(separador, StringSplitOptions.RemoveEmptyEntries);

                int contador_borrar = 0;
                Console.WriteLine(" ");

                foreach (var item in subtextos_y)
                {
                    item.Trim();
                    contador_borrar = contador_borrar + 1;
                    Console.WriteLine(item.ToString());

                }

                string condiciones = "";

                Console.WriteLine(" ");
                if (y == true)
                {
                    for (int i = 1; i < contador_borrar; i++)
                    {

                        condiciones = condiciones + subtextos_y[i] + " AND ";


                    }
                    condiciones = condiciones.Substring(0, (condiciones.Length - 4));
                }
                if (o == true)
                {
                    for (int i = 1; i < contador_borrar; i++)
                    {

                        condiciones = condiciones + subtextos_y[i] + " OR ";

                    }
                    condiciones = condiciones.Substring(0, (condiciones.Length - 3));
                }
                if (regular == true)
                {

                    for (int i = 1; i < contador_borrar; i++)
                    {

                        condiciones = condiciones + subtextos_y[i];

                    }

                }

                Console.WriteLine(condiciones);



                using (SqlConnection con = new SqlConnection(conn_S))
                {

                    try
                    {
                        // consulta sql  
                        string consulta = ("delete from " + subtextos_y[0] + " where " + condiciones);


                        SqlCommand cm = new SqlCommand(consulta, con);

                        // abre conexion 
                        con.Open();
                        // ejecuta consulta
                        cm.ExecuteNonQuery();
                        // mensaje
                        Console.WriteLine("accion realizada");

                    }
                    catch (SqlException e) // manejo de exepciones comunes
                    {

                        E.cat(e);



                    }
                    finally
                    {
                        con.Close();

                    }

                }
            }










            // update - modificar

            if (entrada.StartsWith("modificar en") & entrada.Contains("donde") & entrada.Contains(" y ") & !entrada.Contains(" o "))
            {


                string[] separador_y = new string[] { "modificar en", "donde", " y ", " " };
                y = true;
                modificar(separador_y);



            }
            // separador o
            else if (entrada.StartsWith("modificar en") & entrada.Contains("donde") & entrada.Contains(" o ") & !entrada.Contains(" y "))
            {
                //Console.WriteLine(!entrada.Contains(" y "));

                string[] separador_o = new string[] { "modificar en", "donde", " o ", " " };
                o = true;
                modificar(separador_o);


                //Console.WriteLine(o.ToString() + "o" + y.ToString() + "y" + regular.ToString() + "regular" );
            }
            else if (entrada.StartsWith("modificar en") & entrada.Contains("donde") & !entrada.Contains(" o ") & !entrada.Contains(" y "))
            {
                // 1 parametro 
                string[] separador_r = new string[] { "modificar en", "donde", " " };
                regular = true;
                modificar(separador_r);



            }
            else
            {
                Console.WriteLine("Comando no detectado ");


            }




            void modificar(string[] s)
            {
                string[] separador = s;

                String[] subtextos_y = entrada.Split(separador, StringSplitOptions.RemoveEmptyEntries);

                int contador_borrar = 0;
                Console.WriteLine(" ");

                foreach (var item in subtextos_y)
                {
                    item.Trim();
                    contador_borrar = contador_borrar + 1;
                    Console.WriteLine(item.ToString());

                }

                string condiciones = "";

                Console.WriteLine(" ");
                if (y == true)
                {
                    for (int i = 2; i < contador_borrar; i++)
                    {

                        condiciones = condiciones + subtextos_y[i] + " AND ";


                    }
                    condiciones = condiciones.Substring(0, (condiciones.Length - 4));
                }
                if (o == true)
                {
                    for (int i = 2; i < contador_borrar; i++)
                    {

                        condiciones = condiciones + subtextos_y[i] + " OR ";

                    }
                    condiciones = condiciones.Substring(0, (condiciones.Length - 3));
                }
                if (regular == true)
                {

                    for (int i = 2; i < contador_borrar; i++)
                    {

                        condiciones = condiciones + subtextos_y[i];

                    }

                }


                Console.WriteLine(condiciones);
                Console.WriteLine("");
                Console.WriteLine("update " + subtextos_y[0] + " set " + subtextos_y[1] + " where " + condiciones);
                //Console.ReadKey();


                using (SqlConnection con = new SqlConnection(conn_S))
                {

                    try
                    {
                        // consulta sql  
                        string consulta = ("update " + subtextos_y[0] + " set " + subtextos_y[1] + " where " + condiciones);


                        SqlCommand cm = new SqlCommand(consulta, con);

                        // abre conexion 
                        con.Open();
                        // ejecuta consulta
                        //cm.ExecuteNonQuery();
                        // mensaje
                        Console.WriteLine("accion realizada");
                        Console.WriteLine(cm.ExecuteNonQuery()); //
                    }
                    catch (SqlException e) // manejo de exepciones comunes
                    {

                        E.cat(e);

                    }
                    finally
                    {
                        con.Close();

                    }

                }
            }







            // SELECT - LISTA
            if (entrada.StartsWith("lista en ") & entrada.Contains(" donde ") & entrada.Contains(" y ") & !entrada.Contains(" o ") & entrada.EndsWith(";"))
            {


                string[] separador_y = new string[] { "lista en", "donde", " y ", " " };
                y = true;
                listar(separador_y);



            }
            // separador o
            else if (entrada.StartsWith("lista en ") & entrada.Contains(" donde ") & entrada.Contains(" o ") & !entrada.Contains(" y ") & entrada.EndsWith(";"))
            {
                //Console.WriteLine(!entrada.Contains(" y "));

                string[] separador_o = new string[] { "lista en", "donde", " o ", " " };
                o = true;
                listar(separador_o);


                //Console.WriteLine(o.ToString() + "o" + y.ToString() + "y" + regular.ToString() + "regular" );
            }
            else if (entrada.StartsWith("lista en ") & !entrada.Contains(" o ") & !entrada.Contains(" y ") & entrada.Contains(" *") & entrada.EndsWith(";"))
            {
                // 1 parametro 
                string[] separador_r = new string[] { "lista en ", " *" };
                regular = true;
                listar(separador_r);



            }
            else
            {
                Console.WriteLine("Comando no detectado ");


            }


            void listar(string[] s)
            {
                string[] separador = s;

                String[] subtextos_y = entrada.Split(separador, StringSplitOptions.RemoveEmptyEntries);

                int contador_borrar = 0;
                Console.WriteLine(" ");

                foreach (var item in subtextos_y)
                {
                    item.Trim();
                    contador_borrar = contador_borrar + 1;
                    Console.WriteLine(item.ToString());

                }

                string condiciones = "";

                Console.WriteLine(" ");
                if (y == true)
                {
                    for (int i = 2; i < contador_borrar; i++)
                    {

                        condiciones = condiciones + subtextos_y[i] + " AND ";


                    }
                    condiciones = condiciones.Substring(0, (condiciones.Length - 4));
                    comando_concatenado();
                }
                if (o == true)
                {
                    for (int i = 2; i < contador_borrar; i++)
                    {

                        condiciones = condiciones + subtextos_y[i] + " OR ";

                    }
                    condiciones = condiciones.Substring(0, (condiciones.Length - 3));
                    comando_concatenado();
                }
                if (regular == true)
                {

                    for (int i = 2; i < contador_borrar; i++)
                    {

                        condiciones = condiciones + subtextos_y[i];

                    }
                    comando_regular();
                }


                //Console.WriteLine(condiciones);
                //Console.WriteLine("");
                //Console.WriteLine("select " + subtextos_y[1] + " from " + subtextos_y[0] + " where " + condiciones);

                //Console.WriteLine("select * from {0};",subtextos_y[0]);


                //Console.ReadKey();


                //select y - o
                void comando_concatenado()
                {
                    using (SqlConnection con = new SqlConnection(conn_S))
                    {

                        try
                        {
                            // consulta sql  
                            string consulta = ("select " + subtextos_y[1] + " from " + subtextos_y[0] + " where " + condiciones);
                            Console.WriteLine(consulta);

                            SqlCommand cm = new SqlCommand(consulta, con);

                            // abre conexion 
                            con.Open();
                            // ejecuta consulta
                            //cm.ExecuteScalar();
                            // mensaje
                            //Console.WriteLine(cm.ExecuteScalar());

                            SqlDataReader reader = cm.ExecuteReader();
                            while (reader.Read())
                            {
                                Console.WriteLine(String.Format("{0}", reader[0]));
                            }

                            Console.WriteLine("accion realizada");

                        }
                        catch (SqlException e) // manejo de exepciones comunes
                        {

                            E.cat(e);

                        }
                        finally
                        {
                            con.Close();

                        }

                    }



                }

                //  select todo *
                void comando_regular()
                {
                    using (SqlConnection con = new SqlConnection(conn_S))
                    {

                        try
                        {
                            // consulta sql  
                            string consulta = "select * from " + subtextos_y[0] + ";";
                            Console.WriteLine(consulta);

                            SqlCommand cm = new SqlCommand(consulta, con);

                            // abre conexion 
                            con.Open();
                            // ejecuta consulta
                            //cm.ExecuteScalar();
                            // mensaje

                            SqlDataReader reader = cm.ExecuteReader();
                            while (reader.Read())
                            {
                                Console.WriteLine(String.Format("{0}", reader[0]));
                            }

                            Console.WriteLine("Accion Realizada");

                        }
                        catch (SqlException e) // manejo de exepciones comunes
                        {

                            E.cat(e);

                        }
                        finally
                        {
                            con.Close();

                        }

                    }



                }



            }






        }
















































































































        void insertar_con_Columnas(string tabla, string columnas, string valores)
        {
            string t = tabla, c = columnas, v = valores;


            // Usando "Using" para cerrar conexion automaticamente (Investigar using y IDisposable)
            using (SqlConnection con = new SqlConnection(conn_S))
            {

                try
                {
                    // consulta sql  
                    string consulta = ("insert into " + t + "(" + columnas + ")" + "values" + valores);

                    SqlCommand cm = new SqlCommand(consulta, con);

                    // Abre conexion 
                    con.Open();
                    // Ejecuta consulta
                    cm.ExecuteNonQuery();
                    // Mensaje
                    Console.WriteLine("ACCION REALIZADA");

                }
                catch (SqlException e) // manejo de exepciones comunes
                {

                    E.cat(e);

                }
                finally
                {
                    con.Close();

                }

            }



        }



        //este metodo es igual a insertar con columnas a diferencia de que solo utiliza entrada de tabla y valores, no utiliza comlumnas ya que esta implicito que utlizara todas
        void insertar_sin_Columnas(string tabla, string valores)
        {

            string t = tabla, v = valores;

            // Usando "Using" para cerrar conexion automaticamente
            using (SqlConnection con = new SqlConnection(conn_S))
            {

                try
                {
                    // consulta sql  
                    string consulta = ("insert into " + t + "values" + valores);

                    SqlCommand cm = new SqlCommand(consulta, con);

                    // Abre conexion 
                    con.Open();
                    // Ejecuta consulta
                    cm.ExecuteNonQuery();
                    // Mensaje
                    Console.WriteLine("ACCION REALIZADA");

                }
                catch (SqlException e)
                {
                    E.cat(e);

                }
                finally
                {
                    con.Close();
                }
            }


        }


    }
}
