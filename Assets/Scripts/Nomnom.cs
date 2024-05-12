using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nomnom : MonoBehaviour
{
    public Pacman.FoodType Type;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!gameObject.CompareTag("Cross"))
        {
            Destroy(gameObject);
        }

    }
}
