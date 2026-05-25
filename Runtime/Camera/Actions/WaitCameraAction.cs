using System;
using System.Collections;
using UnityEngine;

namespace AchUtils.Camera
{
    [Serializable]
    public class WaitCameraAction : CameraAction
    {
        public float Duration = 1f;

        public override IEnumerator Execute(CameraDirector director)
        {
            yield return new WaitForSeconds(Duration);
        }
    }
}
