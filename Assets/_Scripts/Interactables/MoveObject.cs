using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveObject : MonoBehaviour
{
    [SerializeField] private float targetY;
    [SerializeField] private float duration = 1f;
    private float timer;
    private Vector3 startPos;
    private Collider trigger;
    private void Start()
    {
        startPos = transform.position;
        trigger = GetComponent<Collider>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        Vector3 endPos = new Vector3(transform.position.x, targetY, transform.position.z);

        transform.position = Vector3.Lerp(startPos , endPos, timer / duration);
        if(Vector3.Distance(transform.position, endPos) < 0.5f)
        {
            trigger.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("End");
            SceneManager.LoadScene("Credits");
        }
    }
}
