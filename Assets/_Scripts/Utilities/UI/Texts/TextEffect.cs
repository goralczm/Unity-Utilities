using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Utilities.Utilities.UI.Texts
{
    public abstract class TextManipulator
    {
        protected TMP_Text _text;
        protected Mesh _mesh;
        protected Vector3[] _vertices;

        protected bool _randomizeVertices;
        protected int[] _wordsAffected;
        protected bool _affectWhole;

        public delegate Vector3 OffsetCalculator(int index);
        private OffsetCalculator _offsetCalculator;

        public TextManipulator(TMP_Text text, OffsetCalculator offsetCalculator)
        {
            _text = text;
            _offsetCalculator = offsetCalculator;
            _wordsAffected = new int[] { };
        }

        protected Vector3 GetOffset(int index) => _offsetCalculator.Invoke(index);

        public void SetOffsetCalculator(OffsetCalculator calculator)
        {
            if (_offsetCalculator == calculator)
                return;

            _offsetCalculator = calculator;
        }

        public void SetWordsAffected(int[] wordsAffected) => _wordsAffected = wordsAffected;

        public void SetAffectWhole(bool affectWhole) => _affectWhole = affectWhole;

        public void SetRandomizeVertices(bool randomizeVertices) => _randomizeVertices = randomizeVertices;

        public void UpdateMesh()
        {
            _text.ForceMeshUpdate();
            _mesh = _text.mesh;
            _vertices = _mesh.vertices;

            ManipulateText();

            _mesh.vertices = _vertices;
            _text.canvasRenderer.SetMesh(_mesh);
        }

        protected abstract void ManipulateText();
    }

    public class TextCharacterManipulation : TextManipulator
    {
        public TextCharacterManipulation(TMP_Text text, OffsetCalculator offsetCalculator) : base(text, offsetCalculator)
        {
        }

        protected override void ManipulateText()
        {
            int wordIndex = 1;

            for (int i = 0; i < _text.textInfo.characterCount; i++)
            {
                TMP_CharacterInfo c = _text.textInfo.characterInfo[i];

                if (!c.isVisible)
                {
                    wordIndex++;
                    continue;
                }

                if (!_affectWhole && !_wordsAffected.Contains(wordIndex))
                    continue;

                int index = c.vertexIndex;
                Vector3 offset = GetOffset(index);

                for (int j = 0; j < 4; j++)
                {
                    if (_randomizeVertices)
                        _vertices[index + j] += GetOffset(index + j);
                    else
                        _vertices[index + j] += offset;
                }
            }
        }
    }

    public enum TextEffectType
    {
        Wobble,
        Jiggle,
        Wave,
    }

    public class TextEffect : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private bool _affectWhole = true;
        [SerializeField] private bool _randomizeVertices;
        [SerializeField, Range(0f, 10f)] private float _effectStrength = 1f;
        [SerializeField] private int[] _wordsAffected;
        [SerializeField] private TextEffectType _effectType;

        [SerializeField] private TextCharacterManipulation _manipulator;

        private void Awake()
        {
            _manipulator = new TextCharacterManipulation(GetComponent<TMP_Text>(), Wobble);
        }

        private void Update()
        {
            switch (_effectType)
            {
                case TextEffectType.Wobble:
                    _manipulator.SetOffsetCalculator(Wobble);
                    break;
                case TextEffectType.Jiggle:
                    _manipulator.SetOffsetCalculator(Jiggle);
                    break;
                case TextEffectType.Wave:
                    _manipulator.SetOffsetCalculator(Wave);
                    break;
            }

            _manipulator.SetAffectWhole(_affectWhole);
            _manipulator.SetWordsAffected(_wordsAffected);
            _manipulator.SetRandomizeVertices(_randomizeVertices);

            _manipulator.UpdateMesh();
        }

        private Vector3 Wobble(int index)
        {
            return new Vector2(Mathf.Sin((Time.time + index) * 1.1f * _effectStrength),
                               Mathf.Cos((Time.time + index) * .8f * _effectStrength));
        }

        private Vector3 Jiggle(int index)
        {
            return UnityEngine.Random.insideUnitCircle * _effectStrength;
        }

        private Vector3 Wave(int index)
        {
            return new Vector2(0, Mathf.Sin((Time.time + index) * 1.1f * _effectStrength));
        }
    }
}
