using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageRules : MonoBehaviour
{
    public MapSO map;
    public GameObject cameraObj;

    GenerateMap mapScript;
    GeneratePieces piecesScript;
    AIManager aiScript;

    [HideInInspector]
    public int mapSize;
    [HideInInspector]
    public int[,] piecesMap;
    [HideInInspector]
    public int[,] fortsMap;
    [HideInInspector]
    public int[,] pieceRotationMap;

    public int player; // 1 is defender, 0 is attacker
    [HideInInspector]
    public int aiDifficulty;
    public bool[] aiPlayer = {false, false};

    public float aiCooldown;

    // Start is called before the first frame update
    void Start()
    {
        aiDifficulty = GameSettings.ComputerDifficulty;
        // map = GameSettings.map;
        if(GameSettings.defenderFirst == true)
            player = 1;
        else
            player = 0;

        int computerFaction = GameSettings.ComputerFaction;
        if (computerFaction == 2)
            computerFaction = Random.Range(0, 2);
        aiPlayer = new bool[2] {computerFaction == 0, computerFaction == 1};

        mapScript = this.gameObject.GetComponent<GenerateMap>();
        piecesScript = this.gameObject.GetComponent<GeneratePieces>();
        aiScript = this.gameObject.GetComponent<AIManager>();

        int n = map.mapSize;
        mapSize = n;
        mapScript.mapSize = n;

        piecesScript.cameraObj = cameraObj;

        piecesMap = new int[n, n];
        fortsMap = new int[n, n];
        pieceRotationMap = new int[n, n];
        for(int i = 0; i < n; i ++){
            for(int j = 0; j < n; j ++){
                piecesMap[i, j] = map.piecesLayout.rows[i].row[j];
                fortsMap[i, j] = map.fortsLayout.rows[i].row[j];
                pieceRotationMap[i, j] = map.pieceRotationLayout.rows[i].row[j];
            }
        }

        Invoke("checkAi", aiCooldown);
    }

    public void makeMove(){
        player = 1 - player;
        Invoke("checkAi", aiCooldown);
    }

    void checkAi(){
        if(aiPlayer[player] == true){
            executeAiMove();
        }
    }

    void executeAiMove(){
        AIManager.Move nextMove = aiScript.makeMove();
        Debug.Log(nextMove.oi.ToString() + " " + nextMove.oj.ToString() + " " + nextMove.fi.ToString() + " " + nextMove.fj.ToString());

        GameObject startTile = mapScript.tiles[nextMove.oi, nextMove.oj];
        GameObject targetTile = mapScript.tiles[nextMove.fi, nextMove.fj];
        GameObject piece = startTile.GetComponent<TileManager>().piece;
        if(piece == null)
            Debug.Log("Piece not found at " + nextMove.oi.ToString() + ", " + nextMove.oj.ToString());
        
        mapScript.board.GetComponent<SelectTiles>().executeTableMove(startTile, targetTile);
    }

    int[] lindir = {0, 1, 0, -1};
    int[] coldir= {1, 0, -1, 0};
    int getFaction(int x){
        if(x == 3)
            return 2;
        return x;
    }
    public void checkDeath(int lin, int col){
        for(int dir = 0; dir < 4; dir ++){
            int nlin = lin + 2 * lindir[dir];
            int ncol = col + 2 * coldir[dir];

            int mlin = lin + lindir[dir];
            int mcol = col + coldir[dir];

            int f = getFaction(piecesMap[lin, col]);

            if(nlin >= 0 && nlin < mapSize && ncol >= 0 && ncol < mapSize && f == getFaction(piecesMap[nlin, ncol]) && getFaction(piecesMap[mlin, mcol]) == 3 - f && piecesMap[mlin, mcol] != 3){
                mapScript.tiles[mlin, mcol].GetComponent<TileManager>().piece.GetComponent<PieceDie>().killPiece();
            }
        }
    }
}
