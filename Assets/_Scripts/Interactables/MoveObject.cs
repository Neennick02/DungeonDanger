using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] private float targetY;
    [SerializeField] private float duration = 1f;
    private float timer;
    
    private void Update()
    {
        timer += Time.deltaTime;

        
        transform.position = Vector3.Lerp(transform.position,new Vector3(transform.position.x, targetY, transform.position.z), timer / duration);
        
    }
}
