using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowManager : MonoBehaviour
{
    public static SlowManager instance;
    public float slowdownFactor = .05f;
    public float slowdownLength = 2f;
    public float transitionDuration = 1f; 
    public float originalTimeScale;
    public float originalFixedDeltaTime;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void DoSlowmotion() {
        originalTimeScale = Time.timeScale;
        originalFixedDeltaTime = Time.fixedDeltaTime;

        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.2f;
    }

    public void UndoSlowmotion() {
        // Time.timeScale += (1f/slowdownLength) * Time.unscaledDeltaTime;
        // Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

        StartCoroutine(UndoSlowmotionSmooth());
    }

    private IEnumerator UndoSlowmotionSmooth()
    {
        float elapsedTime = 0f;
        float startScale = Time.timeScale;

        while (elapsedTime < transitionDuration)
        {
            Time.timeScale = Mathf.Lerp(startScale, originalTimeScale, elapsedTime / transitionDuration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = originalTimeScale;

        Time.fixedDeltaTime = originalFixedDeltaTime;
    }
}
