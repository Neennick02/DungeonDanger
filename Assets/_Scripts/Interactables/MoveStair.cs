using System;
using System.Collections;
using UnityEngine;

public class MoveStair : MonoBehaviour
{
    [SerializeField] private float duration = 5f;
    private float time;


    [SerializeField] private float targetHeight;
    private Vector3 target;

    public static event Action OnDisableCamera;
    private void Start()
    {
        target = new Vector3(transform.position.x, targetHeight, transform.position.z); 
    }
    public void MoveToTargetPosition()
    {
        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        time += Time.deltaTime;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(transform.position, target, time / duration);

            yield return null;  
        }
        OnDisableCamera?.Invoke();
        Debug.Log("dsa");
    }
}
