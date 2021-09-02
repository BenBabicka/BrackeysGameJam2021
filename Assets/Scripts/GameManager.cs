using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public float roundTime;
    public Text roundTimeText;

    public bool endLevel;

    public bool playerHasLeftStartZone;

    public bool paused;

    public GameObject pausedUI;
    public GameObject gameUI;
    public GameObject timesUpUI;
    public Animator endEffectAnimator;

    bool restartPlatforms;
    public AudioSource audioSource;
    public AudioClip click;
    void Start()
    {
        endEffectAnimator = FindObjectOfType<FinishGoal>().GetComponentInChildren<Animator>();
        roundTimeText.text = "Time\n" + Mathf.RoundToInt(roundTime).ToString();
        if (FindObjectOfType<MusicManager>())
            audioSource = FindObjectOfType<MusicManager>().audioSource;

    }

    void Update()
    {
        if (!endLevel)
        {
            if (Camera.main.GetComponent<PlayerCamera>().StartPlayer)
            {
                roundTime -= Time.deltaTime;
                roundTimeText.text = "Time\n" + Mathf.RoundToInt(roundTime).ToString();
                if (!restartPlatforms)
                {
                    if (FindObjectOfType<MovePlatform>())
                    {
                        foreach (var item in FindObjectsOfType<MovePlatform>())
                        {
                            item.waitBool = true;
                            item.waitTimer = item.wait;
                            item.ResetPosition();
                        }
                        restartPlatforms = true;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }

            if(roundTime <= 0)
            {
                roundTime = 0;
                timesUpUI.SetActive(true);
                FindObjectOfType<PlayerController>().Lose();
                endEffectAnimator.SetTrigger("EndLevel");
                endEffectAnimator.transform.parent = Camera.main.transform;
                endEffectAnimator.transform.localPosition = new Vector3(0, 0, 10);
                StartCoroutine(RestartLevel(3));

                endLevel = true;
            }

        }

        if(paused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }
    IEnumerator RestartLevel(float waitTime)
    {
        
        yield return new WaitForSeconds(waitTime-1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Pause()
    {
        audioSource.PlayOneShot(click);

        paused = !paused;
        if(paused)
        {
            pausedUI.SetActive(true);
            gameUI.SetActive(false);
            Time.timeScale = 0;
        }    
        if(!paused)
        {
            pausedUI.SetActive(false);
            gameUI.SetActive(true);
            Time.timeScale = 1;
        }
    }

    public void Restart()
    {
        audioSource.PlayOneShot(click);
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void BackToMainMenu()
    {
        audioSource.PlayOneShot(click);

        Time.timeScale = 1;

        SceneManager.LoadScene(0);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerHasLeftStartZone = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHasLeftStartZone = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHasLeftStartZone = false;
        }
    }
}
