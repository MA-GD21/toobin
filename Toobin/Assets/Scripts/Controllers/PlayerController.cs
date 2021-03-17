using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToobinLib.Controllers
{

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] int m_speed = 20;
        [SerializeField] Rigidbody2D m_rigidbody2D;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey("right"))
            {
                transform.Rotate(Vector3.back * m_speed * Time.deltaTime, Space.Self);
            }
            if (Input.GetKey("left"))
            {
                transform.Rotate(Vector3.forward * m_speed * Time.deltaTime, Space.Self);
            }

            if (Input.GetKey("down"))
            {
                //Move the Rigidbody forwards constantly at speed you define (the blue arrow axis in Scene view)
                //m_rigidbody2D.velocity = transform.forward * m_speed*1000;
                // Move the object forward along its z axis 1 unit/second.
                transform.Translate(Vector3.forward * Time.deltaTime );

                Debug.Log("down was pressed");
            }
        }
    }
}
