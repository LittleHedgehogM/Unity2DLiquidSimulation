using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{

    private SceneUnlockManagement mySceneManagement;
    private PlayerStatus myPlayerStatus;
    public Slider mySlider;

    [Header("UI Animation")]
    public float transitionTime = 0.5f;
    

    void Start()
    {
        mySceneManagement = FindObjectOfType<SceneUnlockManagement>();
        myPlayerStatus = FindObjectOfType<PlayerStatus>();

        myPlayerStatus.ResetValue();
        mySlider.value = myPlayerStatus.getValue();
        Debug.Log("my Slider value " + mySlider.value);

    }

    public void GoLeftScene()
    {
        Debug.Log("GoLeftScene");
        mySceneManagement.GoPreviousScene();
    }

    public void GoRightScene()
    {
        Debug.Log("GoRightScene");
        mySceneManagement.GoNextScene();
    }

    private void OnEnable()
    {
        PlayerStatus.ValueChange += UpdateUIValue;
    }

    private void OnDisable()
    {
        PlayerStatus.ValueChange -= UpdateUIValue;
    }

    public void UpdateUIValue(float targetValue)
    {
        DOTween.Sequence()
        .Append(mySlider.DOValue(targetValue, transitionTime))
        .WaitForCompletion();
    }

    public void DisableGoLeft()
    {

    }

    public void DisableGoRight()
    {

    }

    public void EnableGoRight()
    {

    }
    
    public void EnableGoLeft()
    {
        

    }


}
