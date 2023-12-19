using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingAlphaScene : MonoBehaviour
{
    private MiniBScript miniBScript;

    // Start is called before the first frame update
    void Start()
    {
        GameObject mini = GameObject.Find("Mini Boss");
        miniBScript = mini.GetComponent<MiniBScript>();
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.CompareTag("Player"))
        {
            SceneManager.LoadScene(4);
        }
    }
}
