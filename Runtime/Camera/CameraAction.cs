using System;
using System.Collections;
using UnityEngine;

namespace AchieveOnePark.AchUtils.Camera
{
    [Serializable]
    public abstract class CameraAction
    {
        public abstract IEnumerator Execute(CameraDirector director);
    }
}
