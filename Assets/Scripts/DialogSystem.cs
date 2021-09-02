using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public List<string> Sentences;
    public Text textElement;
    string constructedSentence;
    int index;
    public float timeTillNextChar = 0.1f;
    public float displayTime = 2f;
    float displayTimer;
    float timeTillNextCharTimer;
    public float startDelay;
    void Start()
    {
        timeTillNextCharTimer = timeTillNextChar;
        displayTimer = displayTime;

    }

    // Update is called once per frame
    void Update()
    {
        startDelay -= Time.deltaTime;
        if (startDelay <= 0)
        {
            if (Sentences.Count > 0)
            {
                if (index < Sentences[0].Length)
                {
                    timeTillNextCharTimer -= Time.deltaTime;
                    if (timeTillNextCharTimer < 0)
                    {
                        constructedSentence = constructedSentence + Sentences[0][index];
                        index++;
                        timeTillNextCharTimer = timeTillNextChar;
                    }
                }
                else
                {
                    displayTimer -= Time.deltaTime;
                    if (displayTimer < 0)
                    {
                        index = 0;
                        Sentences.RemoveAt(0);
                        constructedSentence = "";
                        displayTimer = displayTime;
                    }
                }
                textElement.text = constructedSentence;

            }
        }
    }

}
