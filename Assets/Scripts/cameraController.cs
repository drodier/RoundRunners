using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    private const string CLAMP_X = "x";
    private const string CLAMP_Y = "y";

    public Transform player;
    public Transform self;
    public float speed = 4f;
    public float aheadStrength = 1f;
    public float maxX = 3f;
    public float maxY = 1.2f;

    private float direction = 0;
    private Vector2 desiredDirection;

    void Start()
    {
        self.position = player.position;
    }

    void Update(){
        direction = Input.GetAxis("Horizontal");

        desiredDirection = (targetPosition() - self.position)*2;
        if(direction < 0){
            desiredDirection = new Vector3(desiredDirection.x - aheadStrength, desiredDirection.y);
        }else if(direction > 0){
            desiredDirection = new Vector3(desiredDirection.x + aheadStrength, desiredDirection.y);
        }
    }

    void FixedUpdate()
    {
        moveCamera();
    }

    Vector3 targetPosition(){
        return new Vector3(player.position.x + (aheadStrength * direction),
                            player.position.y + aheadStrength,
                            -1);
    }

    void moveCamera(){
        self.position += (new Vector3(desiredDirection.x, desiredDirection.y, 1) * speed * Time.deltaTime);
        self.position = new Vector3(clamp(self.position.x, CLAMP_X), clamp(self.position.y, CLAMP_Y), -1);
    }

    float clamp(float x, string code){
        switch(code){
            case CLAMP_X:
                float deltaX = x - player.position.x;
                if(Mathf.Abs(deltaX) > maxX){
                      return deltaX >= 0 ? player.position.x + maxX : player.position.x - maxX;
                }else{
                    return x;
                }
            case CLAMP_Y:
                float deltaY = x - player.position.y;
                if(Mathf.Abs(deltaY) > maxY){
                      return deltaY >= 0 ? player.position.y + maxY : player.position.y - maxY;
                }else{
                    return x;
                }
            default:
                return x;
        }
    }
}