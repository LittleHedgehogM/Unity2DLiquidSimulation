using System;
using UnityEngine;

[System.Serializable]
public enum Status 
{
    Small = 1,
    MediumLight = 2,
    MediumDark = 3,
    LargeLight = 4,
    LargeDark = 5
}

public class PlayerStatus : MonoBehaviour
{

    private Animator myAnimator;
    private Navigate myNavigation;
    public Status myStatus;
    private int currentValue;
    public ScoreConfigure myScoreConfig;
    private Vector2 myMovement = Vector2.zero;
    public static Action<float> ValueChange;

    void Awake()
    {
        //myStatus = Status.MediumLight;
        currentValue = myScoreConfig.startScore;
        myAnimator = GetComponent<Animator>();
        myNavigation = GetComponent<Navigate>();
    }

    private void Update()
    {
        
        myMovement = myNavigation.getMovementVector();
        myAnimator.SetFloat("Speed", myMovement.sqrMagnitude);
        myAnimator.SetFloat("MovementX", myMovement.x);
        myAnimator.SetInteger("Form", (int)myStatus);
        // Debug.Log((int)myStatus + ", " + myMovement);
    }

    public void ResetValue()
    {
        currentValue = myScoreConfig.startScore;

    }

    public float getValue()
    {
        
        float value = currentValue * 1.0f/ myScoreConfig.maxScore;
        Debug.Log(currentValue +  "/"+ myScoreConfig.maxScore + ", " + value);
        return value;
    }

    public void IncreaseDarknessByInteraction() 
    {
        currentValue += myScoreConfig.interactScore;
        ValueChange?.Invoke(getValue());

    }

    public void DecreaseDarknessByInteraction() 
    {
        currentValue -= myScoreConfig.interactScore;
        ValueChange?.Invoke(getValue());

    }

    public void IncreaseDarknessByQuestion()
    {
        currentValue += myScoreConfig.questionScore;
        ValueChange?.Invoke(getValue());
    }

    public void DecreaseDarknessByQuestion()
    {
        currentValue -= myScoreConfig.questionScore;
        ValueChange?.Invoke(getValue());
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
