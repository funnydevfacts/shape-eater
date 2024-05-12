using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour
{
    private Vector3 camPos;
    [SerializeField] private Transform pacman;


    void Update()
    {
        Vector3 vel = Vector3.zero;
        camPos = new Vector3(pacman.transform.position.x + 14, 0, -10);
        transform.position = camPos;

    }

}