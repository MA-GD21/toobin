using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace ToobinLib.Controllers
{


    public class PlayerController2 : MonoBehaviour
    {


        //Q = L Paddle Backward
        //E = R Paddle Backward
        //A = L Paddle Forward
        //D = R Paddle Forward

        private IEnumerator coroutine;

        [SerializeField] int m_speed = 100;
        [SerializeField] Rigidbody2D m_rigidbody2D;

        // Start is called before the first frame update
        void Start()
        {

            var rotation = 0;
            var direction = 0;
        }


        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                transform.Rotate(Vector3.back * m_speed * Time.deltaTime, Space.Self);

                if (m_rigidbody2D.velocity.magnitude > 20f)
                { m_rigidbody2D.velocity = -transform.up * 20f; }
                else
                { m_rigidbody2D.velocity = -transform.up * 40f; }

                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                    coroutine = ChangeSpeed(true, 0.1f);
                    StartCoroutine(coroutine);
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                transform.Rotate(Vector3.forward * m_speed * Time.deltaTime, Space.Self);
                m_rigidbody2D.velocity = -transform.up * 20f;

                if (coroutine != null)
                {

                    StartCoroutine(coroutine);
                }
            }

            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                transform.Rotate(Vector3.back * m_speed * Time.deltaTime, Space.Self);
                m_rigidbody2D.velocity = -transform.up * 20f;

                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                    coroutine = ChangeSpeed(false, 0.1f);
                    StartCoroutine(coroutine);
                }
            }

            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(Vector3.back * m_speed * Time.deltaTime, Space.Self);
                m_rigidbody2D.velocity = transform.up * 20f;

                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                    coroutine = ChangeSpeed(false, 0.1f);
                    StartCoroutine(coroutine);
                }
            }

            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(Vector3.forward * m_speed * Time.deltaTime, Space.Self);
                m_rigidbody2D.velocity = transform.up * 20f;

                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                    coroutine = ChangeSpeed(false, 0.1f);
                    StartCoroutine(coroutine);
                }
            }

        }

        // every 2 seconds perform the print()
        private IEnumerator ChangeSpeed(bool isIncrease, float waitTime)
        {
            while (m_speed > 0)
            {
                yield return new WaitForSeconds(waitTime);
                if (isIncrease)
                    m_speed = m_speed + 5;
                else
                    m_speed = m_speed - 5;
            }
        }
    }
}


