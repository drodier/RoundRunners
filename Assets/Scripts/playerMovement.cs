using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public characterController controller;
    public float baseRunSpeed = 40f;
    public float dashStrength = 80f;
    public float staminaRegen = 0.1f;
    public float maxStamina = 5f;
    public float stamina = 5f;
    public float dashCost = 0.1f;
    public float dashDeceleration = 1f;

    private float runSpeed = 40f;
    private  float horizontalMove = 0f;
    private float horizontalAxis = 0f;
    private  float lastMove = 0f;
    private bool jump = false; 
    private bool crouch = false;
    private bool dashing = false;
    private  float dashTimer = 10f;

    void Start()
    {
        stamina = maxStamina;
    }

    void Update()    
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        horizontalAxis = horizontalMove / runSpeed;

        jump = Input.GetButtonDown("Jump") ? true : jump;
        if(Input.GetButtonDown("Crouch") && stamina == maxStamina){
            crouch = true;
        } else if(Input.GetButtonUp("Crouch") || (stamina < maxStamina && dashTimer == 10f)){
            crouch = false;
        }

        if(crouch)
        {
            if(horizontalAxis != 0 && dashTimer == 10f)
            {
                initiateDash();
            }
            else
            {
                if((horizontalAxis == 0 || horizontalAxis == lastMove) && dashTimer < 10f)
                {
                    initiateDash();
                }
                else
                {
                    resetDash();
                }
            }
        }
        else if(dashTimer < 10f)
        {
            resetDash();
        }
    }

    void FixedUpdate(){
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
        crouchDash();
        recover();
    }

    void initiateDash(){
        if(!dashing){
            dashing = true;
            crouchDash();
        }
    }

    void crouchDash(){
        if(dashing){
            lastMove = horizontalAxis;
            if(dashTimer == 10f){
                runSpeed = dashStrength;
            }
            runSpeed -= dashDeceleration;
            stamina -= dashCost;
            dashTimer -= 0.1f;
        }
    }

    void resetDash(){
        runSpeed = baseRunSpeed;
        dashTimer = 10f;
        dashing = false;
    }

    void recover(){
        if(!crouch && stamina < maxStamina){
            stamina += staminaRegen;
            stamina = stamina > maxStamina ? maxStamina : stamina;
        }
    }
}
