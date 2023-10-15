using System.Collections.Generic;
using UnityEngine;

public class TestSaveLoadPlayerPosition : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            print("Saving data...");

            SavableData playerData = new SavableData();
            float[] playerPos = new float[]{ _player.position.x, _player.position.y };
            playerData.SaveData("Position", playerPos);
            playerData.SaveToFile("Player");
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            print("Loading data...");

            SavableData playerData = SaveSystem.LoadData("Player") as SavableData;
            if (playerData == null)
            {
                print("No saved data found!");
                return;
            }
            float[] playerPos = new float[] { 0, 0 };
            try
            {
                playerPos = playerData.GetData("Position") as float[];
            }
            catch (KeyNotFoundException)
            {
                print("Player position has not been saved!");
            }
            finally
            {
                Vector2 newPos = new Vector2(playerPos[0], playerPos[1]);
                _player.position = newPos;
            }
        }
    }
}
