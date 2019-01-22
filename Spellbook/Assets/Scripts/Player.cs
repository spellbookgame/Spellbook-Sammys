using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// temporary player class
public class Player : MonoBehaviour
{
    public float fMaxHealth;
    public float fCurrentHealth;
    public int numSpellPieces;

    public Player()
    {
        fMaxHealth = 20.0f;
        numSpellPieces = 0;
    }
}
