using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor
{
    public Item itemToAdd;
    public Item itemToRemove;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Inventory inventory = (Inventory)target;

        EditorGUILayout.LabelField("Debug");

        GUILayout.BeginHorizontal();
        itemToAdd = EditorGUILayout.ObjectField("", itemToAdd, typeof(Item), true) as Item;

        if (GUILayout.Button("Add Item"))
            inventory.AddItem(itemToAdd);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        itemToRemove = EditorGUILayout.ObjectField("", itemToRemove, typeof(Item), true) as Item;

        if (GUILayout.Button("Remove Item"))
            inventory.RemoveItem(itemToRemove);
        GUILayout.EndHorizontal();
    }
}
