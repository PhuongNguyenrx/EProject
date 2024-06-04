using UnityEngine;
using System.Collections.Generic;

public class SearchItem : MonoBehaviour
{
    [SerializeField] private SearchController searchController;
    public bool favorite = false;
    [SerializeField] private List<string> uniqueIDs = new List<string>(); // List to store unique IDs or tags
    [SerializeField] private List<ItemCategory> categories = new List<ItemCategory>(); // List to store assigned categories
    
    // Method to retrieve the unique IDs or tags of the item
    public List<string> GetUniqueIDs()
    {
        return uniqueIDs;
    }
    public List<ItemCategory> GetItemCategories()
    {
        return categories;
    }
    public void RetrievePossibleCategories()
    {
        if (searchController != null)
        {
            List<string> categoryNames = searchController.GetPossibleCategories();
            categories.Clear();
            foreach (string categoryName in categoryNames)
            {
                categories.Add(new ItemCategory(categoryName));
            }
        }
    }

    public void ToggleFavorite() => favorite = !favorite;

    [System.Serializable]
    public class ItemCategory
    {
        public string categoryName;

        public ItemCategory(string name)
        {
            categoryName = name;
        }
    }
}
