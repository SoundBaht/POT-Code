using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera CinemachineVirtualCamera;

    [Range(0,5)]
    [SerializeField] private float ShakeIntensity = 1.1f;
    private float ShakeTime = 0.2f;

    private float timer;
    private CinemachineBasicMultiChannelPerlin _cbmcp;

    [Header("References")]
    private PlayerScript playerScript;
    private Attack attack;


    void Awake()
    {
        CinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        GameObject player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerScript>();
        attack = player.GetComponentInChildren<Attack>();

        if (playerScript.comboCounter != 3)
        {
            StopShake();
        }
    }

    public void ShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin _cbmcp = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = ShakeIntensity;
        timer = ShakeTime;
    }

    void StopShake()
    {
        CinemachineBasicMultiChannelPerlin _cbmcp = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = 0f;
        timer = 0;
    }

    void Update()
    {
        if (playerScript.comboCounter == 3 && attack.Hitting == true)
        {
            StartCoroutine(DelayShakeCamera());
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                StopShake();
            }
        }
    }

    public IEnumerator DelayShakeCamera()
    {
        ShakeIntensity = 3.5f;
        yield return new WaitForSeconds(0.2f);
        ShakeCamera();
        
        
    }
}
