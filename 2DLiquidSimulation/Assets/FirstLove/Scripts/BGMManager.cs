using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [Header("BGM Settings")]
    public AudioClip bgmClip;
    [Range(0f, 1f)]
    public float volume = 0.5f;
    public bool playOnStart = true;

    private AudioSource audioSource;
    public static BGMManager Instance;
    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.volume = volume;

        if (bgmClip != null)
        {
            audioSource.clip = bgmClip;

            if (playOnStart)
                audioSource.Play();
        }
    }
    public void PlayBGM(AudioClip newClip)
    {
        if (audioSource.clip == newClip) return;

        audioSource.clip = newClip;
        audioSource.Play();
    }
    public void StopBGM()
    {
        audioSource.Stop();
    }

    public void SetVolume(float newVolume)
    {
        volume = newVolume;
        audioSource.volume = volume;
    }
}
