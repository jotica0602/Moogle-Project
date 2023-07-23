namespace MoogleEngine;
public class Snippet
{
    // Almaceno todos los documentos con sus textos.
    public static void FeedTexts(Dictionary<string, string> Texts)
    {
        Texts.Clear();
        foreach (var file in Reader.files)
        {
            Texts.Add(file, File.ReadAllText(file).ToLower());
        }
    }

    public static string Show(string[] query, string file)
    {
        string[] text = Reader.Clean(File.ReadAllText(file));
        string snippet = "";
        for (int i = 0; i < query.Length; i++)
        {
            for (int j = 0; j < text.Length; j++)
            {
                if (query[i] == text[j].ToLower())
                {
                    int index = j;
                    if (text.Length != 1 && !(index + 100 > text.Length))
                    {
                        var segment = new ArraySegment<string>(text, index, 120);

                        foreach (var word in segment)
                        {
                            snippet += " " + word;
                        }
                        break;
                    }

                    else
                    {
                        snippet = text[j];
                        break;
                    }
                }
            }
        }
        return snippet;
    }
    // Este metodo esta basado en el algoritmo de Levenshtein.
    // Consiste en recibir dos palabras y su longitud para calcular
    // la cantidad minima de operaciones a realizar para transformar una palabra en otra
    // Mientras menor sea el numero de transformaciones, mas parecidas seran las palabras.
    public static int EditDistance(string w1, string w2, int m, int n)
    {

        // Creo un metodo para comparar variables 
        // devolviendo la menor de 3 en cada caso.

        static int min(int x, int y, int z)
        {
            if (x <= y && x <= z)
            {
                return x;
            }

            if (y <= x && y <= z)
            {
                return y;
            }

            else
            {
                return z;
            }
        }

        // Creo una tabla para almacenar
        // los resultados.

        int[,] tabla = new int[m + 1, n + 1];


        // Relleno la tabla haciendo un bottom-up

        for (int i = 0; i <= m; i++)
        {
            for (int j = 0; j <= n; j++)
            {
                // Si la primera opcion es vacia entonces
                // insertas todos los caracteres en el 
                // segundo string.
                if (i == 0)
                {
                    // Operaciones minimas = j.
                    tabla[i, j] = j;
                }

                // Si el segundo string esta vacio, la opcion es 
                // eliminar todos los caracteres del segundo string .

                else if (j == 0)
                {

                    // Operaciones minimas = i.
                    tabla[i, j] = i;
                }

                // Si las ultimas letras son igulas
                // avanzamos en el string restante.

                else if (w1[i - 1] == w2[j - 1])
                {
                    tabla[i, j] = tabla[i - 1, j - 1];
                }

                // Si el ultimo caracter es diferente
                // tendremos en cuenta todas las posibilidades.
                else
                {
                    tabla[i, j] = 1
                                    + min(tabla[i, j - 1], // Agregar
                                            tabla[i - 1, j], // Eliminar
                                            tabla[i - 1, j - 1]); // Reemplazar.
                }

            }
        }
        return tabla[m, n];
    }

    //  Sugerencia
    public static string Suggestion(string word, Dictionary<string, double> IDF)
    {
        int favdistance = int.MaxValue;
        int distance;
        string favword = word;
        foreach (var w in IDF)
        {
            distance = EditDistance(w.Key, word, w.Key.Length, word.Length);
            if ((distance < favdistance))
            {
                favdistance = distance;
                favword = w.Key;
            }
        }
        return favword;
    }
}