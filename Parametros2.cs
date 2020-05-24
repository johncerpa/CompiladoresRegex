using System;
using System.Text.RegularExpressions;

namespace Parametros2
{
    class Program
    {
        static void Main(string[] args)
        {
            int sw = 1;

            while (sw == 1)
            {
                Console.Write("Escriba el parámetro que desea validar: ");

                // Se lee la entrada y se quitan los espacios del lado izquierdo y derecho
                string ParamValid = Console.ReadLine().Trim();

                // Expresion regular para separar por comas
                // Ejemplo: "Hola a , int b" => Resulta en ["Hola a", "int b"]
                string pattern = @"\s*,\s*";

                // Hace la separación de arriba con el patrón dado
                string[] elems = System.Text.RegularExpressions.Regex.Split(ParamValid, pattern);

                bool noHayError = true;
                for (int i = 0; i < elems.Length; i++)
                {
                    String substr = elems[i].Substring(0, 5);
                    if (substr == "final")
                    {
                        elems[i] = elems[i].Substring(5).Trim();
                    }

                    if (!verificarParam(elems[i]))
                    {
                        Console.WriteLine("Error en el parametro " + (i + 1));
                        noHayError = false;
                    }
                }

                if (noHayError)
                {
                    Console.WriteLine("Los parametros estan bien escritos");
                }

                Console.Write("¿Desea continuar? (1: si/ 0: no): ");
                string opc = Console.ReadLine();

                if (opc != "1")
                {
                    sw = 0;
                }
            }

        }

        static bool verificarParam(String elem)
        {

            // Palabras reservadas para tipos
            String reserv = @"^(byte|short|int|long|float|double|bool|char|String|Array|Byte|Short|Integer|Long|Float|Double|Boolean|Character)";

            // int a, String b
            String exp1 = reserv + @"\s+[a-zA-Z_$][a-zA-Z_$0-9]*$";
            Regex r1 = new Regex(exp1);

            if (r1.IsMatch(elem))
            {
                return true;
            }

            // int[] a, int[]a, int[   ]a, int [] [] [] a
            String exp2 = reserv + @"\s*(\[\s*\]\s*)*\s*[a-zA-Z_$][a-zA-Z_$0-9]*$";
            Regex r2 = new Regex(exp2);

            if (r2.IsMatch(elem))
            {
                return true;
            }

            // int a[] [] [], int b[][][]
            String exp3 = reserv + @"\s*[a-zA-Z_$][a-zA-Z_$0-9]*\s*(\[\s*\]\s*)*$";
            Regex r3 = new Regex(exp3);

            if (r3.IsMatch(elem))
            {
                return true;
            }

            return false;
        }
    }
}