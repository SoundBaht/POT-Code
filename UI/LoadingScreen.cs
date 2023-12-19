using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    void Start() {
        StartCoroutine(LoadToGameplay());
    }

    IEnumerator LoadToGameplay() {
        yield return new WaitForSeconds(3f);
        SceneController.instance.LoadScene("GamePlay");
    }
}
