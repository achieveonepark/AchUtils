using System;
using System.Collections.Generic;
using UnityEngine;

namespace AchUtils.Tutorial
{
    public class TutorialSystem : IDisposable
    {
        private const string SaveKey = "AchUtils_Tutorial_Completed";
        private const string LegacySaveKey = "SS_Tutorial_Completed";

        private readonly HashSet<string> _completedIds = new();
        private readonly TutorialRunner _runner;

        public TutorialSystem(TutorialRunner runner, bool loadOnCreate = true)
        {
            _runner = runner ?? throw new ArgumentNullException(nameof(runner));
            _runner.OnCompleted += HandleCompleted;
            if (loadOnCreate)
                Load();
        }

        public bool IsCompleted(string sequenceId) => _completedIds.Contains(sequenceId);

        public void MarkCompleted(string sequenceId)
        {
            if (string.IsNullOrEmpty(sequenceId)) return;
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

        private void HandleCompleted()
        {
            var sequence = _runner.CurrentSequence;
            if (sequence != null && sequence.SaveProgress)
                MarkCompleted(sequence.SequenceId);
        }

        public void Save()
        {
            PlayerPrefs.SetString(SaveKey, string.Join(",", _completedIds));
            PlayerPrefs.Save();
        }

        public void Load()
        {
            _completedIds.Clear();
            string data = PlayerPrefs.GetString(SaveKey, "");
            if (string.IsNullOrEmpty(data) && PlayerPrefs.HasKey(LegacySaveKey))
            {
                data = PlayerPrefs.GetString(LegacySaveKey, "");
                PlayerPrefs.SetString(SaveKey, data);
                PlayerPrefs.DeleteKey(LegacySaveKey);
                PlayerPrefs.Save();
            }

            if (string.IsNullOrEmpty(data)) return;
            foreach (var id in data.Split(','))
                if (!string.IsNullOrEmpty(id))
                    _completedIds.Add(id);
        }

        public void ResetAll()
        {
            _completedIds.Clear();
            PlayerPrefs.DeleteKey(SaveKey);
            PlayerPrefs.DeleteKey(LegacySaveKey);
        }

        public void Dispose()
        {
            _runner.OnCompleted -= HandleCompleted;
        }
    }
}
