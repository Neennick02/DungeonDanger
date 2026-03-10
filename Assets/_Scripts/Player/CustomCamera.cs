using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class CustomCamera : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cam;
    [SerializeField] private PlayerMovement movement;
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
        movement.State = PlayerMovement.PlayerState.Locomotion;
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

    public void SetTarget(Transform target)
    {
        movement.State = PlayerMovement.PlayerState.Cutscene;
        cam.Follow = target;
        cam.LookAt = target;
    }
}
