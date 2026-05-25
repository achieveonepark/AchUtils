using System;
using System.Collections;
using UnityEngine;

namespace AchieveOnePark.AchUtils.Camera
{
    [Serializable]
    public class MoveCameraAction : CameraAction
    {
        public Vector3 Destination;
        public Transform FollowTarget;
        public float Duration = 1f;
        public AnimationCurve Ease = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        public override IEnumerator Execute(CameraDirector director)
        {
            var cam = director.Camera;
            if (cam == null) yield break;

            Vector3 start = cam.transform.position;
            Vector3 end = FollowTarget != null ? FollowTarget.position : Destination;
            float elapsed = 0f;

            while (elapsed < Duration)
            {
                elapsed += Time.deltaTime;
                Vector3 target = FollowTarget != null ? FollowTarget.position : end;
                cam.transform.position = Vector3.Lerp(start, target, Ease.Evaluate(Mathf.Clamp01(elapsed / Duration)));
                yield return null;
            }

            cam.transform.position = FollowTarget != null ? FollowTarget.position : end;
        }
    }
}
