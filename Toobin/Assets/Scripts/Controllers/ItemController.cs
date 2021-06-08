using System.Collections;
using System.Collections.Generic;
using ToobinLib.Controllers;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    enum ItemType { Can, GreenChest, RedChest, GoldChest, Ball, Stream, Coffee, Gate }

    [SerializeField] ItemType m_itemType;
    // green - 200 pt
    // red - 500 pt
    // gold - 1000 pt
    const int GREEN_POINTS = 200;
    const int RED_POINTS = 500;
    const int GOLD_POINTS = 1000;
    const int SCORE_POINTS = 150;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Player")
            return;
        PlayerController player = collider.GetComponent<PlayerController>();
        switch (m_itemType)
        {
            case ItemType.Can:
                // flip controls
                player.ReverseControls();
                break;

            case ItemType.Stream:
                PlayerController.Instance.EnterStream();
                break;

            case ItemType.GreenChest:
                AddPoints(GREEN_POINTS, player); 
                gameObject.SetActive(false);
                Destroy(this);
                //play sound
                FindObjectOfType<AudioManager>().Play("CollectItem");
                break;

            case ItemType.RedChest:
                AddPoints(RED_POINTS, player);
                gameObject.SetActive(false);
                Destroy(this);
                break;

            case ItemType.GoldChest:
                AddPoints(GOLD_POINTS, player); 
                gameObject.SetActive(false);
                Destroy(this);
                break;
            
            case ItemType.Gate:
                AddPoints(SCORE_POINTS, player);
                break;
            
            case ItemType.Coffee:
                // Reset
                player.ResetControls();
                break;
        }
        print("item interacted with " + collider.gameObject.name);
    }

    private void AddPoints(int value, PlayerController player)
    {
        if (player.playerID == 0)
        {
            GameManager.Instance.Points += value;
            GameManager.Instance.PointsInterface.UpdatePoints(GameManager.Instance.Points, player.playerID);
        }
        else
        {
            GameManager.Instance.Points_p2 += value;
            GameManager.Instance.PointsInterface.UpdatePoints(GameManager.Instance.Points_p2, player.playerID);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag != "Player")
            return;

        switch (m_itemType)
        {
            case ItemType.Stream:
                PlayerController.Instance.ExitStream();
                Debug.Log("exit");
                break;
        }
    }
}