using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterStatHandler : MonoBehaviour
{
    public CharacterScriptableObjectCreator[] characterStats;
    public CharacterSwitcher characterSwitcher;
    private GameObject activeCharacter;
    private int activeCharacterIndex;

    private float[] currentHealth;
    private float[] currentSanity;
    private float[] currentHunger;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = new float[characterSwitcher.characters.Length];
        currentSanity = new float[characterSwitcher.characters.Length];
        currentHunger = new float[characterSwitcher.characters.Length];

        for (int i = 0; i < characterSwitcher.characters.Length; i++)
        {
            currentHealth[i] = characterStats[i].maxHealth;
            currentSanity[i] = characterStats[i].maxSanity;
            currentHunger[i] = characterStats[i].maxHunger;
        }
        
    }

    private void Update()
    {

        activeCharacter = characterSwitcher.getActiveCharacter();
        activeCharacterIndex = characterSwitcher.activeCharacterIndex;

        if (Input.GetKeyDown(KeyCode.T))
        {
            GetCurrentStats();
            
        }
    }

    private void GetCurrentStats()
    {
       
       print("active character: "+ activeCharacter+ " | " + "health:"+ currentHealth[activeCharacterIndex] + " | " + "hunger:" + currentHunger[activeCharacterIndex] +" | " + "sanity:" + currentSanity[activeCharacterIndex]);
        
    }
}
        
    


