using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> musicClips;

    List<AudioClip> queue = new List<AudioClip>();
    int playIndex;
    public bool spawnedIn;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);

    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
           
            spawnedIn = true;
        }
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (FindObjectOfType<MusicManager>().spawnedIn && spawnedIn == false)
                Destroy(gameObject);
        }

        if (spawnedIn)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                if (FindObjectOfType<MusicManager>().gameObject != gameObject)
                    Destroy(FindObjectOfType<MusicManager>().gameObject);
            }
        }
        if (queue.Count != musicClips.Count)
        {
            int randomIndex = Random.Range(0, musicClips.Count);
            if (!queue.Contains(musicClips[randomIndex]))
            {
                queue.Add(musicClips[randomIndex]);
            }
        }

        if (playIndex == musicClips.Count)
        {
            playIndex = 0;
        }
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(queue[playIndex]);
            playIndex++;
        }
    }
}
