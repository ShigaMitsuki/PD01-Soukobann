using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update

    //配列の宣言
    public int[,] Map;
    GameObject[,] Field;

    public GameObject PlayerPrefab;
    public GameObject BoxPrefab;
    public GameObject ClearPointPrefab;

    public GameObject ClearText;

    //出力メソッド
    void PrintArray()
    {

        string DebugText = "";
        DebugText += "\n";
        for (int j = 0; j < Map.GetLength(0); j++)
        {
            for (int i = 0; i < Map.GetLength(1); i++)
            {

                DebugText += Map[j, i].ToString() + ",";
            }

            DebugText += "\n";
        }
        Debug.Log(DebugText);

    }
    //プレイヤーインデックス取得メソッド
    Vector2Int GetPlayerIndex()
    {
        for (int y = 0; y < Map.GetLength(0); y++)
        {
            for (int x = 0; x < Map.GetLength(1); x++)
            {
                if (Field[y, x] != null)
                {
                    if (Field[y, x].tag == "Player")
                    {

                        return new Vector2Int(x, y);
                    }
                }
            }
        }
        return new Vector2Int(-1, -1);
    }


    //NumberをMoveFromからMoveToに動かすメソッド
    bool MoveNumber(string tag, Vector2Int MoveFrom, Vector2Int MoveTo)
    {

        //移動不可の部分
        if (MoveTo.y < 0 || MoveTo.y >= Map.GetLength(0))
        {
            return false;
        }

        if (MoveTo.x < 0 || MoveTo.x >= Map.GetLength(1))
        {
            return false;
        }

        ////移動先に2があったら
        //if (Map[MoveToY, MoveToX] == 2)
        //{

        //    //移動方向を出す
        //    int Velocity = MoveToX - MoveFromX;

        //    //プレイヤーの移動先から、さらに先へ箱を移動させる
        //    //箱の移送処理、MoveNumberメソッド内でMoveNumberメソッドを呼び
        //    //処理が再起している、移動可不可をboolで記録
        //    bool Success = MoveNumber(2, MoveToX,  MoveToY, MoveToX + VelocityX, MoveToY + VelocityY);

        //    //移動失敗の場合、プレイヤーも移動しない
        //    if (!Success) { return false; }

        //}

        if (Field[MoveTo.y, MoveTo.x] != null && Field[MoveTo.y, MoveTo.x].tag == "Box")
        {
            Vector2Int Velocity = MoveTo - MoveFrom;

            bool Success = MoveNumber("Box", MoveTo, MoveTo + Velocity);
            if (!Success) { return false; }
        }

        Field[MoveFrom.y, MoveFrom.x].transform.position = new Vector3(MoveTo.x, Field.GetLength(0) - MoveTo.y, 0);

        Field[MoveTo.y, MoveTo.x] = Field[MoveFrom.y, MoveFrom.x];
        Field[MoveFrom.y, MoveFrom.x] = null;

        // Map[MoveTo.y, MoveTo.x] = Number;
        //Map[MoveFrom.y, MoveFrom.x] = 0;



        Debug.Log(new Vector2Int(MoveFrom.y, MoveFrom.x));
        return true;
    }

    void Start()
    {


        //配列の実態の生成、初期化
        Map = new int[,] {
            { 0, 0, 0, 0, 0, },
            { 0, 3, 1, 3, 0,},
            { 0, 0, 2, 0, 0,},
            { 0, 2, 3, 2, 0,},
            { 0, 0, 0, 0, 0,} };

        //PrintArray();

        Field = new GameObject
            [
                Map.GetLength(0),
                Map.GetLength(1)
            ];

        for (int y = 0; y < Map.GetLength(0); y++)
        {
            for (int x = 0; x < Map.GetLength(1); x++)
            {
                if (Map[y, x] == 1)
                {
                    Field[y, x] = Instantiate(
                        PlayerPrefab,
                        new Vector3(x, Map.GetLength(0) - y, 0),
                        Quaternion.identity
                        );
                }

                if (Map[y, x] == 2)
                {
                    Field[y, x] = Instantiate(
                        BoxPrefab,
                        new Vector3(x, Map.GetLength(0) - y, 0),
                        Quaternion.identity
                        );
                }

                if (Map[y, x] == 3)
                {
                    Field[y, x] = Instantiate(
                        ClearPointPrefab,
                        new Vector3(x, Map.GetLength(0) - y, 0.01f),
                        Quaternion.identity
                        );
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector2Int PlayerIndex = GetPlayerIndex();

            MoveNumber("Player", PlayerIndex, new Vector2Int(PlayerIndex.x + 1, PlayerIndex.y));

            PrintArray();

        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector2Int PlayerIndex = GetPlayerIndex();

            MoveNumber("Player", PlayerIndex, new Vector2Int(PlayerIndex.x - 1, PlayerIndex.y));

            PrintArray();

        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector2Int PlayerIndex = GetPlayerIndex();

            MoveNumber("Player", PlayerIndex, new Vector2Int(PlayerIndex.x, PlayerIndex.y - 1));

            PrintArray();

        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector2Int PlayerIndex = GetPlayerIndex();

            MoveNumber("Player", PlayerIndex, new Vector2Int(PlayerIndex.x, PlayerIndex.y + 1));

            PrintArray();

        }

        if (IsCleard())
        {
            ClearText.SetActive(true);
        }
    }

    bool IsCleard()
    {
        List<Vector2Int> goals = new List<Vector2Int>();

        for (int y = 0; y < Map.GetLength(0); y++)
        {
            for (int x = 0; x < Map.GetLength(1); x++)
            {
                if (Map[y, x] == 3)
                {
                    goals.Add(new Vector2Int(x, y));
                }
            }
        }

        for (int i = 0; i < goals.Count; i++)
        {
            GameObject f = Field[goals[i].y, goals[i].x];
            if (f == null || f.tag != "Box")
            {
                return false;
            }
        }
        Debug.Log("Clear");
        return true;
       
    }
}