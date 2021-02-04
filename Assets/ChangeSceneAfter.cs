using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneAfter : MonoBehaviour
{
    public GameObject gameOVerui;
    void Start()
    {
        StartCoroutine(start(2f));
    }
    IEnumerator start(float t)
    {
        yield return new WaitForSeconds(t);
        gameOVerui.SetActive(true);
    }
}
