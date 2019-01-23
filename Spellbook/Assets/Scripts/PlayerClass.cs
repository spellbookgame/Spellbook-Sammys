using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// temporary player class
public class PlayerClass : MonoBehaviour
{
    public float fMaxHealth;
    public float fCurrentHealth;
    public int numSpellPieces;

    public PlayerClass()
    {
        fMaxHealth = 20.0f;
        numSpellPieces = 0;
    }
}
