using UnityEngine;

namespace Utilities.BuildingSystem
{
    [CreateAssetMenu(menuName = "Buildings/New Building", fileName = "New Building")]
    public class PrefabBuilding : BuildingSO
    {
        public GameObject prefab;
        
        public override Vector2 GetSize()
        {
            return prefab.transform.localScale;
        }

        public override Sprite GetSprite()
        {
            return prefab.GetComponent<SpriteRenderer>().sprite;
        }

        public override void Build(Vector2 position, Quaternion rotation)
        {
            GameObject o = Instantiate(prefab, position, rotation);
            o.name = prefab.name;
        }
    }
}
