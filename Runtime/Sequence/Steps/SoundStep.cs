using System;
using System.Collections;
using UnityEngine;

namespace AchUtils.Sequence
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

            var position = runner != null ? runner.transform.position : Vector3.zero;
            AudioSource.PlayClipAtPoint(Clip, position, Volume);

            if (WaitUntilFinished)
                yield return new WaitForSeconds(Clip.length);
        }
    }
}
