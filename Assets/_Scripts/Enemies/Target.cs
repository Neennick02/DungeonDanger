using UnityEngine;
using UnityEngine.VFX;

public class Target : MonoBehaviour
{
    private Collider m_collider;
    private Camera cam;
    private Plane[] frustrum;

    private bool visible;
    private bool inPool;

    private void Start()
    {
        m_collider = GetComponent<Collider>();
        cam = Camera.main;
    }

    private void Update()
    {  
        frustrum = GeometryUtility.CalculateFrustumPlanes(cam);

        visible = GeometryUtility.TestPlanesAABB(frustrum, m_collider.bounds);

        if (visible)
        {
            //add ourself to target pool
            TargetFinder.AddToPool(this.transform);
            inPool = true;
        }
        else
        {
            //only remove if not visible
            if (!visible)
            {
                //remove ourself from target pool
                TargetFinder.RemoveFromPool(this.transform);
                inPool = false;
            }
        }
    }
}
