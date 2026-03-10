using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class CustomCamera : MonoBehaviour
{
    private CinemachineCamera cam;

    private void Start()
    {
        cam = GetComponent<CinemachineCamera>();
    }

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
        Debug.Log("dddsa");

        gameObject.SetActive(false);    
    }

    public void SetTarget(Transform target)
    {
        cam.Follow = target;
        cam.LookAt = target;
    }
}
