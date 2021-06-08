using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] players;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is ahead of the other player
        Transform leadPlayer = players[0];
        float averageXPos = 0.0f;
        
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].localPosition.y < leadPlayer.localPosition.y) leadPlayer = players[i];
            averageXPos += players[i].localPosition.x;
        }

        // Always average camera xPosition between players
        averageXPos = averageXPos / players.Length;
        setCamera(leadPlayer, averageXPos);
    }

    void setCamera(Transform target, float xPos)
    {
        GameManager.Instance.SetCameraAxis(xPos, target.localPosition.y);
    }
}
