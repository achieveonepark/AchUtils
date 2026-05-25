using System.Collections.Generic;
using UnityEngine;

namespace AchieveOnePark.AchUtils.Tutorial
{
    public class TutorialSystem : MonoBehaviour
    {
        private const string SaveKey = "SS_Tutorial_Completed";

        public static TutorialSystem Instance { get; private set; }

        private readonly HashSet<string> _completedIds = new();
        private TutorialRunner _runner;

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            _runner = GetComponent<TutorialRunner>() ?? gameObject.AddComponent<TutorialRunner>();
            Load();
        }

        public bool IsCompleted(string sequenceId) => _completedIds.Contains(sequenceId);

        public void MarkCompleted(string sequenceId)
        {
            _completedIds.Add(sequenceId);
            Save();
        }

        public void StartTutorial(TutorialSequence sequence)
        {
            if (sequence == null) return;
            if (IsCompleted(sequence.SequenceId)) return;
            _runner.Run(sequence);
        }

        public void StartTutorialForce(TutorialSequence sequence)
        {
            if (sequence == null) return;
            _runner.Run(sequence);
        }

        public void Skip() => _runner.Skip();

        public void TriggerAction(string key) => _runner.TriggerAction(key);

        public TutorialRunner Runner => _runner;

        public void Save()
        {
            PlayerPrefs.SetString(SaveKey, string.Join(",", _completedIds));
            PlayerPrefs.Save();
        }

        public void Load()
        {
            _completedIds.Clear();
            string data = PlayerPrefs.GetString(SaveKey, "");
            if (string.IsNullOrEmpty(data)) return;
            foreach (var id in data.Split(','))
                if (!string.IsNullOrEmpty(id))
                    _completedIds.Add(id);
        }

        public void ResetAll()
        {
            _completedIds.Clear();
            PlayerPrefs.DeleteKey(SaveKey);
        }
    }
}
