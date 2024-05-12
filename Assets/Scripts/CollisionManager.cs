using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private Pacman pacmanRef;

    void Start()
    {
        pacmanRef = GetComponent<Pacman>();
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        Pacman.FoodType Type = col.gameObject.GetComponent<Nomnom>().Type;
        pacmanRef.CollectFood(Type);
    }
}
