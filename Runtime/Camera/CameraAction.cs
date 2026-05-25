using System;
using System.Collections;
using UnityEngine;

namespace AchUtils.Camera
{
    [Serializable]
    public abstract class CameraAction
    {
        public abstract IEnumerator Execute(CameraDirector director);
    }
}
