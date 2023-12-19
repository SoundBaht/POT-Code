using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Developer
{
    [MenuItem("Developer/Add Coins")]
    public static void GiveCoinsToPlayer() {
        Character.instance.currentCurrency = 99999;
        Debug.Log("Give 99999 coins to Player");
    }

    [MenuItem("Developer/Delete All PlayerPrefs")]
    public static void ResetAllPlayerPrefs() {
        PlayerPrefs.DeleteAll();
        Debug.Log("All PlayerPrefs has delete!");
    }

    [MenuItem("Developer/Move to Boss room")]
    public static void MoveToBossRoom() {
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector2(656.8f,136f);
        Debug.Log("Moved Player to Boss Room");
    }
}
