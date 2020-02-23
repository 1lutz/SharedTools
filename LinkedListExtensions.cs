using System;
using System.Collections.Generic;
using System.Linq;

namespace SharedTools
{
    public static class LinkedListExtensions
    {
        public static int RemoveAll<T>(this LinkedList<T> liste, Predicate<T> match)
        {
            if (liste == null) throw new ArgumentNullException("list");
            if (match == null) throw new ArgumentNullException("match");

            int gelöschteElemente = 0;
            LinkedListNode<T> aktuellerKnoten = liste.First;

            while (aktuellerKnoten != null)
            {
                LinkedListNode<T> nächsterKnoten = aktuellerKnoten.Next;

                if (match(aktuellerKnoten.Value))
                {
                    liste.Remove(aktuellerKnoten);
                    gelöschteElemente++;
                }
                aktuellerKnoten = nächsterKnoten;
            }
            return gelöschteElemente;
        }

        static List<string> x = new List<string>();

        public static void AddRange<T>(this LinkedList<T> liste, IEnumerable<T> collection)
        {
            foreach (T element in collection) liste.AddLast(element);
        }

        public static void AddRange<T>(this LinkedList<T> liste, IEnumerable<LinkedListNode<T>> collection)
        {
            foreach (LinkedListNode<T> element in collection) liste.AddLast(element);
        }

        public static void RemoveAt<T>(this LinkedList<T> liste, int pos)
        {
            if (pos < 0 || pos >= liste.Count) throw new ArgumentOutOfRangeException("pos");

            if (pos == 0)
            {
                liste.RemoveFirst();
                return;
            }
            if (pos == liste.Count - 1)
            {
                liste.RemoveLast();
                return;
            }
            LinkedListNode<T> aktuellerKnoten = liste.First;
            if (aktuellerKnoten == null) return;
            LinkedListNode<T> nächsterKnoten;

            for (int i = 0; i < pos; ++i)
            {
                nächsterKnoten = aktuellerKnoten.Next;
                aktuellerKnoten = nächsterKnoten;
            }
            liste.Remove(aktuellerKnoten);
        }
    }
}
