using System;
using System.Collections;
using UnityEngine;

namespace AchieveOnePark.AchUtils.Sequence
{
    [Serializable]
    public class SoundStep : SequenceStep
    {
        public AudioClip Clip;
        [Range(0f, 1f)] public float Volume = 1f;
        public bool WaitUntilFinished = false;

        public override IEnumerator Execute(SequenceRunner runner)
        {
            if (Clip == null) yield break;

            AudioSource.PlayClipAtPoint(Clip, Camera.main != null ? Camera.main.transform.position : Vector3.zero, Volume);

            if (WaitUntilFinished)
                yield return new WaitForSeconds(Clip.length);
        }
    }
}
