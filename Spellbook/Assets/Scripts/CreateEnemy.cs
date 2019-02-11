using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// creates an enemy at the very start of the game
public class CreateEnemy : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        // if an enemy does not exist, create one
        if(GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            // instantiating enemy with 20 health
            enemy = Instantiate(enemy);
            enemy.Initialize(20f);
            enemy.fCurrentHealth = enemy.fMaxHealth;
        }
    }
}
