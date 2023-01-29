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
    public float orientation = 1f;
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
        orientation = Input.GetAxisRaw("Horizontal");
        horizontalMove = orientation * runSpeed;
        jump = Input.GetButtonDown("Jump") ? true : jump;

        crouch = checkCrouch();
        dashable = checkDash();
    }

    void FixedUpdate(){
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
        doCrouch();
        doDash();
        recover();
    }

    bool checkCrouch(){
        if(Input.GetButtonDown("Crouch") && stamina - crouchDashCost >= 0 && (crouchLastMove == orientation || crouchLastMove == 0)){
            return true;
        } else if(Input.GetButtonUp("Crouch") || stamina <= 0){
            resetCrouchDash();
            return false;
        }
        crouchDashing = crouch;
        return crouch;
    }

    void doCrouch(){
        if(crouchDashing)
        {
            if(runSpeed == baseRunSpeed
            ){
                runSpeed = crouchDashStrength;
                crouchLastMove = Input.GetAxis("Horizontal");
            }
            crouchDash();
        }
    }

    void crouchDash(){
        runSpeed -= crouchDashDeceleration;
        stamina -= crouchDashCost;
    }

    void resetCrouchDash(){
        runSpeed = baseRunSpeed;
        crouchDashing = false;
        crouchLastMove = 0;
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
