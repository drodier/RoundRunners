using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public characterController controller;
    public Transform playerTransform;
    public float baseRunSpeed = 40f;
    public float staminaRegen = 0.1f;
    public float maxStamina = 5f;
    public float stamina = 5f;

    private float runSpeed = 40f;
    private  float horizontalMove = 0f;
    private float orientation = 1f;
    private bool jump = false; 
    private bool crouch = false;

    public float crouchDashCost = 0.1f;
    public float crouchDashStrength = 80f;
    public float crouchDashDeceleration = 1f;
    private bool crouchDashing = false;
    private  float crouchLastMove = 0f;


    public float dashCost = 3f;
    public float dashStrength = 2f;
    private bool dashable = true;



    void Start()
    {
        stamina = maxStamina;
    }

    void Update()    
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        orientation = horizontalMove / runSpeed == -1 ? -1 : 1;
        jump = Input.GetButtonDown("Jump") ? true : jump;

        crouch = checkCrouch();
        dashable = checkDash();

        doCrouch();
        doDash();
    }

    void FixedUpdate(){
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
        crouchDash();
        recover();
    }

    bool checkCrouch(){
        if(Input.GetButtonDown("Crouch") && stamina - crouchDashCost >= 0){
            return true;
        } else if(Input.GetButtonUp("Crouch") || stamina <= 0 || crouchLastMove == orientation){
            return false;
        }
        return crouch;
    }

    void doCrouch(){
        if(crouch)
        {
            initiateCrouchDash();
        }
        else
        {
            resetCrouchDash();
        }
    }

    void initiateCrouchDash(){
        if(!crouchDashing){
            crouchDashing = true;
            runSpeed = crouchDashStrength;
            crouchDash();
        }
        else{
            crouchDash();
        }
    }

    void crouchDash(){
        crouchLastMove = Input.GetAxis("Horizontal");
        runSpeed -= crouchDashDeceleration;
        stamina -= crouchDashCost;
    }

    void resetCrouchDash(){
        runSpeed = baseRunSpeed;
        crouchDashing = false;
    }

    bool checkDash(){
        if(Input.GetButtonDown("Dash") && stamina - dashCost >= 0){
            return true;
        } else if(Input.GetButtonUp("Dash") || (stamina < maxStamina)){
            return false;
        }
        return dashable;
    }

    void doDash(){
        if(dashable)
        {
            dash();
        }
    }

    void dash(){
        if(dashable){
            stamina -= dashCost;
            playerTransform.position = new Vector3(playerTransform.position.x + (dashStrength * orientation),
                                                    playerTransform.position.y,
                                                    playerTransform.position.z);
        }
    }

    void resetDash(){
        dashable = false;
    }

    void recover(){
        if(!crouch && !dashable && stamina < maxStamina){
            stamina += staminaRegen;
            stamina = stamina > maxStamina ? maxStamina : stamina;
        }
    }
}
