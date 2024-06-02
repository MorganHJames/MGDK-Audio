using UnityEngine;
using System.Collections.Generic;

namespace MGDK.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private Sound[] soundList;
        private static SoundManager instance;
        private static AudioSource audioSource;
        private Dictionary<string, Sound> soundDictionary;

        public static SoundManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<SoundManager>();
                    if (instance == null)
                    {
                        GameObject soundManagerObject = new GameObject("SoundManager");
                        instance = soundManagerObject.AddComponent<SoundManager>();
                    }
                }
                return instance;
            }
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject); // Ensures only one instance exists
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject); // Keeps the SoundManager between scenes

            audioSource = GetComponent<AudioSource>();
            InitializeSoundDictionary();
        }

        private void InitializeSoundDictionary()
        {
            soundDictionary = new Dictionary<string, Sound>();
            foreach (Sound sound in soundList)
            {
                if (!soundDictionary.ContainsKey(sound.soundName))
                {
                    soundDictionary[sound.soundName] = sound;
                }
            }
        }

        public static void PlaySound(string soundName, float volume = 1, float pitch = 1, float volumeVariance = 0, float pitchVariance = 0)
        {
            if (instance == null)
            {
                Debug.LogWarning("SoundManager instance is null. Ensure SoundManager is initialized.");
                return;
            }

            if (instance.soundDictionary.TryGetValue(soundName, out Sound sound))
            {
                SoundClip[] soundClips = sound.soundClips;
                SoundClip randomSoundClip = soundClips[UnityEngine.Random.Range(0, soundClips.Length)];
                audioSource.outputAudioMixerGroup = sound.mixer;

                // Apply volume variance
                float finalVolume = volume + Random.Range(-volumeVariance, volumeVariance);
                finalVolume *= randomSoundClip.volume;
                finalVolume = Mathf.Clamp(finalVolume, 0, 1); // Ensure volume is within range

                // Apply pitch variance
                float finalPitch = pitch + Random.Range(-pitchVariance, pitchVariance);
                finalPitch *= randomSoundClip.pitch;
                finalPitch = Mathf.Clamp(finalPitch, -3, 3); // Ensure pitch is within range

                audioSource.pitch = finalPitch;
                audioSource.PlayOneShot(randomSoundClip.clip, finalVolume);
                audioSource.pitch = 1; // Reset pitch after playing
            }
            else
            {
                Debug.LogWarning($"Sound {soundName} not found in the dictionary.");
            }
        }

        public void PlaySoundAtPosition(string soundName, Vector3 position, float volume = 1)
        {
            if (soundDictionary.TryGetValue(soundName, out Sound sound))
            {
                SoundClip[] soundClips = sound.soundClips;
                SoundClip randomSoundClip = soundClips[UnityEngine.Random.Range(0, soundClips.Length)];
                AudioSource.PlayClipAtPoint(randomSoundClip.clip, position, volume * randomSoundClip.volume);
            }
            else
            {
                Debug.LogWarning($"Sound {soundName} not found in the dictionary.");
            }
        }
    }
}
