using TMPro;
using UnityEngine;
using System.Collections;
public class PushAble : MonoBehaviour
{
    public float moveDistance = 1f;
    public float moveSpeed = 5f;
    private bool isMoving = false;
    private Vector3 targetPosition;
    [SerializeField] private GameObject player;
    public void Push()
    {
        if (isMoving) return;

        Vector3 direction = transform.position - player.transform.position;

        // Ignore vertical difference
        direction.y = 0;
        direction.Normalize();

        // Snap to cardinal direction
        Vector3 snappedDirection = SnapToCardinal(direction);

        targetPosition = transform.position + snappedDirection * moveDistance;

        // Optional: Add collision check here before moving
        if (!CanMoveTo(targetPosition))
        {
            Debug.Log("Blocked!");
            return;
        }

        StartCoroutine(MoveBlock());
    }

    private Vector3 SnapToCardinal(Vector3 dir)
    {
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
            return new Vector3(Mathf.Sign(dir.x), 0, 0);
        else
            return new Vector3(0, 0, Mathf.Sign(dir.z));
    }

    private IEnumerator MoveBlock()
    {
        isMoving = true;

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );

            yield return null;
        }

        transform.position = targetPosition;
        transform.position = new Vector3(
          Mathf.Round(transform.position.x),
          transform.position.y,
            Mathf.Round(transform.position.z)
            );
        isMoving = false;

        PushInteractable pushEvent = GetComponent<PushInteractable>();
        pushEvent.OffInteract?.Invoke();
    }
    private bool CanMoveTo(Vector3 targetPos)
    {
        float checkRadius = 0.45f; // Slightly smaller than block size

        Collider[] hits = Physics.OverlapBox(
            targetPos,
            transform.localScale * 0.45f,
            Quaternion.identity
        );

        foreach (Collider col in hits)
        {
            if (col.gameObject != this.gameObject)
            {
                return false; // Something is blocking it
            }
        }

        return true;
    }
}
