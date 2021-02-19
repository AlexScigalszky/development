using System;
using System.Text.RegularExpressions;

namespace Example
{
    public class CurrencyHelper
    {

        public static float[] NUMBER_VALUES = { 10000, 9000, 8000, 7000, 6000, 5000, 4000, 3000,
            2000, 1000, 900, 800, 700, 600, 500, 400, 300, 200, 100.1f, 100, 90, 80,
            70, 60, 50, 40, 30, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9,
            8, 7, 6, 5, 4, 3, 2, 1 };

        public static String[] NUMBERS_TEXT_EN = {
            "Ten Thousand",
            "Nine Thousand",
            "Eight Thousand",
            "Seven Thousand",
            "Six Thousand",
            "Five Thousand",
            "Four Thousand",
            "Three Thousand",
            "Two Thousand",
            "One Thousand",
            "Nine Hundred",
            "Eight Hundred",
            "Seven Hundred",
            "Six Hundred",
            "Five Hundred",
            "Four Hundred",
            "Three Hundred",
            "Two Hundred",
            "One Hundred",
            "One Hundred",
            "Ninety",
            "Eighty",
            "Seventy",
            "Sixty",
            "Fifty",
            "Forty",
            "Thirty",
            "Twenty",
            "Nineteen",
            "Eighteen",
            "Seventeen",
            "Sixteen",
            "Fifteen",
            "Fourteen",
            "Thirteen",
            "Twelve",
            "Eleven",
            "Ten",
            "Nine",
            "Eight",
            "Seven",
            "Six",
            "Five",
            "Four",
            "Three",
            "Two",
            "One"
        };

        public static String[] NUMBERS_TEXT_ES = {
            "Diez Mil",
            "Nueve Mil",
            "Ocho Mil",
            "Siete Mil",
            "Seis Mil",
            "Cinco Mil",
            "Cuatro Mil",
            "Tres Mil",
            "Dos Mil",
            "Un Mil",
            "Novecientos",
            "Ochocientos",
            "Setecientos",
            "Seiscientos",
            "Quinientos",
            "Cuatrocientos",
            "Trescientos",
            "Doscientos",
            "Ciento",
            "Cien",
            "Noventa",
            "Ochenta",
            "Setenta",
            "Sesenta",
            "Cincuenta",
            "Cuarenta",
            "Treinta",
            "Veinte",
            "Diecinueve",
            "Dieciocho",
            "Diecisiete",
            "Dieciseis",
            "Quince",
            "Catorce",
            "Trece",
            "Doce",
            "Once",
            "Diez",
            "Nueve",
            "Ocho",
            "Siete",
            "Seis",
            "Cinco",
            "Cuatro",
            "Tres",
            "Dos",
            "Un"
        };


        public static String toText(float amount)
        {
            String cout = "";
            String endl = " ";

            int dollars = (int)amount;
            int cents = (int)((amount - dollars) * 100.101);
            int saved = dollars;

            String answer = "";
            int index = 0;
            while (dollars > 0)
            {
                if (dollars >= NUMBER_VALUES[index])
                {
                    answer += NUMBERS_TEXT_ES[index];
                    answer += " ";
                    dollars -= (int)Math.Round(NUMBER_VALUES[index]);
                }
                ++index;
            }
            if (saved == 0 && cents > 1)
            {
                cout += saved + " dólares con " + ((cents > 0) ? (cents) : (0)) + " centavos" + endl;
            }
            if (saved == 0 && cents == 0)
            {
                cout += "0,00" + endl;
            }
            if (saved == 0 && cents == 1)
            {
                cout += saved + " dólares con " + cents + " centavo" + endl;
            }
            if (saved > 1 && cents > 1)
            {
                cout += answer + "dólares con " + ((cents > 0) ? (cents) : (0)) + " centavos" + endl;
            }
            if (saved > 1 && cents == 1)
            {
                cout += answer + "dólares con " + cents + " centavo" + endl;
            }
            if (saved > 1 && cents == 0)
            {
                cout += answer + "dólares con " + 0 + " centavos" + endl;
            }
            if (saved == 1 && cents > 1)
            {
                cout += answer + "dólar con " + ((cents > 0) ? (cents) : (0)) + " centavos" + endl;
            }
            if (saved == 1 && cents == 1)
            {
                cout += answer + "dólar con " + cents + " centavo" + endl;
            }
            if (saved == 1 && cents == 0)
            {
                cout += answer + "dólar con " + 0 + " centavos" + endl;
            }


            return cout.Trim();
        }

        static String AsText(float amount)
        {
            return new NumLetra().Convert(Convert.ToString(amount), false);
        }

        class NumLetra
        {
            private String[] UNIDADES = { "",
                "un",
                "dos",
                "tres",
                "cuatro",
                "cinco",
                "seis",
                "siete",
                "ocho",
                "nueve"
            };
            private String[] DECENAS = {
                "diez",
                "once",
                "doce",
                "trece",
                "catorce",
                "quince",
                "dieciseis ",
                "diecisiete",
                "dieciocho",
                "diecinueve",
                "veinte",
                "treinta",
                "cuarenta",
                "cincuenta",
                "sesenta",
                "setenta",
                "ochenta",
                "noventa"
            };
            private String[] CENTENAS = {"",
                "ciento",
                "doscientos",
                "trecientos",
                "cuatrocientos",
                "quinientos",
                "seiscientos ",
                "setecientos",
                "ochocientos",
                "novecientos"
            };

            private Regex r;

            public String Convert(String numero, bool mayusculas)
            {

                String literal = "";
                String parte_decimal;
                //si el numero utiliza (.) en lugar de (,) -> se reemplaza
                numero = numero.Replace(".", ",");

                //si el numero no tiene parte decimal, se le agrega ,00
                if (numero.IndexOf(",") == -1)
                {
                    numero = numero + ",00";
                }

                //se valida formato de entrada -> 0,00 y 999 999 999,00
                r = new Regex(@"\d{1,9},\d{1,2}");
                MatchCollection mc = r.Matches(numero);
                if (mc.Count > 0)
                {
                    //se divide el numero 0000000,00 -> entero y decimal
                    String[] Num = numero.Split(',');

                    //de da formato al numero decimal
                    parte_decimal = Num[1] + "/100";

                    //se convierte el numero a literal
                    if (int.Parse(Num[0]) == 0)
                    {
                        //si el valor es cero                
                        literal = "cero ";
                    }
                    else if (int.Parse(Num[0]) > 999999)
                    {
                        //si es millon
                        literal = getMillones(Num[0]);
                    }
                    else if (int.Parse(Num[0]) > 999)
                    {
                        //si es miles
                        literal = getMiles(Num[0]);
                    }
                    else if (int.Parse(Num[0]) > 99)
                    {
                        //si es centena
                        literal = getCentenas(Num[0]);
                    }
                    else if (int.Parse(Num[0]) > 9)
                    {
                        //si es decena
                        literal = getDecenas(Num[0]);
                    }
                    else
                    {
                        //sino unidades -> 9
                        literal = getUnidades(Num[0]);
                    }
                    //devuelve el resultado en mayusculas o minusculas
                    if (mayusculas)
                    {
                        return (literal + parte_decimal).ToUpper();
                    }
                    else
                    {
                        return (literal + parte_decimal);
                    }
                }
                else
                {
                    //error, no se puede convertir
                    return literal = null;
                }
            }

            /* funciones para convertir los numeros a literales */

            private String getUnidades(String numero)
            {   // 1 - 9            
                //si tuviera algun 0 antes se lo quita -> 09 = 9 o 009=9
                String num = numero.Substring(numero.Length - 1);
                return UNIDADES[int.Parse(num)];
            }

            private String getDecenas(String num)
            {
                // 99                        
                int n = int.Parse(num);
                if (n < 10)
                {
                    //para casos como -> 01 - 09
                    return getUnidades(num);
                }
                else if (n > 19)
                {
                    //para 20...99
                    String u = getUnidades(num);
                    if (u.Equals(""))
                    { 
                        //para 20,30,40,50,60,70,80,90
                        return DECENAS[int.Parse(num.Substring(0, 1)) + 8];
                    }
                    else
                    {
                        return DECENAS[int.Parse(num.Substring(0, 1)) + 8] + "y " + u;
                    }
                }
                else
                {
                    //numeros entre 11 y 19
                    return DECENAS[n - 10];
                }
            }

            private String getCentenas(String num)
            {
                // 999 o 099
                if (int.Parse(num) > 99)
                {
                    //es centena
                    if (int.Parse(num) == 100)
                    {
                        //caso especial
                        return " cien ";
                    }
                    else
                    {
                        return CENTENAS[int.Parse(num.Substring(0, 1))] + getDecenas(num.Substring(1));
                    }
                }
                else
                {
                    //por Ej. 099 
                    //se quita el 0 antes de convertir a decenas
                    return getDecenas(int.Parse(num) + "");
                }
            }

            private String getMiles(String numero)
            {
                // 999 999
                //obtiene las centenas
                String c = numero.Substring(numero.Length - 3);
                //obtiene los miles
                String m = numero.Substring(0, numero.Length - 3);
                String n = "";
                //se comprueba que miles tenga valor entero
                if (int.Parse(m) > 0)
                {
                    n = getCentenas(m);
                    return n + "mil " + getCentenas(c);
                }
                else
                {
                    return "" + getCentenas(c);
                }

            }

            private String getMillones(String numero)
            { //000 000 000        
              //se obtiene los miles
                String miles = numero.Substring(numero.Length - 6);
                //se obtiene los millones
                String millon = numero.Substring(0, numero.Length - 6);
                String n = "";
                if (millon.Length > 1)
                {
                    n = getCentenas(millon) + "millones ";
                }
                else
                {
                    n = getUnidades(millon) + "millon ";
                }
                return n + getMiles(miles);
            }

        }
    }
}
