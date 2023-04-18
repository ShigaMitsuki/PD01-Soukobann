using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update

    //�z��̐錾
    int[,] Map;

    //�o�̓��\�b�h
    void PrintArray() {

        string DebugText = "";
        DebugText += "\n";
        for (int j = 0; j < Map.GetLength(0) ; j++)
        {
            for (int i = 0; i < Map.Length ; i++)
            {

                DebugText += Map[j,i].ToString() + ",";
            }

            DebugText +="\n";
        }
        Debug.Log(DebugText);

    }
    //�v���C���[�C���f�b�N�X�擾���\�b�h
    int GetPlayerIndex()
    {
        for (int j = 0; j < Map.GetLength(0); j++)
        {
            for (int i = 0; i < Map.GetLength(j) ; i++)
            {

                if (Map[j, i] == 1)
                {
                    return i;
                }
            }
        }
        return -1;
    }

    //Number��MoveFrom����MoveTo�ɓ��������\�b�h
    bool MoveNumber(int Number,int MoveFromX, int MoveFromY,int MoveToX, int MoveToY)
    {

        ////�ړ��s�̕���
        //if (MoveToX < 0 || MoveToX >= Map.Length)
        //{
        //    return false;
        //}

        //if (MoveToY < 0 || MoveToY >= Map.Rank)
        //{
        //    return false;
        //}

        ////�ړ����2����������
        //if (Map[MoveToY,MoveToX] == 2)
        //{

        //    //�ړ��������o��
        //    int VelocityX = MoveToX - MoveFromX;
        //    int VelocityY = MoveToY - MoveFromY;

        //    //�v���C���[�̈ړ��悩��A����ɐ�֔����ړ�������
        //    //���̈ڑ������AMoveNumber���\�b�h����MoveNumber���\�b�h���Ă�
        //    //�������ċN���Ă���A�ړ��s��bool�ŋL�^
        //    bool Success = MoveNumber(2, MoveToX, MoveToY, MoveToX + VelocityX, MoveToY + VelocityY);

        //    //�ړ����s�̏ꍇ�A�v���C���[���ړ����Ȃ�
        //    if (!Success) { return false; }

        //}

        //Map[MoveToY, MoveToX] = Number;
        //Map[MoveFromY, MoveFromX] = 0;

        return true;
    }

    void Start()
    {
        //�z��̎��Ԃ̐����A������
        Map = new int[,] { { 0, 0, 0, 1, 0, 2, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0 }};

        PrintArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //int PlayerIndex = GetPlayerIndex();
            Debug.Log();

            //MoveNumber(1, PlayerIndex, PlayerIndex + 1);

            PrintArray();

        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //int PlayerIndex = GetPlayerIndex();

            //MoveNumber(1, PlayerIndex, PlayerIndex - 1);

            PrintArray();

        }
    }
}