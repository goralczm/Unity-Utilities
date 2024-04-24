using UnityEngine;
using Utilities.BuildingSystem;

namespace Utilities.LevelEditor
{
    public class TileBuldingGhost : MonoBehaviour, IBuildingPreview
    {
        private SpriteRenderer _rend;
        private IBuildable _buildable;

        private void Awake()
        {
            _rend = GetComponent<SpriteRenderer>();
        }

        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        public void Setup(IBuildable building)
        {
            _buildable = building;
            transform.localScale = building.GetSize();
            _rend.sprite = _buildable.GetSprite();
        }

        public void Build()
        {
            _buildable.Build(transform.position, transform.rotation);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
