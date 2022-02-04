using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;

    GameController gameController;
    PlayerHealth HP;
    PlayerController stats;

    public float enemyInitialHP;
    private float enemyCurrentHP;
    public float impactDamage;

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        GameObject playerHealthObject = GameObject.FindWithTag("Player");
        GameObject playerControllerObject = GameObject.FindWithTag("Player");

        gameController = gameControllerObject.GetComponent<GameController>();
        HP = playerHealthObject.GetComponent<PlayerHealth>();
        stats = playerControllerObject.GetComponent<PlayerController>();

        enemyCurrentHP = enemyInitialHP; //reset HP
    }

    // execute when enters the trigger
    void OnTriggerEnter(Collider other)
    {
        // Enemy(Object) Being damage
        if(other.tag == "Bolt")
        {
            Destroy(other.gameObject);
            enemyCurrentHP -= stats.ATK;
            Debug.Log("Damage per shot: " + stats.ATK + "     " + "Enemy Health: " + enemyCurrentHP + "/" + enemyInitialHP);
        }

        // Enemy (or bullets) collide with player
        if (other.tag == "Player")
        {
            HP.TakeDamage(impactDamage);
            enemyCurrentHP = 0; // immediately destroy any object when collide
        }

        // Player died
        if (HP.playerCurrentHP <= 0)
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
            gameController.GameOver();
        }
        
        // Destroy enemy and get score
        if (enemyCurrentHP <= 0)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            gameController.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}