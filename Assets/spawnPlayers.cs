using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class spawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    public cameraController cam;

    public float minX;
    public float maxX;

    private void Start(){
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), -4);
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        cam.setPlayer(player.GetComponent<Transform>());
    }


}
