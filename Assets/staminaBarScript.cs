using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staminaBarScript : MonoBehaviour
{
    public playerMovement player;
    public Transform staminaBar;
    private float stamina;

    void Start(){

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        staminaBar.localScale = new Vector3(player.stamina / player.maxStamina * 1.5f, staminaBar.localScale.y, 1);
    }
}
