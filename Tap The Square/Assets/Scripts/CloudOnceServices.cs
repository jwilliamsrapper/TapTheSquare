using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CloudOnce;

public class CloudOnceServices : MonoBehaviour
{
    public static CloudOnceServices instance;  // creates singleton



    private void Awake()
    {
        testSingleton();
    }


    private void testSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void submitScoreToLeaderBoard(int score)
    {
        Leaderboards.MostTaps.SubmitScore(score);
    }

    public void submitScoreToOtherLeaderBoard(int score)
    {
        Leaderboards.moreTaps.SubmitScore(score);
    }



}
