using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor.Experimental.GraphView;

public class ActiveCharacterSwitcher : MonoBehaviour
{
    [SerializeField] private FreelookRotation virtualCamera;
    public CharacterController[] characters;
    public float followDistance = 2f; // Distance between characters when following
    [HideInInspector] public int activeCharacterIndex = 0;
    private List<Vector3> activeCharacterPositions = new List<Vector3>(); // Store positions of the active character over time
    public int maxPositionsToStore = 100; // Number of positions to store
    public float rotationSpeed = 10f; // Rotation speed for characters

    void Start()
    {
        SwitchCharacter(0);
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

    public void SwitchCharacter(int index)
    {
        // Disable control for the current character
        //characters[activeCharacterIndex].SetFollow(CalculateTargetPosition(activeCharacterIndex));

        // Enable control for the new character
        activeCharacterIndex = index;


        characters[activeCharacterIndex].SetControl();

        // Clear stored positions when switching characters
        activeCharacterPositions.Clear();

        virtualCamera.UpdateFollowTarget(characters[activeCharacterIndex].cameraFollowObject);
    }

    public void SwitchCharacterByIndex(int index)
    {
        if (index >= 0 && index < characters.Length)
        {
            SwitchCharacter(index);
        }
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
                characters[i].SetFollow(CalculateTargetPosition(i));
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

        Vector3 offset = (characters[characterIndex].transform.position - characters[activeCharacterIndex].transform.position).normalized * followDistance;
        targetPosition += offset;

        //targetPosition = Vector3.Lerp(characters[characterIndex].transform.position, targetPosition, Time.fixedDeltaTime * 5f);


        return targetPosition;
    }
}
