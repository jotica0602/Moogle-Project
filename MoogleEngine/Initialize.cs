using System.Diagnostics;
namespace MoogleEngine
{
    public class Initialize
    {
        //  Diccionario donde almacenare los documentos,sus palabras y frecuencias.
        public static Dictionary<string, Dictionary<string, double>> Files = new Dictionary<string, Dictionary<string, double>>();

        //  Diccionario donde almacenare la IDF de cada palabra.
        public static Dictionary<string, double> IDF = new Dictionary<string, double>();

        //  Diccionario donde almacenare cada documento con su texto correspondiente.
        public static Dictionary<string, string> Texts = new Dictionary<string, string>();

        public static void Feed()
        {
            Stopwatch crono = new Stopwatch();
            crono.Start();
            TF_IDF.Feedfiles(Files);
            TF_IDF.TF(Files);
            TF_IDF.FeedIDF(Files, IDF);
            TF_IDF.Weight(Files, IDF);
            Snippet.FeedTexts(Texts);
            crono.Stop();
            Console.WriteLine(crono.Elapsed);
        }
    }
}