using DG.Tweening;
using UnityEngine;
using Yarn.Unity;

public class SceneItems : MonoBehaviour
{
    public bool fadeOnStart;
    private SpriteRenderer mySpriteRenderer;

    private void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        mySpriteRenderer.DOFade(fadeOnStart?0:1.0f,0);
    }
    [YarnCommand("ShowItem")]
    public void ShowItem()
    {
        mySpriteRenderer.DOFade(1, 0.5f);
    }

    [YarnCommand("FadeItem")]
    public void HideItem() 
    {
        mySpriteRenderer.DOFade(0, 0.5f);
    }
}
