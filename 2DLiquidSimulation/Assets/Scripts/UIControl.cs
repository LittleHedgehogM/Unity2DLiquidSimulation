using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{

    private SceneUnlockManagement mySceneManagement;
    private PlayerStatus myPlayerStatus;
    public Slider mySlider;

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
