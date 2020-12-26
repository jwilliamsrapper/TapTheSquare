using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public static bool paused;

    public GameObject pauseScreen;
    public AudioSource buttonSound;

    public void Paused()
    {
        Instantiate(buttonSound, transform.position, Quaternion.identity);
        paused = true;
        pauseScreen.SetActive(true);

    }

    public void UnPaused()
    {
        Instantiate(buttonSound, transform.position, Quaternion.identity);
        paused = false;
        pauseScreen.SetActive(false);


    }
}
