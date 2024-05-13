using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchItem : MonoBehaviour
{
    public bool favorite = false;
    [SerializeField]
    private List<string> uniqueIDs = new List<string>(); // List to store unique IDs or tags

    // Method to retrieve the unique IDs or tags of the item
    public List<string> GetUniqueIDs()
    {
        return uniqueIDs;
    }

    public void ToggleFavorite() => favorite = !favorite;
}
