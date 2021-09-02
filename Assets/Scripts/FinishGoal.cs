using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinishGoal : MonoBehaviour
{

    public Animator endEffectAnimator;
    IEnumerator NextLevel(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
           StartCoroutine(NextLevel(3));
            collision.GetComponent<PlayerController>().Win();
            FindObjectOfType<GameManager>().endLevel = true;
            endEffectAnimator.SetTrigger("EndLevel");
            endEffectAnimator.transform.parent = Camera.main.transform;
            endEffectAnimator.transform.localPosition = new Vector3(0, 0, 10);
        }
    }

  


}
