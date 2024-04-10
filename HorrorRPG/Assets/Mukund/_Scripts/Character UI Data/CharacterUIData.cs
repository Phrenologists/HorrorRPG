using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Character UI Data")]
public class CharacterUIData : ScriptableObject
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI hungerText;
    public TextMeshProUGUI staminaText;
    public Image characterHappy;
    public Image characterNeutral;
    public Image characterWeak;
    public Image characterScared;
}
