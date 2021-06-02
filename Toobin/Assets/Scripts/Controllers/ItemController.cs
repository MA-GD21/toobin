using System.Collections;
using System.Collections.Generic;
using ToobinLib.Controllers;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    enum ItemType { Can, GreenChest, RedChest, GoldChest, Ball, Stream }

    [SerializeField] ItemType m_itemType;
    // green - 200 pt
    // red - 500 pt
    // gold - 1000 pt
    const int GREEN_POINTS = 200;
    const int RED_POINTS = 500;
    const int GOLD_POINTS = 1000;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Player")
            return;

        switch (m_itemType)
        {
            case ItemType.Can:
                break;

            case ItemType.Stream:
                PlayerController.Instance.EnterStream();
                break;

            case ItemType.GreenChest:
                GameManager.Instance.Points += GREEN_POINTS;
                GameManager.Instance.PointsInterface.UpdatePoints(GameManager.Instance.Points);
                gameObject.SetActive(false);
                Destroy(this);
                //play sound
                FindObjectOfType<AudioManager>().Play("CollectItem");
                break;

            case ItemType.RedChest:
                GameManager.Instance.Points += RED_POINTS;
                GameManager.Instance.PointsInterface.UpdatePoints(GameManager.Instance.Points);
                gameObject.SetActive(false);
                Destroy(this);
                break;

            case ItemType.GoldChest:
                GameManager.Instance.Points += GOLD_POINTS;
                GameManager.Instance.PointsInterface.UpdatePoints(GameManager.Instance.Points);
                gameObject.SetActive(false);
                Destroy(this);
                break;
        }
        print("item interacted with " + collider.gameObject.name);
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