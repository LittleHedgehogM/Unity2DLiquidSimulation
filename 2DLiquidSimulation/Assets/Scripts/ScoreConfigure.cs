using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Game/ScoreConfigure")]
public class ScoreConfigure : ScriptableObject
{

    [Header("��ʼ�÷�")]
    public int startScore;

    [Header("���")]
    public int minScore;

    [Header("���")]
    public int maxScore;

    [Header("�ʴ�÷�")]
    public int questionScore;

    [Header("�����÷�")]
    public int interactScore;
}
