using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Game/ScoreConfigure")]
public class ScoreConfigure : ScriptableObject
{

    [Header("初始得分")]
    public int startScore;

    [Header("最低")]
    public int minScore;

    [Header("最高")]
    public int maxScore;

    [Header("问答得分")]
    public int questionScore;

    [Header("互动得分")]
    public int interactScore;
}
