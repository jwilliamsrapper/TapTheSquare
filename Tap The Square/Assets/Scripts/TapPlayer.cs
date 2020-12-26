using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Advertisements;

public class TapPlayer : MonoBehaviour
{

    int tapCount;

    public Text score;

    public Text timer;

    float time;

    float roundCount; // number of taps needed to win round


    int roundsWon;

    public GameObject retry;  // retry button
    public GameObject gameOver; // game over text
    public GameObject mainMenu; // home/main menu button
    public GameObject directions;
    public GameObject tapEffect;
    public GameObject gameOverEffect;
    public GameObject pauseButton;

    public GameObject player;

    static int loadCount;

    bool tapMe;

    public AudioSource tapNoise;


    int gamesPlayed; // total number of games played

    bool isCounting;

    Vector3 temp;



    public GameObject youWon;
    public GameObject youLost;

    public Text tapsToWin;


    Vector2 screenPosition;
    Vector2 worldPosition;


    public Text theTaps; // 

    int gameTaps; // diffrence in taps required to win and taps you got



    bool activeStreak;
    int winStreakCount;  // current win streak

    public Text winStreak;



    void Start()
    {
        time = Random.Range(15.0f, 30f);

        isCounting = true;
        tapMe = true;
        loadCount++;



        if (time <= 20 && time >= 15)
        {
            roundCount = Random.Range(95f, 135f);
        }

        if (time <= 25 && time >= 20)
        {
            roundCount = Random.Range(135f, 165f);
        }

        if (time <= 30 && time >= 25)
        {
            roundCount = Random.Range(165f, 220f);
        }


        tapsToWin.text = roundCount.ToString("f0") + " Taps to win";

        Advertisement.Initialize("3732271");

        if (loadCount % 3 == 0)
        {
            if (Advertisement.IsReady())
            {
                Advertisement.Show();
            }
        }


        winStreak.text = "Win Streak " + PlayerPrefs.GetInt("winstreakk").ToString();
        roundsWon = PlayerPrefs.GetInt("gameswon");
        winStreakCount = PlayerPrefs.GetInt("winstreakk");
        activeStreak = true;

    }

    private void Update()
    {

        screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);


        if (tapMe == false)
        {
            Destroy(tapsToWin);
            Destroy(winStreak);

        }


        if (isCounting && tapMe == false && PauseGame.paused == false)
        {
            time -= Time.deltaTime;
            timer.text = "Time 00:" + time.ToString("f0");

            if (time < 9.5f)
            {
                timer.text = "Time 00:0" + time.ToString("f0");
            }
        }


        if (isCounting && PauseGame.paused == false)
        {

            // shrinks the square if you stop tapping
            temp = transform.localScale;
            if (temp.x >= .08f && temp.y >= .08f && tapMe == false)
            {
                temp.x -= .00021f;
                temp.y -= .00021f;
                transform.localScale = temp;
            }

            if (Input.GetMouseButtonDown(0))
            {
                // constrains the tapping area
                if (worldPosition.x <= .75f && worldPosition.y <= .75f && worldPosition.x > -.75f && worldPosition.y > -.75f)
                {

                    tapMe = false;
                    directions.SetActive(false);
                    pauseButton.SetActive(true);
                    tapCount++;
                    score.text = tapCount.ToString() + " Taps";



                    Instantiate(tapNoise, transform.position, Quaternion.identity);
                    Instantiate(tapEffect, transform.position, Quaternion.identity);

                    // grows the square based per each tap
                    if (temp.x <= .33f && temp.y <= .33f)
                    {
                        temp.x += .0038f;
                        temp.y += .0038f;
                        transform.localScale = temp;
                    }

                }
            }


        }

        if (time <= 0)
        {
            isCounting = false;


        }

        if (isCounting == false)
        {

            Invoke("GameOver", .25f);

        }
    }


    public void GameOver()
    {

        if (tapCount >= roundCount || tapCount == roundCount)
        {
            youWon.SetActive(true);
            theTaps.text = "You got " + tapCount.ToString() + " out of " + roundCount.ToString("f0") + " taps needed";
        }

        else
        {
            youLost.SetActive(true);
            activeStreak = false;
            theTaps.text = "You got " + tapCount.ToString() + " out of " + roundCount.ToString("f0") + " taps needed";

        }



        retry.SetActive(true);
        gameOver.SetActive(true);
        mainMenu.SetActive(true);
        Destroy(player);
        pauseButton.SetActive(false);

        timer.text = "Time's Up";


        CloudOnceServices.instance.submitScoreToOtherLeaderBoard(tapCount);

        if (PlayerPrefs.GetInt("myHighScore") < tapCount)
        {
            PlayerPrefs.SetInt("myHighScore", tapCount);
            PlayerPrefs.Save();
        }
    }


    private void OnDestroy()
    {

        tapCount += PlayerPrefs.GetInt("alltaps");
        PlayerPrefs.SetInt("alltaps", tapCount); // set total number of taps

        if (tapMe == false)
        {
            gamesPlayed = PlayerPrefs.GetInt("games");
            gamesPlayed++;
            PlayerPrefs.SetInt("games", gamesPlayed); // set total number of games played
            PlayerPrefs.Save();
        }


        if (tapCount >= roundCount || tapCount == roundCount)
        {

            if (activeStreak && tapMe == false)
            {

                roundsWon++;
                winStreakCount++;


                CloudOnceServices.instance.submitScoreToLeaderBoard(winStreakCount);

                // setting the total games won count
                if (PlayerPrefs.GetInt("gameswon") < roundsWon)
                {
                    PlayerPrefs.SetInt("gameswon", roundsWon);
                    PlayerPrefs.Save();
                }


                // setting win streak count
                if (PlayerPrefs.GetInt("winstreakk") < winStreakCount)
                {
                    PlayerPrefs.SetInt("winstreakk", winStreakCount);
                    PlayerPrefs.Save();
                }
            }

        }


        if (activeStreak == false)
        {

            winStreakCount = 0;
            PlayerPrefs.SetInt("winstreakk", winStreakCount);
            PlayerPrefs.Save();
        }

    }


}
