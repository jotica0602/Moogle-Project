using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Net.Mime;
using System.Diagnostics;
using System;
namespace MoogleEngine;

public static class Moogle
{
    public static SearchResult Query(string query)
    {
        Stopwatch crono = new Stopwatch();
        crono.Start();
        Search.FeedQueryWeight(Search.QueryWeight, Reader.Clean(query), Initialize.IDF);
        Search.FeedScore(Initialize.Files, Search.QueryWeight, Search.Score);
        Operators.Process(Operators.Clean(query), Reader.Clean(query), Initialize.Files, Search.Score, Initialize.Texts);
        var SortedResults = Search.Score.OrderByDescending(pair => pair.Value).Take(3);

        string suggestion = "";
        foreach (var word in Reader.Clean(query))
        {
            suggestion += " " + Snippet.Suggestion(word, Initialize.IDF);
        }

        SearchItem[] items = new SearchItem[SortedResults.Count()];
        for (int i = 0; i < SortedResults.Count(); i++)
        {
            items[i] = new SearchItem(SortedResults.ElementAt(i).Key.Remove(0, 11), Snippet.Show(Reader.Clean(query), SortedResults.ElementAt(i).Key), (float)Math.Round(Search.Score.ElementAt(i).Value));
        }

        if (query == string.Empty)
        {
            items = new SearchItem[1];
            items[0] = new SearchItem("No ha introducido ningún criterio a buscar", "", 0.9f);
        }
        crono.Stop();
        Console.WriteLine(crono.Elapsed);
        return new SearchResult(items, suggestion);
    }
}
