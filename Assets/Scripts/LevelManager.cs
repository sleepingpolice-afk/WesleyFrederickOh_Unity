using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    void Awake()
    {
        //do Something on Awake E.g. make an object appearence false
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        Animator transitionAnim = GameObject.Find("SceneTransition").GetComponent<Animator>();
        transitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(1);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        Debug.Log("Loading scene: " + sceneName);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }

        Player.Instance.transform.position = new(0, -4.5f);
        transitionAnim.SetTrigger("End");
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
}