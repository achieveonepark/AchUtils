using AchieveOnePark.AchUtils.ActionReplay;
using AchieveOnePark.AchUtils.Buff;
using AchieveOnePark.AchUtils.Camera;
using AchieveOnePark.AchUtils.Curve;
using AchieveOnePark.AchUtils.Formula;
using AchieveOnePark.AchUtils.Quest;
using AchieveOnePark.AchUtils.Sequence;
using AchieveOnePark.AchUtils.Spawn;
using AchieveOnePark.AchUtils.StatModifier;
using AchieveOnePark.AchUtils.TimeRewind;
using AchieveOnePark.AchUtils.Tutorial;
using UnityEngine;
using StatModifierModel = AchieveOnePark.AchUtils.StatModifier.StatModifier;

namespace AchieveOnePark.AchUtils.Samples
{
    public sealed class AchUtilsSampleController : MonoBehaviour, IDamageable, IStunnable
    {
        [Header("Required Scene Components")]
        [SerializeField] private TutorialRunner tutorialRunner;
        [SerializeField] private Spawner[] spawners;
        [SerializeField] private CameraDirector cameraDirector;
        [SerializeField] private RewindableObject[] rewindableObjects;
        [SerializeField] private SequenceRunner sequenceRunner;

        [Header("Assets")]
        [SerializeField] private TutorialSequence tutorialSequence;
        [SerializeField] private BuffDefinition buffDefinition;
        [SerializeField] private QuestDefinition questDefinition;
        [SerializeField] private SequenceAsset sequenceAsset;
        [SerializeField] private FormulaAsset formulaAsset;
        [SerializeField] private CurveDataAsset curveDataAsset;

        [Header("Targets")]
        [SerializeField] private GameObject buffTarget;
        [SerializeField] private string tutorialActionKey = "SampleAction";
        [SerializeField] private string questEnemyType = "Slime";
        [SerializeField] private float replaySpeed = 1f;

        private TutorialSystem _tutorialSystem;
        private readonly BuffSystem _buffSystem = new();
        private readonly QuestSystem _questSystem = new();
        private readonly ActionRecorder _actionRecorder = new();
        private readonly ActionPlayer _actionPlayer = new();
        private readonly StatSheet _stats = new();
        private readonly FormulaContext _formulaContext = new();
        private SpawnSystem _spawnSystem;
        private TimeRewindSystem _timeRewindSystem;
        private ReplayData _lastReplay;
        private int _stunCount;

        private void Awake()
        {
            _tutorialSystem = tutorialRunner != null ? new TutorialSystem(tutorialRunner) : null;
            _spawnSystem = new SpawnSystem(spawners);
            _timeRewindSystem = new TimeRewindSystem();

            if (rewindableObjects == null) return;

            foreach (var rewindable in rewindableObjects)
                rewindable?.Bind(_timeRewindSystem);
        }

        private void Update()
        {
            _buffSystem.Tick(Time.deltaTime);
            _timeRewindSystem.Tick(Time.deltaTime);
            _actionPlayer.Tick(Time.deltaTime);
        }

        private void OnDestroy()
        {
            _tutorialSystem?.Dispose();
        }

        public void PlayTutorial()
        {
            _tutorialSystem?.StartTutorial(tutorialSequence);
        }

        public void TriggerTutorialAction()
        {
            _tutorialSystem?.TriggerAction(tutorialActionKey);
        }

        public void ApplyBuff()
        {
            var target = buffTarget != null ? buffTarget : gameObject;
            if (buffDefinition != null)
                _buffSystem.Apply(buffDefinition, target);
        }

        public void StartQuest()
        {
            _questSystem.StartQuest(questDefinition);
        }

        public void ProgressQuestKill()
        {
            _questSystem.Progress("Kill", questEnemyType);
        }

        public void StartSpawners()
        {
            _spawnSystem.StartAll();
        }

        public void StopSpawners()
        {
            _spawnSystem.StopAll();
        }

        public void PlaySequence()
        {
            sequenceRunner?.Run(sequenceAsset);
        }

        public void PlayCameraShake()
        {
            cameraDirector?.Play(new ShakeCameraAction
            {
                Duration = 0.35f,
                Intensity = 0.2f,
                Frequency = 25f
            });
        }

        public void StartRecording()
        {
            _actionRecorder.StartRecording();
        }

        public void RecordJump()
        {
            _actionRecorder.RecordButton("Jump", true);
        }

        public void StopRecording()
        {
            _lastReplay = _actionRecorder.StopRecording();
        }

        public void PlayLastRecording()
        {
            _actionPlayer.Play(_lastReplay, replaySpeed);
        }

        public void RewindThreeSeconds()
        {
            _timeRewindSystem.StartRewind();
            _timeRewindSystem.RewindTo(3f);
            _timeRewindSystem.StopRewind();
        }

        public float EvaluateFormula()
        {
            _stats.SetBase("Attack", 100f);
            _stats.SetBase("Defense", 25f);
            _stats.RemoveModifier("Attack", "sample_weapon");
            _stats.AddModifier("Attack", new StatModifierModel("sample_weapon", ModifierType.Flat, 20f));

            _formulaContext.Clear();
            _formulaContext.Set("Attack", _stats.GetFinal("Attack"));
            _formulaContext.Set("Defense", _stats.GetFinal("Defense"));
            _formulaContext.Set("SkillMultiplier", 1.5f);

            return formulaAsset != null
                ? formulaAsset.Evaluate(_formulaContext)
                : 0f;
        }

        public float EvaluateCurve(float input)
        {
            return curveDataAsset != null ? curveDataAsset.Evaluate(input) : 0f;
        }

        public void TakeDamage(float amount)
        {
            Debug.Log($"Sample target took {amount} damage.");
        }

        public void ApplyStun()
        {
            _stunCount++;
            Debug.Log($"Sample target stunned. Count: {_stunCount}");
        }

        public void RemoveStun()
        {
            _stunCount = Mathf.Max(0, _stunCount - 1);
            Debug.Log($"Sample target stun removed. Count: {_stunCount}");
        }
    }
}
