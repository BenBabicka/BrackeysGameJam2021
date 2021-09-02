using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float movementSpeed;
    public float jumpHeight;
    Vector2 direction;
    public Transform groundCheck;
    public float groundCheckDistance;
    public LayerMask jumpableLayers;

    public Vector3 startingPosition;
    public GameObject ghost;

    public GameManager gameManager;

    public AudioSource audioSource;
    public AudioClip jump;
    public AudioClip hurt;
    public AudioClip lose;
    public AudioClip win;

    public float pitchChangeAmount;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.GetComponent<PlayerCamera>().StartPlayer)
        {
            direction.x = Input.GetAxisRaw("Horizontal");

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (IsGrounded())
                {
                    audioSource.pitch = Random.Range(1 - pitchChangeAmount, 1 + pitchChangeAmount);
                    audioSource.PlayOneShot(jump);
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + jumpHeight);
                }
            }
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (gameManager.playerHasLeftStartZone)
                {
                    SetCharacter();
                }
            }
        }
    }

    public void Win()
    {
        audioSource.pitch = Random.Range(1 - pitchChangeAmount, 1 + pitchChangeAmount);
        audioSource.PlayOneShot(win);
    }

    public void Lose()
    {
        audioSource.pitch = Random.Range(1 - pitchChangeAmount, 1 + pitchChangeAmount);
        audioSource.PlayOneShot(lose);
    }

   public void SetCharacter()
    {
        audioSource.pitch = Random.Range(1 - pitchChangeAmount, 1 + pitchChangeAmount);
        audioSource.PlayOneShot(hurt);
        GameObject spawnedGhost = Instantiate(ghost, startingPosition, transform.rotation);
        spawnedGhost.GetComponent<TimeBody>().rb = spawnedGhost.GetComponent<Rigidbody2D>();
        spawnedGhost.GetComponent<TimeBody>().pointsInTime = new List<PointInTime>(gameObject.GetComponent<TimeBody>().pointsInTime);
        spawnedGhost.GetComponent<TimeBody>().StartRewind();
        transform.parent = null;
        gameObject.transform.position = startingPosition;
        gameObject.GetComponent<TimeBody>().pointsInTime.Clear();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        rb.velocity = new Vector2(direction.x * movementSpeed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        RaycastHit2D frontHit = Physics2D.Raycast(new Vector2(groundCheck.position.x + .5f, groundCheck.position.y), Vector2.down, groundCheckDistance, jumpableLayers);
        RaycastHit2D backHit = Physics2D.Raycast(new Vector2(groundCheck.position.x - .5f, groundCheck.position.y), Vector2.down, groundCheckDistance, jumpableLayers);
        if (frontHit || backHit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
