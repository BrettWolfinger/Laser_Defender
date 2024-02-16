using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deflect : MonoBehaviour
{
    //Destroys damageDealer objects when they hit deflector collider so that
    //they dont pass through and damage the ship.
    void OnTriggerEnter2D(Collider2D other) 
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if(damageDealer != null)
        {
            damageDealer.Hit();
        }    
    }
}
