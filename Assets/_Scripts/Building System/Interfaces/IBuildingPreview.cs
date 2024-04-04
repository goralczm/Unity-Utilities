using UnityEngine;

namespace Utilities.BuildingSystem
{
    public interface IBuildingPreview
    {
        public void SetPosition(Vector2 position);
        public void Setup(IBuildable building);
        public void Build();
        public void Show();
        public void Hide();
    }
}