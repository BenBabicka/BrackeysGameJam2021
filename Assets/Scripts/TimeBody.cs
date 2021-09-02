using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{

    public bool isRewinding = false;

    public List<PointInTime> pointsInTime;

    public Rigidbody2D rb;
    bool placed;
    public bool player;
    List<PointInTime> pointInTimesTest;
    public GameManager gameManager;

    public Material setMaterial;
    public Material ghostMaterial;
    public GameObject setEffectPrefab;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        if(player)
        {
            pointsInTime = new List<PointInTime>();
        }
        pointInTimesTest = new List<PointInTime>(pointsInTime);
    }

    private void Update()
    {
        if (!player)
        {
            if (gameManager.playerHasLeftStartZone)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ResetData();
                }
            }
        }
    }

    public void ResetData()
    {
        if (!player)
        {
            pointsInTime = new List<PointInTime>(pointInTimesTest);
            StartRewind();
            placed = false;
        }
    }


    private void FixedUpdate()
    {
        if (!placed)
        {
            if (isRewinding)
                Rewind();
            else
                Record();
        }
    }

    void Record()
    {
        if (player)
        {
            if (Camera.main.GetComponent<PlayerCamera>().StartPlayer)
            {
                pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
            }
        }
 
    }
    void Rewind()
    {
        if (pointsInTime.Count > 0)
        {

            PointInTime pointInTime = pointsInTime[pointsInTime.Count - 1];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            pointsInTime.RemoveAt(pointsInTime.Count - 1);

        }
        else
        {
            placed = true;
            StopRewind();
        }
    }

   public void StartRewind()
    {
        gameObject.GetComponent<SpriteRenderer>().material = ghostMaterial;
        isRewinding = true;
        rb.isKinematic = true;
        transform.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public void StopRewind()
    {
        gameObject.GetComponent<SpriteRenderer>().material = setMaterial;

        isRewinding = false;
        rb.isKinematic = false;
        transform.GetComponent<BoxCollider2D>().isTrigger = false;
        GameObject go = Instantiate(setEffectPrefab, transform.position, transform.rotation);
        Destroy(go, 5f);
    }

}
