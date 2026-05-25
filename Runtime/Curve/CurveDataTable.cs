using System;
using System.Collections.Generic;
using UnityEngine;

namespace AchieveOnePark.AchUtils.Curve
{
    [CreateAssetMenu(menuName = "SS/Curve/Curve Data Table", fileName = "NewCurveTable")]
    public class CurveDataTable : ScriptableObject
    {
        [Serializable]
        public class Entry
        {
            public string Key;
            public CurveDataAsset Curve;
        }

        public List<Entry> Entries = new();

        private Dictionary<string, CurveDataAsset> _lookup;

        private void BuildLookup()
        {
            _lookup = new Dictionary<string, CurveDataAsset>();
            foreach (var e in Entries)
                if (e.Curve != null)
                    _lookup[e.Key] = e.Curve;
        }

        public float Evaluate(string key, float input)
        {
            if (_lookup == null) BuildLookup();
            return _lookup.TryGetValue(key, out var curve)
                ? curve.Evaluate(input)
                : 0f;
        }

        public bool TryGetCurve(string key, out CurveDataAsset curve)
        {
            if (_lookup == null) BuildLookup();
            return _lookup.TryGetValue(key, out curve);
        }

        private void OnValidate() => _lookup = null;
    }
}
