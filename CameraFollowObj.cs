using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObj : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _playerTransform;

    [Header("Flip Rotation Stats")]
    [SerializeField] private float _flipYRotationTime = 0.5f;

    private Coroutine _turnCoroutine;

    private PlayerMovement _player;

    private bool _isFacingRight;

    private void Awake()
    {
        _player = _playerTransform.gameObject.GetComponent<PlayerMovement>();

        _isFacingRight = _player.isFacingRight;
    }

    private void Update()
    {
        transform.position = _playerTransform.position;
    }

    public void CallTurn()
    {
        //_turnCoroutine = StartCoroutine(FlipYLerp());

        LeanTween.rotateY(gameObject, DetermineEndRotation(), _flipYRotationTime).setEaseInOutSine();
    }

    private IEnumerator FlipYLerp()
    {
        float startRotation = transform.localEulerAngles.y;
        float endRotationAmout = DetermineEndRotation();
        float yRotation = 0f;

        float elapsedTime = 0f;
        while (elapsedTime < _flipYRotationTime)
        {
            elapsedTime += Time.deltaTime;

            yRotation = Mathf.Lerp(startRotation, endRotationAmout, (elapsedTime / _flipYRotationTime));
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

            yield return null;
        }
    }

    private float DetermineEndRotation()
    {
        _isFacingRight = !_isFacingRight;

        if (_isFacingRight)
        {
            return 0f;
        }
        else
        {
            return 180f;
        }
    }
}