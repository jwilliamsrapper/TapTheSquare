using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour
{
    private const string twitter = "https://twitter.com/imjaewilliams";
    private const string allMyLinks = "https://solo.to/imjaewilliams";


    public Text winStreak;
    public GameObject howToPlay;
    public AudioSource buttonSound;
    public GameObject stats;

    public Text roundsWon;
    public Text totalTaps;
    public Text gamesPlayed;
    public Text mostTaps;


    public void Start()
    {
        winStreak.text = "Win Streak " + PlayerPrefs.GetInt("winstreakss").ToString();
    }

    public void PlayGame()
    {

        Invoke("GoToGame", .55f);
        Instantiate(buttonSound, transform.position, Quaternion.identity);

    }

    public void GameStats()
    {
        stats.SetActive(true);

        int gamesLost = PlayerPrefs.GetInt("games") - PlayerPrefs.GetInt("gameswon");
        roundsWon.text = "Win - Loss: " + PlayerPrefs.GetInt("gameswon").ToString() + " - " + gamesLost.ToString();
        winStreak.text = "Current Win Streak: " + PlayerPrefs.GetInt("winstreakk").ToString();
        totalTaps.text = "Total # of Taps: " + PlayerPrefs.GetInt("alltaps").ToString();
        gamesPlayed.text = "Total Games Played: " + PlayerPrefs.GetInt("games").ToString();
        mostTaps.text = "Most Taps in a Round: " + PlayerPrefs.GetInt("myHighScore").ToString();
        Instantiate(buttonSound, transform.position, Quaternion.identity);
    }

    public void ExitStats()
    {
        stats.SetActive(false);
        Instantiate(buttonSound, transform.position, Quaternion.identity);
    }

    public void OpenLinks()
    {

        Application.OpenURL(allMyLinks);

    }

    public void Twitter()
    {

        Application.OpenURL(twitter);

    }

    public void Quit()
    {
        Application.Quit();
    }



    public void HowToPlay()
    {

        howToPlay.SetActive(true);
        Instantiate(buttonSound, transform.position, Quaternion.identity);

    }

    public void BackToMenu()
    {

        howToPlay.SetActive(false);
        Instantiate(buttonSound, transform.position, Quaternion.identity);

    }

    public void GoToGame()
    {
        SceneManager.LoadScene("game");
    }

}
