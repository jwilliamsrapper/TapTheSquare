using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{



    public AudioSource buttonSound;


    public void Start()
    {
        Advertisement.Initialize("3732271");
    }


    public void Replay()
    {
        SceneManager.LoadScene("game");

    }


    public void Menu()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }

        SceneManager.LoadScene("menu");
    }






    public void Share()
    {
        StartCoroutine(TakeSSAndShare());
        Instantiate(buttonSound, transform.position, Quaternion.identity);
    }


    private IEnumerator TakeSSAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Destroy(ss);

        new NativeShare().AddFile(filePath).SetSubject("Tap The Sqaure!").SetText("I bet you can't get more taps than me lol. Download Tap The Square! for free here https://jaewilliams.com/mobile-games ").Share();


    }


}
