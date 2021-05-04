using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchScript : MonoBehaviour
{
    [SerializeField] private SceneNavigationComponent _sceneNavigationComponent;
    
    private void Awake()
    {
        var goToScene = PlayerPrefs.GetString(SceneNavigationComponent.PrefsTag, _sceneNavigationComponent.scenesHolder.Scenes.First());
        SceneManager.LoadScene(goToScene);
    }
}
