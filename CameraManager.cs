using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    [SerializeField] private CinemachineVirtualCamera[] _allVirtualCameras;

    [SerializeField] private float _fallPanAmount = 0.25f;
    [SerializeField] private float _fallYPanTime = 0.35f;
    public float _fallSpeedYDampingChangeThreshold = -15f;

    public bool IsLerpingYDamping { get; private set; }
    public bool LerpedFromPlayerFalling { get; set; }

    private Coroutine _lerpYPanCoroutine;
    private Coroutine _panCameraCoroutine;

    private CinemachineVirtualCamera _currentCamera;
    private CinemachineFramingTransposer _framingTransposer;

    private float _normYPanAmout;

    private Vector2 _startingTrackedObjectoffset;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        for (int i = 0; i < _allVirtualCameras.Length; i++)
        {
            if (_allVirtualCameras[i].enabled)
            {
                _currentCamera = _allVirtualCameras[i];

                _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            }   
        }

        _normYPanAmout = _framingTransposer.m_YDamping;

        _startingTrackedObjectoffset = _framingTransposer.m_TrackedObjectOffset;
    }

    #region Lerp the Y Damping

    public void lerpYDamping(bool isPlayerFalling)
    {
        _lerpYPanCoroutine = StartCoroutine(LerpYAction(isPlayerFalling));
    }


    private IEnumerator LerpYAction(bool isPlayerFalling)
    {
        IsLerpingYDamping = true;

        float startDampAmount = _framingTransposer.m_YDamping;
        float endDamAmount = 0f;

        if (isPlayerFalling)
        {
            endDamAmount = _fallPanAmount;
            LerpedFromPlayerFalling = true;
        }
        else
        {
            endDamAmount = _normYPanAmout;
        }

        float elapsedtime = 0f;
        while (elapsedtime < _fallPanAmount)
        {
            elapsedtime += Time.deltaTime;

            float lerpedPanAmount = Mathf.Lerp(startDampAmount, endDamAmount, (elapsedtime / _fallYPanTime));
            _framingTransposer.m_YDamping = lerpedPanAmount;

            yield return null;
        }
        
        IsLerpingYDamping = false;
    }

    #endregion

    #region Pan Camera

    /* public void PanCameraOnContact(float panDistance, float panTime, PanDirection panDirection, bool panToStartingPos)
    {
        _panCameraCoroutine = StartCoroutine(PanCamera(panDistance, panTime, panDirection, panToStartingPos));
    } */

    /* private IEnumerator PanCamera(float panDistance, float panTime, PanDirection panDirection, bool panToStartingPos)
    {
        Vector2 endPos = Vector2.zero;
        Vector2 startingPos = Vector2.zero;

        if (!panToStartingPos)
        {
            switch (panDirection)
            {
                case PanDirection.Up:
                    endPos = Vector2.up;
                    break;
                case PanDirection.Down:
                    endPos = Vector2.down;
                    break;
                case PanDirection.Left:
                    endPos = Vector2.right;
                    break;
                case PanDirection.Right:
                    endPos = Vector2.left;
                    break;
                default:
                    break;
            }
 
            endPos *= panDistance;
            startingPos = _startingTrackedObjectoffset;

            endPos += startingPos;
        }
        else
        {
            startingPos = _framingTransposer.m_TrackedObjectOffset;
            endPos = _startingTrackedObjectoffset;
        }

        float elapsedTime = 0f;
        while (elapsedTime < panTime)
        {
            elapsedTime += Time.deltaTime;
            
            Vector3 panlerp = Vector3.Lerp(startingPos, endPos, (elapsedTime / panTime));
            _framingTransposer.m_TrackedObjectOffset = panlerp;

            yield return null;
        }
    } */

    #endregion

    #region Swap Caneras

    public void SwapCamera(CinemachineVirtualCamera cameraFromLeft, CinemachineVirtualCamera cameraFromRight, Vector2 triggerExitDirection)
    {
        if (_currentCamera == cameraFromLeft && triggerExitDirection.x > 0f)
        {
            cameraFromRight.enabled = true;

            cameraFromLeft.enabled = false;

            _currentCamera = cameraFromRight;

            _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }
        else if (_currentCamera == cameraFromRight && triggerExitDirection.x < 0f)
        {
            cameraFromLeft.enabled = true;

            cameraFromRight.enabled = false;

            _currentCamera = cameraFromLeft;

            _framingTransposer = _currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }
    }
    

    #endregion

}

