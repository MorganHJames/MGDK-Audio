using UnityEditor;
using UnityEngine;

namespace MGDK.Audio
{
    [CustomEditor(typeof(Sound))]
    public class SoundEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Sound sound = (Sound)target;

            // Ensure the sound name matches the file name
            string assetPath = AssetDatabase.GetAssetPath(sound);
            string fileName = System.IO.Path.GetFileNameWithoutExtension(assetPath);

            if (sound.soundName != fileName)
            {
                sound.soundName = fileName;
                EditorUtility.SetDirty(sound);
            }

            // Ensure each sound clip has the correct clip name and default volume
            for (int i = 0; i < sound.soundClips.Length; i++)
            {
                if (sound.soundClips[i].clip != null)
                {
                    sound.soundClips[i].clipName = sound.soundClips[i].clip.name;
                    if (sound.soundClips[i].volume == 0)
                    {
                        sound.soundClips[i].volume = 1f;
                    }
                    if (sound.soundClips[i].pitch == 0)
                    {
                        sound.soundClips[i].pitch = 1f;
                    }
                }
            }

            // Apply changes
            if (GUI.changed)
            {
                EditorUtility.SetDirty(sound);
            }
        }
    }
}