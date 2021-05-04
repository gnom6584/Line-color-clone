using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigationComponent : MonoBehaviour
{
    public const String PrefsTag = "CURRENT SCENE";

    public ScenesHolder scenesHolder;

    private String _nextScene;

    private Boolean _lastScene;
    
    private void Awake()
    {
        var thisSceneName = SceneManager.GetActiveScene().name;
        var thisIndex = scenesHolder.Scenes.ToList().FindIndex(it => it == thisSceneName);
        _lastScene = thisIndex >= scenesHolder.Scenes.Length - 1;
        if (!_lastScene)
            _nextScene = scenesHolder.Scenes[thisIndex + 1];
   
    }
    
    public void GoToNextScene()
    {
        if(_lastScene)
            return;
        
        SceneManager.LoadScene(_nextScene);
        PlayerPrefs.SetString(PrefsTag, _nextScene);
        PlayerPrefs.Save();
    }
}
