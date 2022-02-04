using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float playerInitialHP = 100.0f;
    public float playerCurrentHP;

    public Text HPText;
    public Font myFont;
    // Start is called before the first frame update
    void Start()
    {
        playerCurrentHP = playerInitialHP;
        HPText.font = myFont;
        HPText.text = "HP: "+ playerCurrentHP.ToString() + "/" + playerInitialHP.ToString();
    }

    public void TakeDamage(float damage)
    {
        playerCurrentHP -= damage;
        UpdateHPText();
    }

    public void UpdateHPText()
    {
        HPText.text = "HP: " + playerCurrentHP + "/" + playerInitialHP.ToString();
        // Avoid display negative HP value
        if (playerCurrentHP <= 0)
        {
            HPText.text = "HP: 0" + "/" + playerInitialHP.ToString();
        }
    }
}
