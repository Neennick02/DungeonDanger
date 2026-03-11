using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class CustomCamera : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cam;

    public static event Action OnCutSceneStart;
    public static event Action OnCutSceneEnd;

    private void OnEnable()
    {
        MoveStair.OnDisableCamera += DisableCam;
    }

    private void OnDisable()
    {
        MoveStair.OnDisableCamera -= DisableCam;
    }
    public void DisableCam()
    {
        cam.Follow = null;
        cam.LookAt = null;
        gameObject.SetActive(false);

        OnCutSceneEnd?.Invoke();
    }

    public void DelayAfterSeconds(float seconds)
    {
        StartCoroutine(DisableAfterTime(seconds));
    }

    public IEnumerator DisableAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        DisableCam();
    }

    public void StartCutScene()
    {
        OnCutSceneStart?.Invoke();
    }

    public void SetTarget(Transform target)
    {
        OnCutSceneStart?.Invoke();

        cam.Follow = target;
        cam.LookAt = target;
    }
}
