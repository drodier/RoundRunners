using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staminaBarScript : MonoBehaviour
{
    public playerMovement player;
    public Transform staminaBar;
    public Transform staminaContainer;
    private float stamina;

    void Start(){

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float offset = player.stamina / player.maxStamina;
        staminaBar.localScale = new Vector3(1.1f * offset, .15f, 1);
        staminaBar.position = new Vector3(staminaContainer.position.x-((1-offset)/2), staminaContainer.position.y, staminaContainer.position.z);
    }
}
