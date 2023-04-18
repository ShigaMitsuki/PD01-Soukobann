using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update

    //配列の宣言
    int[,] Map;

    //出力メソッド
    void PrintArray() {

        string DebugText = "";
        DebugText += "\n";
        for (int j = 0; j < Map.GetLength(0) ; j++)
        {
            for (int i = 0; i < Map.Length / Map.GetLength(0); i++)
            {

                DebugText += Map[j,i].ToString() + ",";
            }

            DebugText +="\n";
        }
        Debug.Log(DebugText);

    }
    //プレイヤーインデックス取得メソッド
    int GetPlayerIndexX()
    {

        for (int j = 0; j < Map.GetLength(0); j++)
        {
            for (int i = 0; i < Map.Length / Map.GetLength(0); i++)
            {

                if (Map[j, i] == 1)
                {
                    return i;
                }
            }
        }

        return -1;
    }

    int GetPlayerIndexY()
    {

        for (int j = 0; j < Map.GetLength(0); j++)
        {
            for (int i = 0; i < Map.Length / Map.GetLength(0); i++)
            {

                if (Map[j, i] == 1)
                {
                    return j;
                }
            }
        }

        return -1;
    }

    //NumberをMoveFromからMoveToに動かすメソッド
    bool MoveNumber(int Number,int MoveFromX, int MoveFromY,int MoveToX, int MoveToY)
    {

        //移動不可の部分
        if (MoveToX < 0 || MoveToX >= Map.Length / Map.GetLength(0))
        {
            return false;
        }

        if (MoveToY < 0 || MoveToY >= Map.GetLength(0))
        {
            return false;
        }

        //移動先に2があったら
        if (Map[MoveToY, MoveToX] == 2)
        {

            //移動方向を出す
            int VelocityX = MoveToX - MoveFromX;
            int VelocityY = MoveToY - MoveFromY;

            //プレイヤーの移動先から、さらに先へ箱を移動させる
            //箱の移送処理、MoveNumberメソッド内でMoveNumberメソッドを呼び
            //処理が再起している、移動可不可をboolで記録
            bool Success = MoveNumber(2, MoveToX,  MoveToY, MoveToX + VelocityX, MoveToY + VelocityY);

            //移動失敗の場合、プレイヤーも移動しない
            if (!Success) { return false; }

        }

        Map[MoveToY, MoveToX] = Number;
        Map[MoveFromY, MoveFromX] = 0;

        return true;
    }

    void Start()
    {
        //配列の実態の生成、初期化
        Map = new int[,] {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 
            { 0, 0, 0, 1, 0, 2, 0, 0, 0 }, 
            { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0 } };

        PrintArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            int PlayerIndexX = GetPlayerIndexX();
            int PlayerIndexY = GetPlayerIndexY();
            //Debug.Log();

            MoveNumber(1, PlayerIndexX,PlayerIndexY, PlayerIndexX + 1, PlayerIndexY );

            PrintArray();

        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int PlayerIndexX = GetPlayerIndexX();
            int PlayerIndexY = GetPlayerIndexY();

            MoveNumber(1, PlayerIndexX, PlayerIndexY, PlayerIndexX - 1,PlayerIndexY );

            PrintArray();

        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            int PlayerIndexX = GetPlayerIndexX();
            int PlayerIndexY = GetPlayerIndexY();

            MoveNumber(1, PlayerIndexX, PlayerIndexY, PlayerIndexX ,PlayerIndexY- 1 );

            PrintArray();

        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            int PlayerIndexX = GetPlayerIndexX();
            int PlayerIndexY = GetPlayerIndexY();

            MoveNumber(1, PlayerIndexX, PlayerIndexY, PlayerIndexX,PlayerIndexY + 1);

            PrintArray();

        }
    }
}