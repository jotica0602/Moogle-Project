namespace MoogleEngine;
public class Search
{
    //  Diccionario donde almacenare las palabras de la query y sus pesos.
    public static Dictionary<string, double> QueryWeight = new Dictionary<string, double>();

    //  Diccionario donde almacenare los Docs y su puntuacion.
    public static Dictionary<string, double> Score = new Dictionary<string, double>();

    // Agrega al diccionario las palabras de la query y calcula su peso (QueryWeight)
    // QW = alpha+(1-alpha)*(WF/MaxFreqWord)
    public static void FeedQueryWeight(Dictionary<string, double> QueryWeight, string[] query, Dictionary<string, double> IDF)
    {
        double alpha = 0.5;

        // Agrega las palabras sin repetir con su frecuencia 

        QueryWeight.Clear();
        foreach (var word in query)
        {
            if (!QueryWeight.ContainsKey(word))
            {
                QueryWeight.Add(word, 1);
            }
            else
            {
                QueryWeight[word]++;
            }
        }

        // Determinamos la palabra de mayor frecuencia 
        double maxfreqword = 0;

        foreach (var word in QueryWeight)
        {
            maxfreqword = Math.Max(QueryWeight[word.Key], maxfreqword);
        }

        // Calculamos el peso iterando por la frecuencia de cada palabra
        // y dividiendo su valor entre la palabra de mayor frecuencia.

        foreach (var word in QueryWeight)
        {
            if (IDF.ContainsKey(word.Key))
            {
                QueryWeight[word.Key] = QueryWeight[word.Key] = (alpha + (1 - alpha) * (word.Value / maxfreqword)) * IDF[word.Key];
            }
        }
    }
    // Agrega a un diccionario los Docs y su puntuacion (Score) con respecto 
    // a la busqueda realizada.
    // Para ello se utilizo la formula de similitud de cosenos

    public static void FeedScore(Dictionary<string, Dictionary<string, double>> Files, Dictionary<string, double> QW, Dictionary<string, double> Score)
    {
        Score.Clear();
        foreach (var file in Files)
        {
            double numerator = 0, denominator = 0, d1 = 0, d2 = 0, score = 0;

            foreach (var minidict in file.Value)
            {
                if (QW.ContainsKey(minidict.Key))
                {
                    numerator += Files[file.Key][minidict.Key] * QW[minidict.Key];
                    d1 += Math.Pow(Files[file.Key][minidict.Key], 2);
                    d2 += Math.Pow(QW[minidict.Key], 2);
                }
                else
                {
                    d1 += Math.Pow(Files[file.Key][minidict.Key], 2);
                }

                denominator = Math.Sqrt(d1) * Math.Sqrt(d2);
                if (denominator == 0) score = 0;

                else
                {
                    score = numerator / denominator;
                }
            }
            Score.Add(file.Key, score);

        }

        // Elimino todos los docs con score = 0;
        foreach (var file in Score)
        {
            if (file.Value == 0)
            {
                Score.Remove(file.Key);
            }
        }
    }
    // Fin de la recolecta de datos para el Modelo Vectorial
}

