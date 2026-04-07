using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource heartBeatSource;
    [SerializeField] private AudioSource defaultMusicSource;
    [SerializeField] private AudioSource combatMusicSource;
    private float fadeOutDuration = 1f;
    private float timer;

    public void ToggleHeartBeat(bool active)
    {
        heartBeatSource.enabled = active;
    }

    public void ToggleCombatMusic(bool active)
    {
        if (active)
        {
            combatMusicSource.enabled = true;
            combatMusicSource.time = 0;
            StartCoroutine(FadeMusic(defaultMusicSource, combatMusicSource));
        }
        else
        {
            StartCoroutine(FadeMusic(combatMusicSource, defaultMusicSource));
        }
    }

    private IEnumerator FadeMusic(AudioSource oldAudio, AudioSource newAudio)
    {
        timer = 0;


        while(timer < fadeOutDuration)
        {
            timer += Time.deltaTime;

            oldAudio.volume = Mathf.Lerp(1, 0, timer / fadeOutDuration);
            newAudio.volume = Mathf.Lerp(0, 1, timer / fadeOutDuration);

            yield return null;
        }
    }
}
