// =============================================================================================
// =========================== weighted graphs =================================================
// =============================================================================================

// this on the other hand, is used for weighted graphs...

using System;
using System.Collections.Generic;

namespace GraphImpl.Weighted
{
    public class CreateGraph
    {
        public void doMagic()
        {
            var g = new Graph();
            // after having gone through all possibilities, create a graph vertex


            g.add_vertex('A', new Dictionary<char, int> {{'B', 7}, {'C', 8}});
            g.add_vertex('B', new Dictionary<char, int> {{'A', 7}, {'F', 2}});
            g.add_vertex('C', new Dictionary<char, int> {{'A', 8}, {'F', 6}, {'G', 4}});
            g.add_vertex('D', new Dictionary<char, int> {{'F', 8}});
            g.add_vertex('E', new Dictionary<char, int> {{'H', 1}});
            g.add_vertex('F', new Dictionary<char, int> {{'B', 2}, {'C', 6}, {'D', 8}, {'G', 9}, {'H', 3}});
            g.add_vertex('G', new Dictionary<char, int> {{'C', 4}, {'F', 9}});
            g.add_vertex('H', new Dictionary<char, int> {{'E', 1}, {'F', 3}});

            g.shortest_path('A', 'H').ForEach(x => Console.WriteLine(x));
        }
    }
}