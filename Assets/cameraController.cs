using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Transform player;
    public Transform self;
    public float speed = 0.5f;

    private Vector2 desiredDirection;

    void Start()
    {
        self.position = player.position;
    }

    void Update(){
        desiredDirection = player.position - self.position;
    }

    void FixedUpdate()
    {
        moveCamera();
    }

    void moveCamera(){
        self.position.x += desiredDirection.x * speed * Time.deltaTime;
        self.position.y += desiredDirection.y * speed * Time.deltaTime;
    }
}
