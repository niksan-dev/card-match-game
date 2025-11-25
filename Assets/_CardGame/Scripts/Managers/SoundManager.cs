using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Niksan.CardGame
{
    /// <summary>
    /// Singleton Sound Manager that plays sound effects based on sound type.
    /// </summary>
    public class SoundManager : MonoBehaviour
    {
        // public static SoundManager Instance { get; private set; }

        [Header("Audio Clips")]
        [SerializeField] private List<SoundSFX> audioClips = new List<SoundSFX>();

        private Dictionary<SoundType, AudioSource> sounds = new();

        private void Awake()
        {

            // Populate dictionary
            foreach (var audio in audioClips)
            {
                if (!sounds.ContainsKey(audio.soundType))
                {
                    sounds.Add(audio.soundType, audio.sfx);
                }
            }
        }

        /// <summary>
        /// Plays the sound corresponding to the given sound type.
        /// </summary>
        public void PlaySound(SoundType soundType)
        {
            if (sounds.TryGetValue(soundType, out AudioSource source))
            {
                source.Play();
            }
            else
            {
                Debug.LogWarning($"SoundManager: Sound type '{soundType}' not found.");
            }
        }
    }
}
[Serializable]
public class SoundSFX
{
    public AudioSource sfx;
    public SoundType soundType;
}

public enum SoundType
{
    FLIP = 0,
    MATCH = 1,
    MISMATCH = 2,
    GAME_WIN = 3,
}
