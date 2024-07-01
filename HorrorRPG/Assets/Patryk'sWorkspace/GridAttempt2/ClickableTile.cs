using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour
{
    public int tileX;
    public int tileY;
    public PatryksTilemap map;
    void OnMouseUp() {
        Debug.Log("Click");
        map.MoveSelectedUnitTo(tileX, tileY);
    }
}