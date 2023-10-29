using UnityEngine;
using System.IO;

public class ManageSettings : MonoBehaviour
{
    public GameRoomSettings gameSettings;
    // Start is called before the first frame update
    private void Awake()
    {
        LoadSettings();
    }

    public static GameRoomSettings LoadSettings(){
        string saveName = Application.dataPath + "/settings.txt";
        if(File.Exists(saveName)){
            string saveString = File.ReadAllText(saveName);
            return JsonUtility.FromJson<GameRoomSettings>(saveString);
        }

        return new GameRoomSettings{
            ComputerFaction = 2,
            ComputerDifficulty = 0,
            defenderFirst = true,
            limitedKingMovement = true,
            map = (MapSO)ScriptableObject.CreateInstance("MapSO")
        };
    }

    public static void SaveSettings(GameRoomSettings gameSettings){
        string saveName = Application.dataPath + "/settings.txt";
        string json = JsonUtility.ToJson(gameSettings);
        File.WriteAllText(saveName, json);
    }
}
