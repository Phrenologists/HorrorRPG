using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    public MovementScript[] characters;
    public float followDistance = 2f; // Distance between characters when following
    private int activeCharacterIndex = 0;

    void Start()
    {
        // Disable control for all characters except the first one
        for (int i = 1; i < characters.Length; i++)
        {
            characters[i].enabled = false;
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

        // Make non-player characters follow the active character
        FollowCharacter();
    }

    private void SwitchCharacter(int index)
    {
        // Disable control for the current character
        characters[activeCharacterIndex].enabled = false;

        // Enable control for the new character
        activeCharacterIndex = index;
        characters[activeCharacterIndex].enabled = true;

        // Reset Rigidbody velocity to prevent issues when switching characters
        characters[activeCharacterIndex].GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void FollowCharacter()
    {
        // Calculate the offset based on the controlled character
        int offset = characters.Length - activeCharacterIndex;

        // Update positions of characters
        for (int i = 1; i < characters.Length; i++)
        {
            // Calculate target index in a circular manner
            int targetIndex = (i + offset) % characters.Length;

            // Calculate target position with desired distance
            Vector3 targetPosition;

            // For character 1 (controlled), it follows character 5
            if (activeCharacterIndex == 0)
                targetPosition = characters[characters.Length - 1].transform.position - (characters[characters.Length - 1].transform.forward * followDistance);
            else
                targetPosition = characters[targetIndex].transform.position - (characters[targetIndex].transform.forward * followDistance);

            // Update position only if the character is not the currently controlled one
            if (i != activeCharacterIndex)
            {
                characters[i].transform.position = Vector3.Lerp(characters[i].transform.position, targetPosition, Time.deltaTime * 5f);
            }
        }
    }
}
