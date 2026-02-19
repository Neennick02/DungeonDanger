using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class TargetFinder : MonoBehaviour
{
    public static List<Transform> pool;
    public static DistanceClass dc;
  

    public List<Transform> poolView;
    public string currentTargetName; 

    private Transform currentTarget;
    
    [SerializeField] private CinemachineCamera playerCamera;
    [SerializeField] private CinemachineCamera targetCamera;


    private bool lockedOn;

    #region OnEnable/Disable
    private void OnEnable()
    {
        PlayerInput.OnTarget += TargetAndUntarget;
        PlayerInput.OnTargetRight += SelectTarget;
        PlayerInput.OnTargetLeft += SelectTarget;
    }
    private void OnDisable()
    {
        PlayerInput.OnTarget -= TargetAndUntarget;
        PlayerInput.OnTargetRight -= SelectTarget;
        PlayerInput.OnTargetLeft -= SelectTarget;
    }
    #endregion

    private void Start()
    {
        if (pool == null)
        {
            pool = new List<Transform>();
        }

        if(dc == null)
        {
            dc = new DistanceClass();
        }
    }
    private void Update()
    {
        //makes pool visable in editor
        poolView = pool; 
        
        if(currentTarget != null)
        {
            currentTargetName = currentTarget.name;
        }
        else
        {
            currentTargetName = null;
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

    private void SelectTarget(int next)
    {
        if(pool != null && pool.Count > 1)
        {
            System.Predicate<Transform> predicate = FindTransform;
            int currentIndex = pool.FindIndex(predicate);

            int nextIndex = currentIndex + next;

            if (nextIndex > pool.Count -1) nextIndex = 0;
            if (nextIndex < 0) nextIndex = pool.Count - 1;

            if (nextIndex >= 0 && nextIndex < pool.Count)
            {
                currentTarget = pool[nextIndex];
                targetCamera.LookAt = currentTarget;
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

            targetCamera.LookAt = currentTarget;
            targetCamera.gameObject.SetActive(true);

            playerCamera.gameObject.SetActive(false);
        }
    }
    private void LockOff()
    {
        currentTarget = null;

        targetCamera.LookAt = null;
        targetCamera.gameObject.SetActive(false);

        playerCamera.gameObject.SetActive(true);
        lockedOn = false;
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
        if(pool != null && !pool.Contains(target))
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
}

public class DistanceClass : IComparer<Transform>
{
    public int Compare(Transform x, Transform y)
    {
        Camera camera = Camera.main;

        Vector3 xViewport = camera.WorldToViewportPoint(x.position);
        Vector3 yViewport = camera.WorldToViewportPoint(y.position);

        if(xViewport.x < yViewport.x)
        {
            return -1;
        }
        else if(xViewport.x == yViewport.x)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }
} 
