using UnityEngine;
using System.Collections.Generic;

public class GroupManager : MonoBehaviour
{
    List<List<int>> groups = new List<List<int>>();

    void Update()
    {
        if (ImageDragHandler.latestGroup.Count > 0)
        {
            // Remove the images in the latest group from their current groups
            foreach (List<int> group in groups)
            {
                group.RemoveAll(i => ImageDragHandler.latestGroup.Contains(i));
            }
            // Remove any empty groups
            groups.RemoveAll(group => group.Count == 0);
            // Add the latest group
            groups.Add(new List<int>(ImageDragHandler.latestGroup));
            // Clear the latest group
            ImageDragHandler.latestGroup.Clear();

            // Print the current groups
            PrintGroups();
        }
    }

    void PrintGroups()
    {
        for (int i = 0; i < groups.Count; i++)
        {
            Debug.Log("Group " + (i + 1) + ": " + string.Join(", ", groups[i]));
        }
    }
}
