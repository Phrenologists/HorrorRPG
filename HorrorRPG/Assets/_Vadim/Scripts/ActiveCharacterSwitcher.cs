using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Cinemachine;
public class ActiveCharacterSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook virtualCamera;
    public GameObject[] characters;
    public float followDistance = 2f; // Distance between characters when following
    [HideInInspector] public int activeCharacterIndex = 0;
    private List<Vector3> activeCharacterPositions = new List<Vector3>(); // Store positions of the active character over time
    public int maxPositionsToStore = 100; // Number of positions to store
    public float rotationSpeed = 10f; // Rotation speed for characters

    void Start()
    {
        // Disable control for all characters except the first one
        for (int i = 1; i < characters.Length; i++)
        {
            characters[i].GetComponent<CharacterController>().enabled = false;
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

    public void SwitchCharacter(int index)
    {
        // Disable control for the current character
        characters[activeCharacterIndex].GetComponent<CharacterController>().enabled = false;

        // Enable control for the new character
        activeCharacterIndex = index;
        characters[activeCharacterIndex].GetComponent<CharacterController>().enabled = true;

        // Clear stored positions when switching characters
        activeCharacterPositions.Clear();

        virtualCamera.enabled = false;
        virtualCamera.LookAt = characters[activeCharacterIndex].GetComponent<CharacterController>().cameraFollowObject;
        virtualCamera.Follow = characters[activeCharacterIndex].GetComponent<CharacterController>().cameraFollowObject;
        virtualCamera.enabled = true;
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
                // Calculate the target position for non-active characters
                Vector3 targetPosition = CalculateTargetPosition(i);
                // Move non-active characters towards the target position
                characters[i].transform.position = Vector3.Lerp(characters[i].transform.position, targetPosition, Time.deltaTime * 5f);

                // Rotate the character to face the movement direction
                if ((targetPosition - characters[i].transform.position).magnitude > 0.1f)
                {
                    float targetY = Quaternion.LookRotation(targetPosition - characters[i].transform.position).y;
                    Quaternion targetRotation = new Quaternion(characters[i].GetComponent<CharacterController>().RotationLogic.Rotation.x, targetY, characters[i].GetComponent<CharacterController>().RotationLogic.Rotation.z, characters[i].GetComponent<CharacterController>().RotationLogic.Rotation.w) ;
                    characters[i].transform.rotation = Quaternion.Lerp(characters[i].GetComponent<CharacterController>().RotationLogic.Rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
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
