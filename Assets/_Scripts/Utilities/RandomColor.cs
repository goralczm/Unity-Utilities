using UnityEngine;

namespace Utilities.Utilities
{
    /// <summary>
    /// Sets the object <see cref="SpriteRenderer.color"/> to random color.
    /// </summary>
    public class RandomColor : MonoBehaviour
    {
        [SerializeField] private bool _setRandomOnStart;

        private SpriteRenderer _rend;

        private void Start()
        {
            _rend = GetComponent<SpriteRenderer>();

            if (_setRandomOnStart)
                SetRandomColor();
        }

        /// <summary>
        /// Sets the random color.
        /// </summary>
        public void SetRandomColor()
        {
            _rend.color = new Color(Random.Range(0f, 1f),
                                    Random.Range(0f, 1f),
                                    Random.Range(0f, 1f));

        }
    }
}
