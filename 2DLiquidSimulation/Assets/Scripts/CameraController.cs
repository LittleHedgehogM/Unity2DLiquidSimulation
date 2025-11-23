using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    
    public void ChangeSceneTo(SceneLabel targetScene)
    {
        this.transform.position = new Vector3(targetScene.transform.position.x, targetScene.transform.position.y, -10) ;
        
    }

}
