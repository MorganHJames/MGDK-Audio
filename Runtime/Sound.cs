using UnityEngine;
using UnityEngine.Audio;

namespace MGDK.Audio
{
    [System.Serializable]
    public struct SoundClip
    {
        [HideInInspector] public string clipName;
        public AudioClip clip;
        [Range(0, 1)] public float volume;
        [Range(-3, 3)] public float pitch;
    }

    [CreateAssetMenu(fileName = "NewSound", menuName = "Audio/Sound")]
    public class Sound : ScriptableObject
    {
        [HideInInspector]
        public string soundName;
        public AudioMixerGroup mixer;
        public SoundClip[] soundClips;
    }
}