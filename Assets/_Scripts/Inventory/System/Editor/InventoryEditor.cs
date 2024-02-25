using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor
{
    public Item itemToAdd;
    public Item itemToRemove;

    public int amountToAdd;
    public int amountToRemove;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Inventory inventory = (Inventory)target;

        EditorGUILayout.LabelField("Debug");

        GUILayout.BeginHorizontal();
        itemToAdd = EditorGUILayout.ObjectField("", itemToAdd, typeof(Item), true) as Item;
        amountToAdd = EditorGUILayout.IntField("", amountToAdd);
        GUILayout.EndHorizontal();
        if (GUILayout.Button("Add Item"))
            inventory.AddItem(itemToAdd, amountToAdd);

        GUILayout.BeginHorizontal();
        itemToRemove = EditorGUILayout.ObjectField("", itemToRemove, typeof(Item), true) as Item;
        amountToRemove = EditorGUILayout.IntField("", amountToRemove);
        GUILayout.EndHorizontal();
        if (GUILayout.Button("Remove Item"))
            inventory.RemoveItem(itemToRemove, amountToRemove);
    }
}
