using UnityEngine;

namespace AchieveOnePark.AchUtils.RedDot
{
    public class RedDotView : MonoBehaviour
    {
        [SerializeField] private string _path;
        [SerializeField] private GameObject _dotObject;

        private void OnEnable()
        {
            RedDotSystem.Instance.Subscribe(_path, OnChanged);
            Refresh();
        }

        private void OnDisable()
        {
            RedDotSystem.Instance.Unsubscribe(_path, OnChanged);
        }

        private void OnChanged(bool hasRedDot)
        {
            if (_dotObject != null)
                _dotObject.SetActive(hasRedDot);
        }

        public void Refresh()
        {
            OnChanged(RedDotSystem.Instance.Evaluate(_path));
        }
    }
}
