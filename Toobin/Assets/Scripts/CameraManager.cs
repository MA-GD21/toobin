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
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].localPosition.y < leadPlayer.localPosition.y) leadPlayer = players[i];
        }
        
        setCamera(leadPlayer);
    }

    void setCamera(Transform target)
    {
        // TODO Camera is awkward when switching players; fixed camera to average x-position instead of target.localPosition.x
        GameManager.Instance.SetCameraAxis(68.0f, target.localPosition.y);
    }
}
