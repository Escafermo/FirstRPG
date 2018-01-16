using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour {

    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] float triggerRadius = 10f;

    private float currentHealthPoints = 100;
    AICharacterControl aiCharacter = null;
    GameObject player = null;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        aiCharacter = GetComponent<AICharacterControl>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position,transform.position);

        if (distanceToPlayer <= triggerRadius)
        {
            print("ball");
            aiCharacter.SetTarget(player.transform);
        }
        else
        {
            aiCharacter.SetTarget(transform);
        }


    }


    public float healthAsPercentage
    {
        get
        {
            return currentHealthPoints / (float)maxHealthPoints;
        }
    }
}
