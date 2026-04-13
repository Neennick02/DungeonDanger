using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;
using System;

public class TargetFinder : MonoBehaviour
{
    public static List<Transform> pool;
    public static DistanceClass dc;
  

    public List<Transform> poolView;
    public string currentTargetName; 

    public Transform currentTarget { get; private set; }
    
    [SerializeField] private CinemachineCamera playerCamera;

    private bool useFirstCamera;
    private CinemachineCamera currentCamera;
    [SerializeField] private CinemachineCamera targetCamera0;
    [SerializeField] private CinemachineCamera targetCamera1;

    [SerializeField] private Transform _targetPointer;
    private Transform lastTargetPosition;
    
    
    public float maxTargetingDistance = 10f;
    private bool lockedOn;

    public static event Action OnLockOff;
    [SerializeField] private List<AudioClip> targetAudio;

    #region OnEnable/Disable
    private void OnEnable()
      { 
            dc = new DistanceClass(transform);
        
            pool = new List<Transform>();
            poolView = new List<Transform>();


        PlayerInputHandler.OnTarget += TargetAndUntarget;
        PlayerInputHandler.OnTargetRight += SelectTarget;
        PlayerInputHandler.OnTargetLeft += SelectTarget;
        PlayerInputHandler.OnChangeTarget += ChangeTarget;

        PlayerHealth.OnDeath += EmptyList;
        BaseHealth.SwitchTarget += SelectNewTarget;

    }
    private void OnDisable()
    {
        PlayerInputHandler.OnTarget -= TargetAndUntarget;
        PlayerInputHandler.OnTargetRight -= SelectTarget;
        PlayerInputHandler.OnTargetLeft -= SelectTarget;
        PlayerInputHandler.OnChangeTarget -= ChangeTarget;

        PlayerHealth.OnDeath -= EmptyList;
        BaseHealth.SwitchTarget -= SelectNewTarget;
    }
    #endregion

    private void Start()
    {
        lastTargetPosition = _targetPointer;

        currentCamera = targetCamera0;
    }

    private void Update()
    {
        //makes pool visable in editor
        poolView = pool; 
        
        //update target name in inspector
        if(currentTarget != null)
        {
            currentTargetName = currentTarget.name;
        }
        else
        {
            currentTargetName = null;
        }

        //disable target if target is to far away
        if(currentTarget != null)
        {
            float distance = Vector3.Distance(transform.position, currentTarget.position);
            if (distance > maxTargetingDistance)
            {
                //if more than 1 other, select other
                if(pool.Count > 1)
                {
                    AudioManager.Instance.PlayClip(targetAudio);
                    SelectTarget(1);
                }
                else
                //otherwise stop targeting
                {
                    LockOff();
                    OnLockOff?.Invoke();
                }
            }
        }



        UpdatePointerPosition();
    }

    private void UpdatePointerPosition()
    {       
        //only update position when not targeting

        if (NearestTarget() != null && !lockedOn)
        {
            //dont update position if it didnt change
            if (Vector3.Distance(NearestTarget().position, lastTargetPosition.position) < 0.1f) return;

            //update position with offset
            _targetPointer.position = NearestTarget().position + new Vector3(0, 2, 0);
            
            //set position
            lastTargetPosition = _targetPointer; 
        }
        else
        {

            //keep target under player when not in use
            _targetPointer.position = transform.position + new Vector3(0, -50, 0);

        }
    }

    public void TargetAndUntarget()
    {
        if (!lockedOn) //check if already locked on a target
        {
            LockOn();
        }
        else
        {
            LockOff();
        }
    }

    public void SelectNewTarget()
    {
        if (currentTargetName == null) return;

        //disable lockon
        if (pool.Count == 0)
        {
            LockOff();
            OnLockOff?.Invoke();
            return;
        }

        
        //selects a new target when target dies
        Transform newTarget = NearestTarget();

        if(newTarget != null)
        {
            currentTarget = newTarget;
            targetCamera0.LookAt = currentTarget;
            lockedOn = true;
        }
   }

    private void ChangeTarget(Vector2 input)
    {
        if(input.y > 0)
        {
            SelectTarget(1);
        }
        else if(input.y < 0)
        {
            SelectTarget(-1);
        }
    }
    private void SelectTarget(int next)
    {
        if(pool != null && pool.Count > 1)
        {
            System.Predicate<Transform> predicate = FindTransform;
            int currentIndex = pool.FindIndex(predicate);

            int nextIndex = currentIndex + next;

            //loop over list if min/max is reached
            if (nextIndex > pool.Count -1) nextIndex = 0;
            if (nextIndex < 0) nextIndex = pool.Count - 1;

            if (nextIndex >= 0 && nextIndex < pool.Count)
            {
                float distanceToTarget = Vector3.Distance(pool[nextIndex].position, transform.position);

                //only lock on when next target is in range
                if(distanceToTarget < maxTargetingDistance)
                {
                    currentTarget = pool[nextIndex];
                    SwitchTargetCamera();
                }
                else
                {
                    //lockoff when not in range
                    LockOff();
                    OnLockOff?.Invoke();
                }
            }
        }
    }

    private bool FindTransform(Transform t)
    {
        return t.Equals(currentTarget);
    }
 
    private void LockOn()
    {

        currentTarget = NearestTarget();

        if (currentTarget)
        {
            lockedOn = true;

            currentCamera.LookAt = currentTarget;
            currentCamera.gameObject.SetActive(true);

            playerCamera.gameObject.SetActive(false);
        }
    }
    private void LockOff()
    {
        currentTarget = null;

        currentCamera.LookAt = null;
        currentCamera.gameObject.SetActive(false);

        playerCamera.gameObject.SetActive(true);
        lockedOn = false;
    }
    private void SwitchTargetCamera()
    {
        currentCamera.gameObject.SetActive(false);

        // toggle camera
        useFirstCamera = !useFirstCamera;
        currentCamera = useFirstCamera ? targetCamera0 : targetCamera1;

        // assign target
        currentCamera.LookAt = currentTarget;

        // enable new camera
        currentCamera.gameObject.SetActive(true);
    }
    private Transform NearestTarget()
    {
        if(pool != null && pool.Count > 0)
        {
            //returns the target nearest the center of the screen
            Vector3 center = new Vector3(0.5f, 0.5f, 0);
            Camera cam = Camera.main;

            int minIndex = 0;
            float shortestDistance = 1000f;

            for (int i = 0; i < pool.Count; i++)
            {
                if (pool[i] != null) continue;

                Vector3 targetViewport = cam.WorldToViewportPoint(pool[i].position);

                //remove z component
                targetViewport -= Vector3.forward * targetViewport.z;

                float targetDistanceFromCenter = Vector3.Distance(targetViewport, center);

                if (targetDistanceFromCenter < shortestDistance)
                {
                    shortestDistance = targetDistanceFromCenter;
                    minIndex = i;
                }
            }

            //returns the closest target to center
            return poolView[minIndex];
        }
        return null;
    }

    public static void AddToPool(Transform target)
    {
        if (pool == null) return;

        pool.RemoveAll(t => t == null);

        if (!pool.Contains(target))
        {
            pool.Add(target);
            pool.Sort(dc);
        }
    }
    public static void RemoveFromPool(Transform target)
    {
        if (pool != null && pool.Contains(target))
        {
            pool.Remove(target);
            pool.Sort(dc);
        }
    }

    public void EmptyList()
    {
        pool.Clear();
        poolView.Clear();
    }
}

public class DistanceClass : IComparer<Transform>
{
    private Transform player;

    public DistanceClass(Transform playerTransform)
    {
        player = playerTransform;
    }
    public int Compare(Transform x, Transform y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return 1;
        if (y == null) return -1;

        try
        {
            float distance1 = Vector3.Distance(x.position, player.position);
            float distance2 = Vector3.Distance(y.position, player.position);

            return distance1.CompareTo(distance2);
        }
        catch (MissingReferenceException)
        {
            return 0;
        }
    }
} 
