using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundEffect : MonoBehaviour
{
    private float length, startpos;
    public Camera cam;
    public float parallaxEffect;

    void Start(){
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate(){
        float temp = (cam.GetComponent<Transform>().position.x * (1-parallaxEffect));
        float dist = (cam.GetComponent<Transform>().position.x * parallaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if(temp > startpos + length) startpos += length;
        else if(temp < startpos - length) startpos -= length;
    }
}