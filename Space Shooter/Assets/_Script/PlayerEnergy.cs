using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergy : MonoBehaviour
{
    public float playerInitialEnergy = 100.0f;
    public float playerCurrentEnergy;

    public Text EnergyText;
    public Font myFont;
    // Start is called before the first frame update
    void Start()
    {
        EnergyText.font = myFont;
        playerCurrentEnergy = playerInitialEnergy;
        EnergyText.text = "Energy: " + playerCurrentEnergy.ToString() + "/" + playerInitialEnergy.ToString();
    }

    void Update()
    {
        if (playerCurrentEnergy >= playerInitialEnergy)
        {
            playerCurrentEnergy = playerInitialEnergy;
            UpdateEnergyText();
        }
    }
    public void UseEnergy(float abilityCost)
    {
        playerCurrentEnergy -= abilityCost;
        UpdateEnergyText();
    }

    public void UpdateEnergyText()
    {
        EnergyText.text = "Energy: " + playerCurrentEnergy + "/" + playerInitialEnergy.ToString();
    }
}
