using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

[CustomEditor(typeof(ChestBehaviour))]
public class ChestEditor : Editor
{

    public string[] rarities = { "Common", "Rare", "Very Rare", "Epic" };
    public string[] allItemsNames = { "Steel Axe", "Carrot", "Ancient Rune" };

    int selected = 0;

    /*public override void OnInspectorGUI()
    {

        ChestBehaviour cb = (ChestBehaviour)target;

        selected = EditorGUILayout.Popup("Rarity", selected, rarities);
        cb.rarity = rarities[selected];

        arrayLength = EditorGUILayout.IntField("Size", arrayLength);

        cb.ClearInventory(arrayLength + 1);

        for (int i = 0; i < arrayLength; i++)
        {


            string label = "Item " + i.ToString();
            EditorGUILayout.ObjectField(label, null, typeof(InventoryItem));
            //cb.inventory[i] = ItemManager.allItems[selectedItems[i]];
        }

        base.OnInspectorGUI();
    }*/
}
