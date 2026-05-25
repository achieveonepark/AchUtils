using System;
using System.Collections;
using UnityEngine;

namespace AchUtils.Camera
{
    [Serializable]
    public class ZoomCameraAction : CameraAction
    {
        public float TargetFOV = 40f;
        public float Duration = 1f;
        public AnimationCurve Ease = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        public override IEnumerator Execute(CameraDirector director)
        {
            var cam = director.Camera;
            if (cam == null) yield break;

            float startFOV = cam.fieldOfView;
            float elapsed = 0f;

            while (elapsed < Duration)
            {
                elapsed += Time.deltaTime;
                cam.fieldOfView = Mathf.Lerp(startFOV, TargetFOV, Ease.Evaluate(Mathf.Clamp01(elapsed / Duration)));
                yield return null;
            }

            cam.fieldOfView = TargetFOV;
        }
    }
}
