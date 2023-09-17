using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public const int EMPTY = 0;
    public const int MAXVAL = 2000000000;

    public const int ATK = 1;
    public const int DEF = 2;
    public const int KING = 3;

    int[,] m = new int[MAXN,MAXN];
    int[,] fort = new int[MAXN,MAXN];
    int player;
    int mapSize;
    public int depth;

    struct Move{
        int oi, oj, fi, fj;
        int val;
        int[] kildir;
    };

    // 1 is attacker, 2 is defender
    int[] lindir = {0, 1, 0, -1};
    int[] coldir = {1, 0, -1, 0};

    int getFaction(int x){
        if(x == KING)
            return DEF;
        return x;
    }

    // Sets Kill flags in order to reverse
    Move makeMove(Move mv){
        int oi = mv.oi, oj = mv.oj, fi = mv.fi, fj = mv.fj;

        if(m[oi,oj] == EMPTY){
            printf("Empty tile: %d %d - %d %d\n", oi, oj, fi, fj);
            return Move(0, 0, 0, -1);
        }
        if(oi != fi && oj != fj){
            printf("Invalid move: %d %d - %d %d\n", oi, oj, fi, fj);
            return Move(0, 0, 0, -1);
        }
        if(m[fi,fj] != EMPTY){
            printf("Invalid destination: %d %d - %d %d\n", oi, oj, fi, fj);
            return Move(0, 0, 0, -1);
        }

        m[fi,fj] = m[oi,oj];
        m[oi,oj] = EMPTY;
        
        int f = getFaction(m[fi,fj]);
        for(int dir = 0; dir < 4; dir ++){
            if(fi + 2 * lindir[dir] >= 0 && fi + 2 * lindir[dir] < mapSize && fj + 2 * coldir[dir] >= 0 && fj + 2 * coldir[dir] < mapSize){
            if(getFaction(m[fi + 2 * lindir[dir],fj + 2 * coldir[dir]]) == f && getFaction(m[fi + lindir[dir],fj + coldir[dir]]) == 3 - f){
                mv.kildir[dir] = 1;
                m[fi + lindir[dir],fj + coldir[dir]] = EMPTY;
            }
            }
        }
        
        return mv;
    }

    // Kill Flags have to be set
    int reverseMove(Move mv){
        int oi = mv.oi, oj = mv.oj, fi = mv.fi, fj = mv.fj;

        if(m[fi,fj] == EMPTY){
            printf("Empty tile (reverse): %d %d - %d %d\n", oi, oj, fi, fj);
            return 1;
        }
        if(oi != fi && oj != fj){
            printf("Invalid move (reverse): %d %d - %d %d\n", oi, oj, fi, fj);
            return 1;
        }
        if(m[oi,oj] != EMPTY){
            printf("Invalid destination (reverse): %d %d - %d %d\n", oi, oj, fi, fj);
            return 1;
        }

        for(int dir = 0; dir < 4; dir ++){
            if(mv.kildir[dir])
            m[fi + lindir[dir],fj + coldir[dir]] = 3 - getFaction(m[fi,fj]);
        }
        
        m[oi,oj] = m[fi,fj];
        m[fi,fj] = EMPTY;
        return 0;
    }

    // returns map value for the defender
    int mapValue(){
        int s = 0, i, j, n;
        int ki, kj;
        n = mapSize;

        // Account for the number of pieces
        for(i = 0; i < n; i ++){
            for(j = 0; j < n; j ++){
                if(m[i,j] == DEF)
                    s += 2;
                else if(m[i,j] == ATK)
                    s -= 1;

                else if(m[i,j] == KING){
                    ki = i;
                    kj = j;
                }
            }
        }

        // Add damage for attackers next to the king
        for(int dir = 0; dir < 4; dir ++){
            if(ki + lindir[dir] >= 0 && ki + lindir[dir] < n && kj + coldir[dir] >= 0 && kj + coldir[dir] < n && m[ki + lindir[dir],kj + coldir[dir]] == ATK)
            s --;
        }

        // TODO: Encourage the king to escape

        return s;
    }

    Move calculateMove(int depth, int player){
        /*
        Player 1 is Attacker: wants minimum
        Player 2 is Defender: wants maximum
        */
        if(DEPTH == depth){
            return Move(0, 0, 0, 0, mapValue());
        }

        int i, j, x, n = mapSize;
        Move final;

        if(player == 1)
            final.val = MAXVAL;
        else
            final.val = -MAXVAL;

        for(i = 0; i < n; i ++){
            for(j = 0; j < n; j ++){
                if(getFaction(m[i,j]) == player){

                    for(int dir = 0; dir < 4; dir ++){
                        int lin = i + lindir[dir], col = j + coldir[dir];
                        int okFort = fort[i,j];

                        while(lin >= 0 && lin < n && col >= 0 && col < n && m[lin,col] == EMPTY && (okFort == fort[lin,col] || fort[lin,col] == 0)){
                            if(fort[lin,col] == 0)
                            okFort = 0;

                            Move mv = {i, j, lin, col, 0};
                            mv = makeMove(mv);
                            mv.val = calculateMove(depth + 1, 3 - player).val;
                            reverseMove(mv);

                            if((player == 1 && mv.val < final.val) || (player == 2 && mv.val > final.val)){
                                final = mv;
                            }

                            lin += lindir[dir];
                            col += coldir[dir];
                        }
                    }

                }
            }
        }

        return final;
    }


    void Start(){
        ManageRules rulesScript = this.GameObject.GetComponent<ManageRules>();
        mapSize = rulesScript.mapSize;
        depth = rulesScript.aiDifficulty;
    }

    public void makeMove(){
        m = rulesScript.piecesMap;
        fort = rulesScript.fortsMap;
        player = rulesScript.player;

        calculateMove(depth, player);
    }
}
