using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Yarn.Unity;

public class CameraJump : MonoBehaviour
{
    private List<SceneID> scenes = new List<SceneID>();

    private void Start()
    {
        scenes = FindObjectsOfType<SceneID>().ToList();
    }

    [YarnCommand("ChangeSceneTo")]
    public void ChangeSceneTo(string SceneName)
    {
        if (scenes.Count == 0) 
        {
            scenes = FindObjectsOfType<SceneID>().ToList();
        }
        foreach (SceneID scene in scenes) 
        {
            if (scene.SceneTag.ToString().Equals(SceneName))
            {
                Vector3 targetPos = scene.transform.position;
                targetPos.z = -10;
                this.transform.position = targetPos;
                return;
            }
        }
    }
}
