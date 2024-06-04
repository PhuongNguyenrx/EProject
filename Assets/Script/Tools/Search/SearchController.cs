using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SearchController : MonoBehaviour
{
    TMP_InputField searchInputField;
    [SerializeField] Transform itemContainer;
    SearchItem[] searchItems;
    bool toggleFavoriteFilter = false;
    [SerializeField] List<string> categories = new List<string>();
    private void Start()
    {
        searchInputField = GetComponent<TMP_InputField>();
        searchItems = new SearchItem[itemContainer.childCount];
        for (int i = 0; i < itemContainer.childCount; i++)
        {
            searchItems[i] = itemContainer.GetChild(i).GetComponent<SearchItem>();
        }
    }
    public void FilterFavorite()
    {
        toggleFavoriteFilter = !toggleFavoriteFilter;
        foreach (var searchItem in searchItems)
        {
            if (toggleFavoriteFilter)
                searchItem.gameObject.SetActive(searchItem.favorite);
            else
                searchItem.gameObject.SetActive(true);
        }
    }
    public void FilterItems()
    {
        toggleFavoriteFilter = false;
        var searchQuery = searchInputField.text;
        foreach (var searchItem in searchItems)
        {
            // Get the unique IDs or tags of the item
            List<string> itemIDs = searchItem.GetUniqueIDs();

            bool matchesSearchQuery = false;
            foreach (string uniqueID in itemIDs)
            {
                if (uniqueID.ToLower().Contains(searchQuery))
                {
                    matchesSearchQuery = true;
                    break;
                }
            }
            searchItem.gameObject.SetActive(matchesSearchQuery);
        }
    }
    public void FilterByCategory(string categoryName)
    {
        toggleFavoriteFilter = false;
        foreach (var searchItem in searchItems)
        {
            var categories = searchItem.GetItemCategories();
            bool categoryMatch = false;
            foreach (var category in categories)
            {
                if (category.categoryName.Equals(categoryName))
                {
                    categoryMatch = true;
                    break;
                }
            }
            searchItem.gameObject.SetActive(categoryMatch);
        }
    }
    public List<string> GetPossibleCategories()
    {
        return categories;
    }
}
