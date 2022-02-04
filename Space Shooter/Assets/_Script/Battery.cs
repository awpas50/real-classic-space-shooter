using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public float energyRefills;
    PlayerEnergy MP;
    // Start is called before the first frame update
    void Start()
    {
        GameObject playerEnergyObject = GameObject.FindWithTag("Player");
        MP = playerEnergyObject.GetComponent<PlayerEnergy>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            if (MP.playerCurrentEnergy >= MP.playerInitialEnergy)
            {
                return;
            }
            else
            {
                Destroy(gameObject);
                MP.playerCurrentEnergy += energyRefills;
                MP.UpdateEnergyText();
            }
        }
    }
}
