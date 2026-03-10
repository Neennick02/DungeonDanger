using System;
using System.Collections;
using UnityEngine;

public class MoveStair : MonoBehaviour
{
    [SerializeField] private float duration = 5f;
    private float time;


    [SerializeField] private float targetHeight;
    private Vector3 target;
    private Vector3 start;
    public static event Action OnDisableCamera;
    bool done = false;
    private void Start()
    {
        start = transform.position;
        target = new Vector3(transform.position.x, targetHeight, transform.position.z); 
    }
    public void MoveToTargetPosition()
    {
        StopAllCoroutines();    

        if(!done)
        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(start, target, time / duration);

            yield return null;  
        }

        transform.position = target;
        OnDisableCamera?.Invoke();
        done = true;
    }
}
