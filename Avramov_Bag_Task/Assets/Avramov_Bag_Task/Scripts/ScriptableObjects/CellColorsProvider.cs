using System;
using System.Collections.Generic;
using UnityEngine;

namespace Avramov.Bag
{
    [CreateAssetMenu(fileName = "CellColorsProvider", menuName = "Avramov_Bag/CellColorsProvider")]
    public class CellColorsProvider : ScriptableObject
    {
        [SerializeField] private Color _defaultColor;
        [SerializeField] private List<CellColorSettings> _settings;

        public Color GetColor(CellStateEnum state) => _settings.Find(x => x.CellState == state)?.Color ?? _defaultColor;

        [Serializable]
        public class CellColorSettings
        {
            [field: SerializeField] public CellStateEnum CellState { get; private set; }
            [field: SerializeField] public Color Color { get; private set; }
        }
    }
}
