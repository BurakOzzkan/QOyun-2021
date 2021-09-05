using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour
{
    Int32 timeString;

    // Start is called before the first frame update
    void Start()
    {
        timeString = (Int32)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

    }

    // Update is called once per frame
    void Update()
    {
        Int32 currTime = (Int32)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        if (currTime - timeString > 36)
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
