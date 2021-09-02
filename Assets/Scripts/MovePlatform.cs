using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public Transform position_1;
    public Transform position_2;
    public float moveSpeed;

    bool pos_1;

    public GameManager gameManager;
    Vector3 startPosition;

    public float wait;
    [HideInInspector]
    public float waitTimer;
    public bool waitBool;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        startPosition = transform.position;
        waitTimer = wait;
    }

    private void Update()
    {
        if (gameManager.playerHasLeftStartZone)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ResetPosition();
            }
        }
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
        waitTimer = wait;
        waitBool = false;
        pos_1 = false;
    }

    void FixedUpdate()
    {
        if (waitBool)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
            {
                waitTimer = wait;
                waitBool = false;
            }
        }
        if (!waitBool)
        {
            if (pos_1)
            {
                if (Vector2.Distance(transform.position, position_1.position) < 0.05f)
                {
                    pos_1 = false;
                    waitBool = true;
                    transform.position = position_1.position;
                }
                transform.position = Vector2.MoveTowards(transform.position, position_1.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                if (Vector2.Distance(transform.position, position_2.position) < 0.05f)
                {
                    pos_1 = true;
                    waitBool = true;
                    transform.position = position_2.position;

                }
                transform.position = Vector2.MoveTowards(transform.position, position_2.position, moveSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Ghost")
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Ghost")
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Ghost")
        {
            collision.transform.SetParent(null);
        }
    }
}
