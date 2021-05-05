using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    enum MovementType { PingPong, Static, UpAndDown}

    [SerializeField] MovementType m_movementType;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector3[] positions;

    private int index;
    

    // Update is called once per frame
    void Update()
    {
        switch (m_movementType)
        {
            case MovementType.PingPong:
                PingPongMovement();
                break;

            case MovementType.Static:
                break;

            case MovementType.UpAndDown:
                break;
        }
    }

    private void PingPongMovement()
    {
        transform.position = Vector2.MoveTowards(transform.position, positions[index], Time.deltaTime * speed);

        if (transform.position == positions[index])
        {
            if (index == positions.Length - 1)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                index = 0;
            }
            else
            {
                index++;
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        print("Enemy Just entered the collision defined by the object " + collider.gameObject.name);

        if (collider.tag == "Player")
        {
            Destroy(gameObject);
        }
        /*else if (collider.collider.name.Contains("Bottle"))
        {
            Destroy(this);
        }*/
    }
}
