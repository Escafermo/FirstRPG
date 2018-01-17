using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;


public class Player : MonoBehaviour {

    [SerializeField] float maxHealthPoints = 100f;
    
    private float currentHealthPoints = 100;
    
    public float  healthAsPercentage
    {
        get
        {
            return currentHealthPoints / (float)maxHealthPoints;
        }
    }
}
