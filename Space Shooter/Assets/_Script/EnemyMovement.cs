using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject Player;
    public GameObject enemy;
    public float speed = 2f;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Trace();
    }

    private void Trace()
    {
        if(Player)
        {
            transform.LookAt(Player.transform);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }
}
