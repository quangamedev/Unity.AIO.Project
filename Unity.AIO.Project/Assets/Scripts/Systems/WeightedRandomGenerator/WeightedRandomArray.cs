/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   25/7/22
--------------------------------------*/

using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Generic Weighted Random Generator that returns anything.
/// The generator can also return a null if intended
/// </summary>
/// <typeparam name="T">The Item that we want to get from the system.</typeparam>
[System.Serializable]
public class WeightedRandomArray<T>
{

    [System.Serializable]
    public struct Entry
    {
        public T item;
        public int weight;

        public Entry(T item, int weight)
        {
            this.item = item;
            this.weight = weight;
        }
    }
    
    //the list that contains all the drops
    //put in order of high to low
    [SerializeField] private Entry[] _entries;
    
    public int Count => _entries.Length;

    //public void Add(T item, int weight) => _entries.Add(new Entry(item, weight));
    
    private int[] _cumulativeWeights;

    /// <summary>
    /// Gets a random object based on the weight of the object.
    /// </summary>
    /// <returns></returns>
    public T GetRandomItem()
    {
        CalculateCumulativeWeights();
        
        //the total weight of items
        var total = _entries.Sum(item => item.weight);

        //random number 
        var randomNumber = Random.Range(1, total + 1);

        for (int i = 0; i < _cumulativeWeights.Length; i++)
        {
            if (randomNumber <= _cumulativeWeights[i])
            {
                //return to stop the code from running, if it runs the item dropped are almost always the last item in the list
                return _entries[i].item;
            }
        }

        Debug.LogWarning("Could not get random object.");
        return default;
    }

    private void CalculateCumulativeWeights()
    {
        _cumulativeWeights = new int[_entries.Length];

        for (int i = 0; i < _cumulativeWeights.Length; i++)
        {
            if (i == 0)
            {
                _cumulativeWeights[0] = _entries[0].weight;
                continue;
            }

            _cumulativeWeights[i] = _cumulativeWeights[i - 1] + _entries[i].weight;
        }
    }

    // private void CalculateDropPercentage()
    // {
    //     int total = _entries.Sum(item => item.weight);
    //
    //     foreach (var e in _entries)
    //     {
    //         e.dropChance = (float) e.weight / total * 100.0f;
    //     }
    // }
}
