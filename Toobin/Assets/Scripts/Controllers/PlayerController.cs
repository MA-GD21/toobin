using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace ToobinLib.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        //Q = L Paddle Backward
        //E = R Paddle Backward
        //A = L Paddle Forward
        //D = R Paddle Forward

        private IEnumerator coroutine;

        [SerializeField] int m_speed = 200;
        [SerializeField] Rigidbody2D m_rigidbody2D;
        int current_lives = 1;
        const int MAX_LIVES = 2;

        private void Start()
        {
            GameManager.Instance.PointsInterface.UpdateLives(current_lives);
        }

        // Update is called once per frame
        void Update()
        {
            // L Paddle Forward
            if (Input.GetKeyDown(KeyCode.A))
            {
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
            //L Paddle Rotate
            if (Input.GetKey(KeyCode.A))
            { transform.Rotate(Vector3.back * m_speed * Time.deltaTime, Space.Self); }

            //R Paddle Forward
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (m_rigidbody2D.velocity.magnitude > 20f)
                { m_rigidbody2D.velocity = -transform.up * 20f; }
                else
                { m_rigidbody2D.velocity = -transform.up * 40f; }

                if (coroutine != null)
                {

                    StartCoroutine(coroutine);
                }
            }
            // R Paddle Rotate
            if (Input.GetKey(KeyCode.D))
            { transform.Rotate(Vector3.forward * m_speed * Time.deltaTime, Space.Self); }


            // L Paddle Backwards
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (m_rigidbody2D.velocity.magnitude > 20f)
                { m_rigidbody2D.velocity = transform.up * 20f; }
                else
                { m_rigidbody2D.velocity = transform.up * 40f; }

                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                    coroutine = ChangeSpeed(false, 0.1f);
                    StartCoroutine(coroutine);
                }
            }
            // L Paddle Backwards Rotate
            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(Vector3.back * m_speed * Time.deltaTime, Space.Self);
            }

            // R Paddle Backwards
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (m_rigidbody2D.velocity.magnitude > 20f)
                { m_rigidbody2D.velocity = transform.up * 20f; }
                else
                { m_rigidbody2D.velocity = transform.up * 40f; }

                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                    coroutine = ChangeSpeed(false, 0.1f);
                    StartCoroutine(coroutine);
                }
            }
            // R Paddle Backwards Rotate
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(Vector3.forward * m_speed * Time.deltaTime, Space.Self);
            }

            GameManager.Instance.SetCameraYaxis(transform.localPosition.y);
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

        public void GetDamage()
        {
            current_lives--;
            GameManager.Instance.PointsInterface.UpdateLives(current_lives);

            if (current_lives <= 0)
            {
                this.gameObject.SetActive(false);
                // game over screen should be here
            }
        }

        public void AddLives()
        {
            current_lives++;

            if (current_lives > MAX_LIVES)
                current_lives = MAX_LIVES;

            GameManager.Instance.PointsInterface.UpdateLives(current_lives);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            print("player interacted with " + collider.gameObject.name);
            if (collider.tag == "Enemy")
            {
                current_lives--;
                GameManager.Instance.PointsInterface.UpdateLives(current_lives);
                if (current_lives <= 0)
                {
                    // Game Over
                    GameManager.Instance.GameOver();
                }
                else
                {
                    // flashes without colliding
                }
            }

        }
    }
}


