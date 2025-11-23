using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SceneUnlockManagement : MonoBehaviour
{
    private CameraController myCameraController;
    private PlayerJump myPlayerJump;

    public int maxSceneIndex = 5;
    public int currentSceneIndex = 1;
    public int currentMaxSceneIndex = 5;
    public SceneLabel startScene;
    public SceneLabel currentScene;

    public List<SceneLabel> allScenes = new List<SceneLabel>();
    public List<SceneLabel> availableScenes =  new List<SceneLabel>();

    private void Start() 
    {
        allScenes = FindObjectsOfType<SceneLabel>().ToList<SceneLabel>();
        availableScenes.Add(startScene);
        myCameraController = FindObjectOfType<CameraController>();
        myPlayerJump = FindObjectOfType<PlayerJump>();
        ChangeSceneTo(1); 

    }

    public void UnlockNewScene() 
    {
        
    }

    public void ChangeSceneTo(int targetSceneIndex)
    {
        foreach(SceneLabel aLabel in allScenes)
        {
            Debug.Log("ChangeSceneTo " + targetSceneIndex);
            if (aLabel.SceneLabelIndex == targetSceneIndex)
            {

                myCameraController.ChangeSceneTo(aLabel);
                myPlayerJump.PlayerJumpTo(aLabel.spawnPoint.position);
                currentSceneIndex = aLabel.SceneLabelIndex;
                Debug.Log("currentSceneIndex =  " + currentSceneIndex);

                break;
            }
        }
    }

    public void GoPreviousScene() 
    {
        int prevSceneIndex = Mathf.Max(currentSceneIndex - 1, 0);
        ChangeSceneTo(prevSceneIndex); 
        
    }

    public void GoNextScene() 
    {
        int nextSceneIndex = Mathf.Max(currentSceneIndex + 1, 0);
        ChangeSceneTo(nextSceneIndex);
    }
}
