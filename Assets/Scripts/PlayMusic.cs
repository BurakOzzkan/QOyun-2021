using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public AudioClip bgMusic;
    AudioSource audioData;
    Int32 timeString;
    Int32 lastPlayed;

    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        timeString = (Int32)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        lastPlayed = timeString;
        audioData.PlayOneShot(bgMusic);

    }

    // Update is called once per frame
    void Update()
    {
        Int32 currTime = (Int32)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        if (((currTime - timeString) % 288) == 0 && currTime != lastPlayed)
        {
            lastPlayed = currTime;
            audioData.PlayOneShot(bgMusic);
        }
    }
}
