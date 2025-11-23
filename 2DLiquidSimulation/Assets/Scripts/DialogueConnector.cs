using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
public class DialogueConnector : MonoBehaviour
{
    private PlayerStatus myPlayerStatus;

    void Start()
    {
        myPlayerStatus = FindObjectOfType<PlayerStatus>();
    }

    [YarnCommand("IncreaseScore")]
    public void IncreaseScore()
    {
        myPlayerStatus.IncreaseDarknessByInteraction();
    }

    [YarnCommand("DecreaseScore")]
    public void DecreaseScore()
    {
        myPlayerStatus.DecreaseDarknessByInteraction();
    }


}
