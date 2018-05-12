using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Prueba
{
    public class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Ingrese el tamaño del primer arreglo");
            int n = getLength(Console.ReadLine());

            Console.WriteLine("\nIngrese la primera cadena de numeros separadas por ',' desde 1 hasta 10000");
            int[] arr = Array.ConvertAll(getArray(Console.ReadLine(), n: n), arrTemp => Convert.ToInt32(arrTemp));

            Console.WriteLine("\nIngrese el tamaño del segundo arreglo");
            int m = getLength(Console.ReadLine(), n);

            Console.WriteLine("\nIngrese la segunda cadena de numeros separadas por ',' desde 1 hasta 1000 ");
            int[] brr = Array.ConvertAll(getArray(Console.ReadLine(), m: m), brrTemp => Convert.ToInt32(brrTemp));

            int[] result = missingNumbers(arr, brr);
            var strResult = string.Join(" ", result);
            Console.WriteLine($"\n\nEl resultado de la operación es: {strResult} ");
            Console.WriteLine(
                "\n\n--------------Desarrollado por Edwin Borda-----------------\n" +
                "--------------Correo: edwin.borda@outlook.com-----------------"
                );
            Console.ReadLine();
        }
        //valida el número ingresado 
        public static int getLength(string value, int? n = null)
        {
            if (!isNumber(value))
            {
                Console.WriteLine("Debe digitar un número");
                return getLength(Console.ReadLine(), n);
            }

            var result = Convert.ToInt32(value);

            if (n != null)
            {
                if (n.Value > 0 && result < n.Value && result < 2 * Math.Pow(10, 5))
                {
                    Console.WriteLine("El valor debe ser mayor a n");
                    return getLength(Console.ReadLine(), n);
                }
            }

            return result;
        }
        //realiza validaciones a los arreglos obtenidos
        private static string[] getArray(string value, int? n = null, int? m = null)
        {
            string[] characteres = value.Split(',');

            if (value.Length > 1)
            {
                if (!value.Contains(","))
                {
                    Console.WriteLine("Formato de separación incorrecto, intente nuevamente");
                    return getArray(Console.ReadLine(), n: n, m: m);
                }
            }

            if (!validateNumber(characteres))
            {
                Console.WriteLine("\nEn la cadena hay valores que no son número o no estan dentro del rango, intente de nuevo");
                return getArray(Console.ReadLine(), n: n, m: m);
            }

            validateLengthArray(characteres, n, m);



            if (validateRange(characteres))
            {
                Console.WriteLine("\nLos números ingresados no debe superar en 101 al valor mínimo , intente de nuevo");
                return getArray(Console.ReadLine(), n: n, m: m);
            }

            return characteres;
        }

        //valida el rango de los números ingresados
        private static bool validateRange(string[] array)
        {
            List<int> lista = array.ToList().Select(p =>
            {
                return Convert.ToInt32(p);
            }).ToList();

            int min = lista.Min();
            int max = lista.Max();

            return (max - min) > 101;
        }

        //valida los número de los arreglos
        private static bool validateNumber(string[] array)
        {
            var numbers = true;
            array.ToList().ForEach(p =>
            {
                if (!isNumber(p))
                    numbers = false;

                if (Convert.ToInt32(p) > Math.Pow(10, 4))
                    numbers = false;
            });

            return numbers;
        }

        //valida si el tamaño del arreglo corresponde a lo ingresado
        private static void validateLengthArray(string[] array, int? n = null, int? m = null)
        {
            if (n != null && array.Length != n.Value)
            {
                Console.WriteLine("El tamaño del arreglo no corresponde a la cantidad de números ingresados, intente nuevamente");
                getArray(Console.ReadLine(), n: n);
            }

            if (m != null && array.Length != m.Value)
            {
                Console.WriteLine("\n El tamaño del arreglo no corresponde a la cantidad de números ingresados");
                getArray(Console.ReadLine(), m: m);
            }
        }

        //valida si el valor de la cadena son números
        private static bool isNumber(string value)
        {
            Regex regex = new Regex(@"^[0-9]*$");
            Match match = regex.Match(value);
            return match.Success;
        }

        //obtiene los números perdidos
        private static int[] missingNumbers(int[] arr, int[] brr)
        {
            List<int> missingNumber = new List<int>();
            List<int> onlyValuesArr = arr.ToList().Distinct().ToList();
            List<int> onlyValuesBrr = brr.ToList().Distinct().ToList();
            List<int> sumOnlyNumbers = onlyValuesArr.Concat(onlyValuesBrr).Distinct().ToList();
            
            foreach (var item in sumOnlyNumbers)
            {
                int countA = 0;
                int countB = 0;

                for (int i = 0; i < arr.Length; i++)
                {
                    if (item == arr[i])
                        countA++;
                }

                for (int i = 0; i < brr.Length; i++)
                {
                    if (item == brr[i])
                        countB++;
                }

                if (countA != countB)
                    missingNumber.Add(item);
            }

            var orderList = missingNumber.OrderBy(i => i);
            return orderList.ToArray();

        }
    }
}


