using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyKey : MonoBehaviour
{
    [Header("Scene Settings")]
    [Tooltip("Scene Index")]
    public int sceneIndex = 1;

    [Header("Optional Delay")]
    public float delayBeforeLoad = 0f;

    private bool hasPressed = false;

    void Update()
    {
        if (!hasPressed && Input.anyKeyDown)
        {
            hasPressed = true;
            Invoke(nameof(LoadScene), delayBeforeLoad);
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
