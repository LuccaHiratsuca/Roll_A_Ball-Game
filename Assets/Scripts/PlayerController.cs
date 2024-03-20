using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    public float speed = 0;
    public int life = 2;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public TextMeshProUGUI timerText; 
    private float timer = 10f;
    public GameObject loseTextObject;
    private CollisionSound collisionSound;
    private MoreTimeSound moreTimeSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collisionSound = FindObjectOfType<CollisionSound>();
        moreTimeSound = FindObjectOfType<MoreTimeSound>();

        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            LoseGame();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            moreTimeSound.play();
            other.gameObject.SetActive(false);
            count += 1;
            timer += 2;
        }
        SetCountText(); 
        UpdateTimerText();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            collisionSound.play();
            life -= 1;
            SetCountText();
            if (life == 0)
            {
                LoseGame();
            }
        }
        SetCountText(); 
    }


    void SetCountText()
    {
        countText.text = "Count: " + count.ToString() + " Life: " + life.ToString();
        if (count >= 8)
        {
            WinGame();
        }
    }
    void UpdateTimerText()
    {
        timerText.text = "Time Left: " + timer.ToString("F2"); 
    }

    void WinGame()
    {
        winTextObject.SetActive(true);
        Invoke("ReturnToMainMenu", 2); 
    }

    void LoseGame()
    {
        loseTextObject.SetActive(true);
        this.enabled = false;
        Invoke("ReturnToMainMenu", 2);
    }

    void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
