using System;
using System.Linq;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.Diagnostics;

namespace MyExtensions {
    public static class MyExtensions {
        static Random rnd = new();
        public static (T[], T[]) SplitArray<T>(this T[] array, int index) =>
        (array.Take(index).ToArray(), array.Skip(index).ToArray());

        public static bool ContainsDuplicates<T>(this IEnumerable<T> theEnum) {
        HashSet<T> theSet = theEnum.ToHashSet();
            if (theSet.Count() == theEnum.Count())
                return false;
            return true;
        }
        public static T[] GetRandomSubset<T>(this IEnumerable<T> theEnum, int newArrayCount, bool removeSubsetFromList = false) {
            List<T> oldList = theEnum.ToList();
            if (oldList.Count() <= newArrayCount || newArrayCount == -1) {
                if (removeSubsetFromList && theEnum is List<T> theList)
                    theList.RemoveAll(t => true);
                return oldList.ToArray();
            }
            T[] newArray = new T[newArrayCount];
            for (int i = 0; i < newArrayCount; i++) {
                T randItem = oldList[rnd.Next(oldList.Count())];
                newArray[i] = randItem;
                oldList.Remove(randItem);
                if (removeSubsetFromList && theEnum is List<T> theList) {
                    theList.Remove(randItem);
                } else if (removeSubsetFromList && theEnum is HashSet<T> theSet) {
                    theSet.Remove(randItem);
                } else if (removeSubsetFromList && theEnum is T[]) {
                    throw new Exception("GetRandomSubset can't remove elements from an array.");
                } else if (removeSubsetFromList) {
                    throw new Exception("GetRandomSubset can't remove elements from something that isn't a list or set.");
                }
            }
            return newArray;
        }
        public static Dictionary<TKey, TValue> ConvertTupleToDict<TKey, TValue>((TValue, TKey[])[] tupleArray) {
            Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();
            foreach((TValue, TKey[]) tuple in tupleArray) {
                foreach (TKey key in tuple.Item2) {
                    dict[key] = tuple.Item1;
                }
            }
            return dict;
        } 
        
    }
}