using UnityEngine;
using System.IO;

public class ManageSettings : MonoBehaviour
{
    public class GameRoomSettings
    {
        public int ComputerFaction;
        public int ComputerDifficulty;
        public bool defenderFirst;
        public bool limitedKingMovement;
        public MapSO map;
    }

    public GameRoomSettings gameSettings;
    // Start is called before the first frame update
    private void Awake()
    {
        gameSettings = LoadSettings();
        if(gameSettings.map == null){
            ChooseBoard chooseScript = this.gameObject.GetComponent<ChooseBoard>();
            gameSettings.map = chooseScript.maps[chooseScript.id];
        }
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
            map = null 
        };
    }

    public static void SaveSettings(GameRoomSettings gameSettings){
        string saveName = Application.dataPath + "/settings.txt";
        string json = JsonUtility.ToJson(gameSettings);
        File.WriteAllText(saveName, json);
    }
}
