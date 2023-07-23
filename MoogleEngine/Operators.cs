namespace MoogleEngine;
public class Operators
{
    // Consiste en recibir un texto, llevarlo todo a minusculas,
    // eliminar los caracteres especiales exceptuando los operadores y por ultimo crear un array
    // almacenando cada palabra en una posicion.
    public static string[] Clean(string text)
    {
        char[] charstoremoveq = { '`', '@', '#', '$', '%', '&', '(', ')', '-', '=', '_', '+', '[', ']', '{', '}', ',', '<', '>', '.', ';', ':', ' ', 'ª', 'º', '?' };
        string lowertext = text.ToLower();
        string[] bow = text.Split(charstoremoveq, StringSplitOptions.RemoveEmptyEntries);
        return bow;
    }

    // Dada una palabra, determina cuantas veces se
    // repite el caracter '*'.

    public static double Charcounter(string word)
    {
        double cont = 0;
        foreach (var character in word)
        {
            if (character == '*')
            {
                cont++;
            }
        }
        return cont;
    }

    // Metodo para calcular la distancia entre dos palabras en un texto
    public static int Distance(string text, string w1, string w2)
    {

        if (w1.Equals(w2))
        {
            return 0; // Si ambas palabras son iguales no hay distancia entre ellas 
        }

        string[] words = text.Split(" ");

        int min_dist = (words.Length) + 1;

        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Equals(w1))
            {
                for (int j = 0; j < words.Length; j++)
                {
                    if (words[j].Equals(w2))
                    {
                        int curr = Math.Abs(i - j) - 1;

                        if (curr < min_dist)
                        {
                            min_dist = curr;
                        }
                    }
                }
            }
        }
        return min_dist;
    }

    //Trabajo con Operadores
    public static void Process(string[] queryops, string[] cleanquery, Dictionary<string, Dictionary<string, double>> Files, Dictionary<string, double> Score, Dictionary<string, string> Texts)
    {
        for (int i = 0; i < queryops.Length; i++)
        {

            if (queryops[i].StartsWith('!'))
            {
                foreach (var file in Files)
                {
                    if (Files[file.Key].ContainsKey(cleanquery[i]))
                    {
                        //  Analizo cada documento de mi diccionario Files
                        //  en caso de contener la palabra, remuevo el doc
                        //  de mi diccionario de Scores.
                        Score.Remove(file.Key);
                    }
                }
            }

            if (queryops[i].StartsWith('^'))
            {
                foreach (var file in Files)
                {
                    if (!Files[file.Key].ContainsKey(cleanquery[i]))
                    {
                        //  Analizo cada documento de mi diccionario Files
                        //  en caso de NO contener la palabra, remuevo el doc
                        //  de mi diccionario de Scores.
                        Score.Remove(file.Key);
                    }
                }
            }

            if (queryops[i].StartsWith('*'))
            {
                foreach (var file in Files)
                {
                    if (Files[file.Key].ContainsKey(cleanquery[i]) && Score.ContainsKey(file.Key))
                    {
                        //  Analizo cada documento de mi diccionario Files
                        //  en caso de contener la palabra, aumento el valor
                        //  de su puntuacion en el doble de la cantidad de veces
                        //  que aparezca el caracter '*' repetido.
                        Score[file.Key] = Score[file.Key] + (Charcounter(queryops[i]) * 2);
                    }
                }
            }

            if (queryops[i].StartsWith('~'))
            {
                if (i == 0)
                {
                    //  Si la palabra con el operador '~' esta
                    //  en la primera posicion de la query
                    //  rompo el ciclo y prosigue con las demas
                    //  acciones.
                    break;
                }

                else
                {
                    //  De ocurrir lo contrario itero por cada 
                    //  texto de mi diccionario Texts y reviso si
                    //  contiene la palabra con el operador '~' y 
                    //  y la palabra anterior a ella.
                    foreach (var file in Texts)
                    {
                        string w1 = cleanquery[i], w2 = cleanquery[i - 1];
                        if (file.Value.Contains(w1) && file.Value.Contains(w2) && Distance(file.Value,w1,w2)<=10)
                        {
                            //  En caso de que la expresion evalue True
                            //  Hallo la distancia entre las palabras en el texto
                            //  con el metodo Distance y aumento en 10 la puntuacion
                            //  del doc en Score.   
                            Score[file.Key] = Score[file.Key] + 1.5;
                        }
                        else if(file.Value.Contains(w1) && file.Value.Contains(w2) && Distance(file.Value,w1,w2)>10)
                        {
                            Score[file.Key] = Score[file.Key] + 1.0;
                        }
                    }
                }
            }
        }
        // Fin del trabajo con operadores
    }
}