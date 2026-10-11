using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private TransitionFade transitionFade;

    public void LoadNextScene()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        string nextScene = SceneManager.GetSceneByBuildIndex(nextIndex).name;

        LoadScene(nextScene);

    }

    private void LoadScene(string sceneName)
    {
        if(transitionFade != null && transitionFade.gameObject.activeInHierarchy)
        {
            transitionFade.FadeAndLoad(sceneName);
        } 
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void LoadSceneByName(string name)
    {
        LoadScene(name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetValues()
    {
        ItemPoolManager.coins = 0;
        ItemPoolManager.ItemPool.Clear();
        ItemPoolManager.inventory.Clear();
    }
}
