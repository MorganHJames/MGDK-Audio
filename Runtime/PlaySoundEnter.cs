using UnityEngine;

namespace MGDK.Audio
{
    public class PlaySoundEnter : StateMachineBehaviour
    {
        [SerializeField] private Sound sound;
        [SerializeField, Range(0, 1)] private float volume = 1;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            SoundManager.PlaySound(sound.soundName, volume);
        }
    }
}