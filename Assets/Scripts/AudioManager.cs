using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum AudioClips
{
    Button_SFX,
    Reward_SFX,
    MachinePlace_SFX,
    ContractComplete_SFX,
    TimeLimitExceeded_SFX,
    ItemMove_SFX
}

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private int poolSize = 10;
    [SerializeField] private GameObject audioSourcePrefab;
    [SerializeField] private List<AudioClip> audioClips = new List<AudioClip>();
    private Queue<AudioSource> audioPool = new Queue<AudioSource>();

    private Dictionary<AudioClips, AudioClip> audioClipDic;

    protected override void Awake()
    {
        base.Awake();

        audioClipDic = new Dictionary<AudioClips, AudioClip>
        {
            {AudioClips.Button_SFX, audioClips[0] },
            {AudioClips.Reward_SFX, audioClips[1] },
            {AudioClips.MachinePlace_SFX, audioClips[2] },
            {AudioClips.ContractComplete_SFX, audioClips[3] },
            {AudioClips.TimeLimitExceeded_SFX, audioClips[4] },
            {AudioClips.ItemMove_SFX, audioClips[5] },
        };

        InitializePool();
    }

    private void InitializePool()
    {
        for(int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(audioSourcePrefab, transform);
            AudioSource audioSource = obj.GetComponent<AudioSource>();
            obj.SetActive(false);
            audioPool.Enqueue(audioSource);
        }
    }

    private AudioSource GetPooledAudioSource()
    {
        if(audioPool.Count > 0)
        {
            AudioSource source = audioPool.Dequeue();
            source.gameObject.SetActive(true);
            return source;
        }
        else
        {
            GameObject obj = Instantiate(audioSourcePrefab, transform);
            AudioSource audioSource = obj.GetComponent<AudioSource>();
            return audioSource;
        }
    }

    public void PlaySound(AudioClips clip, Vector3 position, float volume = 1f, bool loop = false)
    {
        AudioSource source = GetPooledAudioSource();
        source.transform.position = position;
        AudioClip audioClip = audioClipDic[clip];
        source.clip = audioClip;
        source.volume = volume;
        source.loop = loop;
        source.Play();

        if(!loop)
            StartCoroutine(ReturnToPool(source, audioClip.length));
    }
    public void PlaySound(AudioClips clip, float volume = 1f)
    {
        AudioClip audioClip = audioClipDic[clip];

        if(audioClip == null)
        {
            Debug.LogWarning("PlaySound called with a null AudioClip!");
            return;
        }

        AudioSource audioSource = GetPooledAudioSource();

        if(audioSource == null)
        {
            Debug.LogWarning("No available AudioSource in the pool!");
            return;
        }

        audioSource.clip = null;

        audioSource.gameObject.SetActive(true);
        audioSource.volume = Mathf.Clamp01(volume);
        audioSource.PlayOneShot(audioClip);

        StartCoroutine(ReturnToPool(audioSource, audioClip.length));
    }

    private System.Collections.IEnumerator ReturnToPool(AudioSource source, float delay)
    {
        yield return new WaitForSeconds(delay);
        source.Stop();
        source.gameObject.SetActive(false);
        audioPool.Enqueue(source);
    }
}
