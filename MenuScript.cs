using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuScript : MonoBehaviour{ 

    public void LoadScene(string name) => UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    public void LoadScene(int index) => UnityEngine.SceneManagement.SceneManager.LoadScene(index);

    public void LoadScene(string name, float delay) => StartCoroutine(LoadSceneCoroutine(name, delay));
    public void LoadScene(int index, float delay) => StartCoroutine(LoadSceneCoroutine(index, delay));

    private IEnumerator LoadSceneCoroutine(string name, float delay){
        yield return new WaitForSeconds(delay < 0 ? 0 : delay);
        LoadScene(name);
    }
    private IEnumerator LoadSceneCoroutine(int index, float delay)
    {
        yield return new WaitForSeconds(delay < 0 ? 0 : delay);
        LoadScene(index);
    }
}
