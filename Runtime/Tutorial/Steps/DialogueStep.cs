using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace AchieveOnePark.AchUtils.Tutorial
{
    [CreateAssetMenu(menuName = "SS/Tutorial/Steps/Dialogue", fileName = "DialogueStep")]
    public class DialogueStep : TutorialStep
    {
        [TextArea(2, 6)] public string Message;
        public Sprite CharacterSprite;
        public float AutoAdvanceDelay = 0f;

        [Header("UI References (optional overrides)")]
        public GameObject DialoguePanelObject;
        public string MessageTag = "TutorialDialogue";

        public override IEnumerator Execute(TutorialRunner runner)
        {
            var panel = DialoguePanelObject != null
                ? DialoguePanelObject
                : GameObject.FindWithTag(MessageTag);

            if (panel != null)
            {
                panel.SetActive(true);
                var text = panel.GetComponentInChildren<Text>();
                if (text != null) text.text = Message;

                var image = panel.GetComponentInChildren<Image>();
                if (image != null && CharacterSprite != null) image.sprite = CharacterSprite;
            }

            if (AutoAdvanceDelay > 0f)
            {
                yield return new WaitForSeconds(AutoAdvanceDelay);
            }
            else
            {
                yield return new WaitUntil(() => runner.HasTriggeredAction("DialogueNext"));
                runner.ConsumeAction("DialogueNext");
            }

            if (panel != null) panel.SetActive(false);
        }
    }
}
