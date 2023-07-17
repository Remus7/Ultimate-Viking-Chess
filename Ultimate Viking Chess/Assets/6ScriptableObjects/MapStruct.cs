using UnityEngine;

[System.Serializable]
public struct rowData{
    public int[] row;
}


[System.Serializable]
public class MapStruct 
{
    public rowData[] rows;
}
