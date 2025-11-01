using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SceneUnlockManagement : MonoBehaviour
{

    public static SceneUnlockManagement instance;

    public int maxSceneIndex = 5;
    public int currentSceneIndex = 1;
    public int currentMaxSceneIndex = 1;
    public SceneLabel startScene;
    public SceneLabel currentScene;

    public List<SceneLabel> allScenes = new List<SceneLabel>();
    public List<SceneLabel> availableScenes =  new List<SceneLabel>();

    private void Start() 
    {
        allScenes = FindObjectsOfType<SceneLabel>().ToList<SceneLabel>();
        availableScenes.Add(startScene);
    }

    public void UnlockNewScene() 
    {
        
    }

    public void ChangeSceneTo(int targetSceneIndex) 
    {
        
    }

    public void GoPreviousScene() 
    {
    
    }

    public void GoNextScene() 
    {
        
    }
}
