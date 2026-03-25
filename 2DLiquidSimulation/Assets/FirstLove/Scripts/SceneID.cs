using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum SceneTags
{
    Classroom,
    SchoolGate,
    Restaurant,
    EscapeRoom,
    ShoppingDistrict,
    InternetCafe,
    HappyEnding,
    BadEnding
}


public class SceneID : MonoBehaviour
{
    public SceneTags SceneTag;
}
