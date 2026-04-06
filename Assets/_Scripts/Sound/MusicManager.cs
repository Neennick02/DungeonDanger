using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource heartBeatSource;
    public void ToggleHeartBeat(bool active)
    {
        heartBeatSource.enabled = active;
    } 
}
