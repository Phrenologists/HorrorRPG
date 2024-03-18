using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class CharacterSwitcher : MonoBehaviour
{
    public GameObject[] characters;
    public float followDistance = 2f; // Distance between characters when following
    private int activeCharacterIndex = 0;
    private List<Vector3> activeCharacterPositions = new List<Vector3>(); // Store positions of the active character over time
    public int maxPositionsToStore = 100; // Number of positions to store

    void Start()
    {
        // Disable control for all characters except the first one
        for (int i = 1; i < characters.Length; i++)
        {
            characters[i].GetComponent<MovementScript>().enabled = false;
        }
    }

    void Update()
    {
        // Switch character based on input keys (1-5)
        for (int i = 0; i < characters.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SwitchCharacter(i);
                break;
            }
        }

        // Make non-player characters follow the path of the active character
        FollowActiveCharacter();
    }

    private void SwitchCharacter(int index)
    {
        // Disable control for the current character
        characters[activeCharacterIndex].GetComponent<MovementScript>().enabled = false;

        // Enable control for the new character
        activeCharacterIndex = index;
        characters[activeCharacterIndex].GetComponent<MovementScript>().enabled = true;

        // Clear stored positions when switching characters
        activeCharacterPositions.Clear();
    }

    private void FollowActiveCharacter()
    {
        // Store position of the active character
        activeCharacterPositions.Insert(0, characters[activeCharacterIndex].transform.position);
        // Limit the number of stored positions
        if (activeCharacterPositions.Count > maxPositionsToStore)
        {
            activeCharacterPositions.RemoveAt(activeCharacterPositions.Count - 1);
        }

        for (int i = 0; i < characters.Length; i++)
        {
            if (i != activeCharacterIndex)
            {
                // Calculate the target position for non-active characters
                Vector3 targetPosition = CalculateTargetPosition(i);
                // Move non-active characters towards the target position
                characters[i].transform.position = Vector3.Lerp(characters[i].transform.position, targetPosition, Time.deltaTime * 5f);
            }
        }
    }

    private Vector3 CalculateTargetPosition(int characterIndex)
    {
        // Determine the target index for the character in the chain
        int targetIndex = characterIndex - 1;
        if (targetIndex < 0)
            targetIndex += characters.Length; // Wrap around to the last character

        // Retrieve the corresponding position from the stored positions of the active character
        int storedIndex = Mathf.Min(targetIndex, activeCharacterPositions.Count - 1);
        Vector3 targetPosition = activeCharacterPositions[storedIndex];

        // Adjust the target position based on the follow distance
        Vector3 offset = (characters[characterIndex].transform.position - characters[activeCharacterIndex].transform.position).normalized * followDistance;
        targetPosition += offset;

        return targetPosition;
    }
}