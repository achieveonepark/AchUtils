using System;
using System.Collections;
using UnityEngine;

namespace AchUtils.Camera
{
    [Serializable]
    public class ShakeCameraAction : CameraAction
    {
        public float Duration = 1f;
        public float Intensity = 0.3f;
        public float Frequency = 25f;
        public AnimationCurve AttenuationCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);

        public override IEnumerator Execute(CameraDirector director)
        {
            var cam = director.Camera;
            if (cam == null) yield break;

            Vector3 originalPos = cam.transform.localPosition;
            float elapsed = 0f;

            while (elapsed < Duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / Duration;
                float strength = AttenuationCurve.Evaluate(t) * Intensity;
                float angle = elapsed * Frequency * Mathf.PI * 2f;
                cam.transform.localPosition = originalPos + new Vector3(
                    Mathf.Sin(angle) * strength,
                    Mathf.Cos(angle * 0.7f) * strength,
                    0f);
                yield return null;
            }

            cam.transform.localPosition = originalPos;
        }
    }
}
