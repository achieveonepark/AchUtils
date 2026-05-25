using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace AchUtils.Tutorial
{
    [CreateAssetMenu(menuName = "AchUtils/Tutorial/Steps/Highlight Button", fileName = "HighlightButtonStep")]
    public class HighlightButtonStep : TutorialStep
    {
        public GameObject TargetObject;
        public Color HighlightColor = new Color(1f, 1f, 0f, 0.4f);
        public string WaitForActionKey;

        public override IEnumerator Execute(TutorialRunner runner)
        {
            if (TargetObject == null) yield break;

            var image = TargetObject.GetComponent<Image>();
            Color originalColor = Color.white;
            if (image != null)
            {
                originalColor = image.color;
                image.color = HighlightColor;
            }

            TargetObject.SetActive(true);

            if (!string.IsNullOrEmpty(WaitForActionKey))
            {
                while (!runner.HasTriggeredAction(WaitForActionKey))
                    yield return null;
                runner.ConsumeAction(WaitForActionKey);
            }

            if (image != null) image.color = originalColor;
        }

        public override void OnSkip(TutorialRunner runner)
        {
            if (TargetObject == null) return;
            var image = TargetObject.GetComponent<Image>();
            if (image != null) image.color = Color.white;
        }
    }
}
