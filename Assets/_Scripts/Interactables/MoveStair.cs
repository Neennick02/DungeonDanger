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

    #region OnEnable
    private void OnEnable()
    {
        SaveStatueInteractable.OnSavePlayerData += SavePosition;
        GameManager.OnLoad += LoadPosition;
    }

    private void OnDisable()
    {
        SaveStatueInteractable.OnSavePlayerData -= SavePosition;
        GameManager.OnLoad -= LoadPosition;
    }
    #endregion
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
    private void SavePosition()
    {
        PlayerPrefs.SetFloat(gameObject.name + "moveAble", transform.position.y);
    }
    private void LoadPosition()
    {
        Debug.Log("load0");
        float yPos = PlayerPrefs.GetFloat(gameObject.name + "moveAble", transform.position.y);
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
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
