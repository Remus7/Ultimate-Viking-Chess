using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static bool[] ComputerFaction = new bool[2];
    public static int ComputerDifficulty = 1;
    public static bool defenderFirst = true;
    public static bool extendedKingMovement = false;
    public static MapSO map;
}