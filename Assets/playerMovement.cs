using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public characterController controller;

    public float runSpeed = 40f;

    public  float horizontalMove = 0f;

    public float horizontalAxis = 0f;

    public  float lastMove = 0f;

    public bool jump = false;
    
    public bool crouch = false;

    public bool dashing = false;

    public double stamina = 10f;
    
    public  double dashTimer = 10f;
    void Start()
    {
        
    }

    void Update()    
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        horizontalAxis = horizontalMove / runSpeed;

        jump = Input.GetButtonDown("Jump") ? true : jump;
        if(Input.GetButtonDown("Crouch") && stamina == 10f){
            crouch = true;
        } else if(Input.GetButtonUp("Crouch") || (stamina < 10f && dashTimer == 10f)){
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
                runSpeed = 80f;
            }
            runSpeed -= 1;
            dashTimer -= 0.1f;
            stamina -= 0.1f;
        }
    }

    void resetDash(){
        runSpeed = 40f;
        dashTimer = 10f;
        dashing = false;
    }

    void recover(){
        if(!crouch && stamina < 10){
            stamina += 0.1f;
            stamina = stamina > 10 ? 10 : stamina;
        }
    }
}
