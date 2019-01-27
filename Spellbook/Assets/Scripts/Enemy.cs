using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float fMaxHealth;
    public float fCurrentHealth;

    public Enemy(float maxHealth)
    {
        fMaxHealth = maxHealth;
    }
}
