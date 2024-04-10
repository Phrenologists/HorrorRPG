using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [Header("Character UI Dataset")]
    public CharacterUIData character1Data;
    public CharacterUIData character2Data;
    public CharacterUIData character3Data;
    public CharacterUIData character4Data;
    public CharacterUIData character5Data;

    private CharacterUIData _currentCharacter;



    public void UpdateHealthText(Component sender, object currentHealth)
    {
        SetCurrentData(sender);
        _currentCharacter.healthText.text = currentHealth.ToString();
        
    }

    
    
    private void SetCurrentData(Component sender) 
    {
        GameObject parentObject = sender.transform.parent.gameObject;
        if(parentObject.name == "Character1")
        {
            _currentCharacter = character1Data;
        }
        else if(parentObject.name == "Character2")
        {
            _currentCharacter = character2Data;
        }
        else if(parentObject.name == "Character3")
        {
            _currentCharacter = character3Data;
        }
        else if (parentObject.name == "Character4")
        {
            _currentCharacter = character4Data;

        }
        else if (parentObject.name == "Character5")
        {
            _currentCharacter = character5Data;
        }

    }


    
}
