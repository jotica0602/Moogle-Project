using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.SymbolStore;
using System.Transactions;
using System.Net.Sockets;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Net.Mime;
using System.Runtime.CompilerServices;
namespace MoogleEngine
{
    public class Reader
    {
        // Redirijo la ruta del programa hacia la carpeta content
        public static string path = Path.Join("..", "Content");

        // Creo un array con todos los Docs de mi ruta.
        public static string[] files = Directory.GetFiles(path);

        // Consiste en recibir un texto, llevarlo todo a minusculas,
        // eliminar los caracteres especiales y por ultimo crear un array
        // almacenando cada palabra en una posicion.

        public static string[] Clean(string text)
        {
            char[] charstoremove = { '~', '`', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '=', '_', '+', '[', ']', '{', '}', ',', '<', '>', '.', ';', ':', ' ', 'ª', 'º', '?', '\n', '\t', '«', '»', '\r' };
            string[] bow = text.Split(charstoremove, StringSplitOptions.RemoveEmptyEntries);
            // crea el array de palabras del texto.
            return bow;
        }

        // Recibe el texto de un archivo y le aplica el metodo clean,
        // devolviendo un array de palabras
        public static string[] Tobow(string file)
        {
            string text = File.ReadAllText(file);
            string[] bow = Clean(text);
            for (int i = 0; i < bow.Length; i++)
            {
                bow[i] = bow[i].ToLower();
            }
            return bow;
        }
    }
}



