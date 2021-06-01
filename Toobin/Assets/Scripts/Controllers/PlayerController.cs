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
        public static PlayerController Instance;

        private IEnumerator coroutine = null;
        [SerializeField] int m_alpha = 0;
        [SerializeField] int m_speed = 100;
        int m_oldSpeed;
        public const int MAX_SPEED = 150;
        [SerializeField] Rigidbody2D m_rigidbody2D;
        int current_lives = 2;
        const int MAX_LIVES = 2;
        bool m_isGoingForward = true;
        bool m_isKeyPressed = false;

        public int Speed { get => m_speed; set => m_speed = value; }
        public int Speed1 { get => m_speed; set => m_speed = value; }

        private void Start()
        {
            Instance = this;
            GameManager.Instance.PointsInterface.UpdateLives(current_lives);
        }

        // Update is called once per frame
        void Update()
        {
            // L Paddle Forward
            if (Input.GetKeyDown(KeyCode.A))
            {   //i think this code adds too much +5 to m_alpha (the turn angle) during one button press. Maybe use a courotine to only add +5 once?
                m_alpha += 5;
                m_isKeyPressed = true;
                transform.Rotate(new Vector3(0, 0, m_alpha));
                //Is there a better solution to accelerate the player? 
                m_rigidbody2D.velocity = -transform.up * m_speed;
                Handlecoroutine(true);
            }

            //R Paddle Forward
            if (Input.GetKeyDown(KeyCode.D))
            {
                m_isKeyPressed = true;
                m_alpha += -5;
                transform.Rotate(new Vector3(0, 0, m_alpha));
                m_rigidbody2D.velocity = -transform.up * m_speed;
                Handlecoroutine(true);
            }

            //need code for button press R paddle forward & l paddle forward to just accelerate the player without turning him in either direction

            // L Paddle Backwards
            if (Input.GetKeyDown(KeyCode.Q))
            {
                m_isKeyPressed = true;
                m_alpha += 5;
                transform.Rotate(new Vector3(0, 0, m_alpha));
                m_rigidbody2D.velocity = transform.up * m_speed;
                Handlecoroutine(false);
            }

            // R Paddle Backwards
            if (Input.GetKeyDown(KeyCode.E))
            {
                m_isKeyPressed = true;
                m_alpha += -5;
                transform.Rotate(new Vector3(0, 0, m_alpha));
                m_rigidbody2D.velocity = transform.up * m_speed;
                Handlecoroutine(false);
            }

            if (!m_isKeyPressed || m_rigidbody2D.velocity == Vector2.zero || m_speed == 0)
                m_rigidbody2D.velocity = m_rigidbody2D.velocity - new Vector2(0, 0.1f);

            m_isKeyPressed = false;

            m_alpha = 0;
            GameManager.Instance.SetCameraAxis(transform.localPosition.x, transform.localPosition.y);
        }

        public void EnterStream()
        {
            m_oldSpeed = m_speed;
            m_speed = MAX_SPEED;
            if (m_isGoingForward)
                m_rigidbody2D.velocity = -transform.up * m_speed;
            else
                m_rigidbody2D.velocity = transform.up * m_speed;
        }

        public void ExitStream()
        {
            m_speed = m_oldSpeed;
            if (m_isGoingForward)
                m_rigidbody2D.velocity = -transform.up * m_speed;
            else
                m_rigidbody2D.velocity = transform.up * m_speed;
        }

        // every 2 seconds perform the print()
        private IEnumerator ChangeSpeed(bool isIncrease, float waitTime)
        {
            yield return new WaitForSeconds(waitTime);

            if (isIncrease)
            {
                if (!m_isGoingForward)
                {
                    m_isGoingForward = true;
                    m_speed = 0;
                    m_rigidbody2D.velocity = Vector2.zero;
                }
                else
                {
                    m_speed = Mathf.Min(m_speed + 5, MAX_SPEED);
                }
            }
            else
            {
                if (m_isGoingForward)
                {
                    m_isGoingForward = false;
                    m_speed = 0;
                    m_rigidbody2D.velocity = Vector2.zero;
                }
                else
                {
                    m_speed = Mathf.Min(m_speed + 5, MAX_SPEED);
                }
            }
        }

        private void Handlecoroutine(bool increase)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);

            coroutine = ChangeSpeed(increase, 0.1f);
            StartCoroutine(coroutine);
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
                m_speed = 0;
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
            /*if (collider.tag == "Sand")
            {
                m_speed = 0;
            }*/
        }
    }
}


