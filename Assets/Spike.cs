using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (FindObjectOfType<TimeBody>())
            {
                foreach (var item in FindObjectsOfType<TimeBody>())
                {
                    item.ResetData();
                }
            }
            if (FindObjectOfType<MovePlatform>())
            {
                foreach (var item in FindObjectsOfType<MovePlatform>())
                {
                    item.ResetPosition();
                }
            }
            collision.GetComponent<PlayerController>().SetCharacter();
           
        }
    }
}
