using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangButtToDieButt : MonoBehaviour
{
    private EventSystem eventSystem;
    private UIManager uiManager;

    public GameObject Restart;
    public GameObject Resume;


    // Start is called before the first frame update
    void Start()
    {
        eventSystem = GetComponentInChildren<EventSystem>();
        uiManager = GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (uiManager.Check != true)
        {
            if (uiManager.CheckShowDeadScene != false)
            {
                eventSystem.SetSelectedGameObject(Restart);
                uiManager.Check = true;
            }
            else if (uiManager.CheckShowDeadScene != true)
            {
                eventSystem.SetSelectedGameObject(Resume);
                uiManager.Check = true;
            }

        }
        
    }
}
