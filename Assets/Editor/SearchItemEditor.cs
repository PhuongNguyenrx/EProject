using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SearchItem))]
public class SearchItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SearchItem searchItem = (SearchItem)target;

        // Retrieve categories from the SearchController
        if (GUILayout.Button("Retrieve Categories"))
        {
            searchItem.RetrievePossibleCategories();
        }
    }
}
