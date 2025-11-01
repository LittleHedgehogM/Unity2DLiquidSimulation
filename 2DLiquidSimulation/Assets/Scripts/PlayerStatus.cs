using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Status 
{
    Small,
    MediumLight,
    MediumDark,
    LargeLight,
    LargeDark
}

public class PlayerStatus : MonoBehaviour
{

    private Animator myAnimator;
    private Status myStatus;
    private int Darkness;
    public ScoreConfigure myScoreConfig;
    
    void Start()
    {
        myStatus = Status.Small;
        Darkness = myScoreConfig.startScore;
    }

    public void IncreaseDarknessByInteraction() 
    {
        Darkness += myScoreConfig.interactScore;
    }

    public void DecreaseDarknessByInteraction() 
    {
        Darkness -= myScoreConfig.interactScore;
    }

    public void IncreaseDarknessByQuestion()
    {
        Darkness += myScoreConfig.questionScore;
    }

    public void DecreaseDarknessByQuestion()
    {
        Darkness -= myScoreConfig.questionScore;
    }

    public Status checkStatus() 
    {
        return myStatus;
    }

    private void LevelUp() 
    {
        switch (myStatus)
        { 
            case Status.Small: 
            { 
                 break; 
            }
            case Status.MediumLight: 
            { 
                 break; 
            }
            case Status.MediumDark: 
            { 
                 break; 
            }
            
        }

        if (myStatus == Status.LargeLight 
            || myStatus == Status.LargeDark) 
        {
            // game over
        }
    }

    // create path go where player clicked

}
