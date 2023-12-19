using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeFirstButton : MonoBehaviour
{
    private EventSystem eventSystem;
    public GameObject back;
    public GameObject scoreBoard;
    private UpgradeUIManager upgradeUIManager;
    public bool stop = false;

    // Start is called before the first frame update
    void Start()
    {
        eventSystem = GetComponentInChildren<EventSystem>();
        upgradeUIManager = GetComponent<UpgradeUIManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stop != true)
        {
            if (upgradeUIManager.upgradeCheck != false)
            {
                eventSystem.SetSelectedGameObject(back);
                stop = true;
            }
            else if (upgradeUIManager.upgradeCheck != true)
            {
                eventSystem.SetSelectedGameObject(scoreBoard);
                stop = true;
            }
        }
        
        
        
    }
}
