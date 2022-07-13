using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneControllerManager : SingletonBehaviour<SceneControllerManager>
{
    private bool isFading;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private Image fadeImage = null;
    public SceneName startingSceneName;
    private Player player;

    public void FadeAndLoadScene(string sceneName, Vector3 spawnLocation)
    {
        if (!isFading)
        {
            StartCoroutine(FadeAndSwitchScene(sceneName, spawnLocation));
        }
    }

    private IEnumerator Start()
    {
        player = GameObject.FindWithTag(Tags.Player).GetComponent<Player>();
        fadeImage.color = new Color(0f, 0f, 0f, 1f);
        fadeCanvasGroup.alpha = 1;

        
        yield return StartCoroutine(LoadSeneAtSetActive(startingSceneName.ToString()));

        EventHandler.CallAfterSceneLoadEvent();
        StartCoroutine(Fade(0f));
    }

    public IEnumerator Fade(float finalAlpha)
    {
        isFading = true;
        fadeCanvasGroup.blocksRaycasts = true;

        float fadeSpeed = Mathf.Abs(fadeCanvasGroup.alpha - finalAlpha) / fadeDuration;

        while (!Mathf.Approximately(fadeCanvasGroup.alpha, finalAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, finalAlpha, fadeSpeed * Time.deltaTime);

            yield return null;
        }

        isFading = false;
        fadeCanvasGroup.blocksRaycasts = false;
    }
    
    private IEnumerator FadeAndSwitchScene(string sceneName, Vector3 spawnLocation)
    {
        EventHandler.CallBeforeSceneUnloadFadeOutEvent();
        yield return StartCoroutine(Fade(1f));

        player.transform.position = spawnLocation;
        EventHandler.CallBeforeSceneUnloadEvent();

        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        yield return StartCoroutine(LoadSeneAtSetActive(sceneName));
        
        EventHandler.CallAfterSceneLoadEvent();

        StartCoroutine(Fade(0f));
        EventHandler.CallAfterSceneLoadEvent();
    }

    public IEnumerator LoadSeneAtSetActive(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        Scene newlyLoadScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newlyLoadScene);
    }

}
