namespace MoogleEngine;
public class TF_IDF
{
    // Agrega al diccionario los datos de la siguiente forma:
    // <Doc,<palabras,frecuencia>>.

    public static void Feedfiles(Dictionary<string, Dictionary<string, double>> Files)
    {
        Files.Clear();
        foreach (var file in Reader.files)
        {
            Files.Add(file, new Dictionary<string, double>());
            foreach (var word in Reader.Tobow(file))
            {
                if (!Files[file].ContainsKey(word))
                {
                    //  Si el doc no tiene esa palabra la agregara 
                    //  con 1 en su valor de frecuencia
                    Files[file].Add(word, 1);
                }
                else
                {
                    //  En caso de contenerla aumentara
                    //  en 1 su valor de frecuencia
                    Files[file][word]++;
                }
            }
        }
    }

    // Comienza la Recolecta de datos para el Modelo Vectorial

    // Recibe dos diccionarios y agrega al segundo "todas las palabras de todos los textos
    // al segundo sin repetir", teniendo en cuenta si la palabra habia aparecido en algun 
    // documento con anterioridad, de ser asi aumentaba el contador de repeticiones de esa palabra.
    // Finalmente calcula su IDF.
    // IDF = Log10(N/ni)
    // N --> Total de DOcs
    // ni --> Total de Docs en los que aparece la palabra.
    public static void FeedIDF(Dictionary<string, Dictionary<string, double>> Files, Dictionary<string, double> IDF)
    {
        IDF.Clear();
        // Primera parte del metodo: Agregar las palabras 
        // al diccionario teniendo en cuenta si han aparecido
        // con anterioridad en los cuerpos de otros documentos o no.

        foreach (var file in Files)
        {
            foreach (var minidict in file.Value)
            {
                if (!IDF.ContainsKey(minidict.Key))
                {
                    IDF.Add(minidict.Key, 1); //Si no esta agregala con 1 una repeticion.
                }
                else
                {
                    IDF[minidict.Key]++; // Si esta, sumale 1 a su valor.
                }
            }
        }

        // Calculo el IDF de cada palabra.
        foreach (var word in IDF)
        {
            IDF[word.Key] = Math.Log10(Reader.files.Length / IDF[word.Key]);
        }
    }

    // Recibe un Diccionario con la estructura <documento,palabras,frecuencia> y calcula la Frecuencia Normalizada (TF)
    // de cada una de sus palabras en el texto correspondiente a cada una
    // TF = WF/MaxFreqW
    // WF --> Frecuencia de la palabra
    // MaxFreqW --> palabra de mayor frecuencia en el texto correspondiente .
    public static void TF(Dictionary<string, Dictionary<string, double>> Files)
    {
        foreach (var file in Files)
        {
            double maxfreqword = 0;
            foreach (var minidict in file.Value)
            {
                maxfreqword = Math.Max(minidict.Value, maxfreqword);
                // Itero buscando la palabra de mayor frecuencia por texto.
            }

            foreach (var minidict in file.Value)
            {
                Files[file.Key][minidict.Key] = Files[file.Key][minidict.Key] / maxfreqword;
                // Itero buscando la frecuencia de cada palabra y dividiendola por la de 
                // mayor frecuencia en su texto correspondiente.
            }
        }
    }

    // Recibe dos diccionarios como parametros y devuelve 
    // el peso (W) de cada palabra.
    // W=TF*IDF.
    public static void Weight(Dictionary<string, Dictionary<string, double>> Files, Dictionary<string, double> IDF)
    {
        foreach (var file in Files)
        {
            foreach (var minidict in file.Value)
            {
                if (IDF.ContainsKey(minidict.Key))
                {
                    Files[file.Key][minidict.Key] = Files[file.Key][minidict.Key] * IDF[minidict.Key];
                }
            }
        }
    }
}