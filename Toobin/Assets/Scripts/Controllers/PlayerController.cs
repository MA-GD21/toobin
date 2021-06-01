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
        [SerializeField] int m_alpha = 0;
        [SerializeField] int m_speed = 200;
        [SerializeField] Rigidbody2D m_rigidbody2D;
        int current_lives = 2;
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
            {   //i think this code adds too much +5 to m_alpha (the turn angle) during one button press. Maybe use a courotine to only add +5 once?
                m_alpha += 5;
                transform.Rotate(new Vector3(0, 0, m_alpha));
                //Is there a better solution to accelerate the player? 
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

            //R Paddle Forward
            if (Input.GetKeyDown(KeyCode.D))
            {
                m_alpha += -5;
                transform.Rotate(new Vector3(0, 0, m_alpha));
                if (m_rigidbody2D.velocity.magnitude > 20f)
                { m_rigidbody2D.velocity = -transform.up * 20f; }
                else
                { m_rigidbody2D.velocity = -transform.up * 40f; }

                if (coroutine != null)
                {

                    StartCoroutine(coroutine);
                }
            }

            //need code for button press R paddle forward & l paddle forward to just accelerate the player without turning him in either direction

            // L Paddle Backwards
            if (Input.GetKeyDown(KeyCode.Q))
            {
                m_alpha += 5;
                transform.Rotate(new Vector3(0, 0, m_alpha));
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


            // R Paddle Backwards
            if (Input.GetKeyDown(KeyCode.E))
            {
                m_alpha += -5;
                transform.Rotate(new Vector3(0, 0, m_alpha));
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


