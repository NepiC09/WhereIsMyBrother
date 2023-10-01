using System.Collections.Generic;
using UnityEngine;

public struct Diary
{
    public const string ru_RU_message = "����� ������ � ��������";
    public const string en_EN_message = "New record in diary";

    public const string ru_RU_FirstMessage = "����� ������� �������, ������� [J]";
    public const string en_EN_FirstMessage = "Press [J] to open diary";

    public const string ru_RU_MessageFirstUpdate = "������� �������� ���� ������. � ������ ��������� �� ���: �����, ��� ������� ������, ��� � ��� �����.";
    public const string ru_RU_MessageSomebodyOrderStuffs = "� �������� ���� ����� ��� � ������� ������� ������� �� ���� �� ������� � �� ������ � ��������. ��� �� ����� � �����? ��������, ���� ��� ������� �������� ���� ������ �� �����? ��� ��� �������� ����� �������, �� � ����������� ��������� �� ������.";
    public const string ru_RU_MessageBadEquip = "������ ������������ ��� �������� ���� � ���������� ��������������. �� ������ ������ ��������, ��� ������ ������ ����� �����. �������� ������, ������� ������, ������ �������, ����� ��������� �������� � ���. �� ��������� �������� ������������ �������.";
    public const string ru_RU_MessageAndrewInCountry = "������ ����� ����, ��� ������ ���������� � ���, ������ �����, ��� �� ����� ������� �� ����� � ���������� ���������� �� ��������, ��� ����� �������� ��������� � ��������� �������, ��������� ��������� � ��� �������� - � ������ �� ��������. ��������, ������ ��� � �� �������� �� ���� ���.";
    public const string ru_RU_MessageGrandmaNumber = "����� � ��������� �� ������, ������� ���� ��� �������, ������ ���� ����� �������. ���������� �� ���������, �� ����� ����� ������ ������: � �� ������ ��� �� � ��� �����������. ����� ������� ��� �������? ����� �� �����������, �� ���������, ����� ���� �� ������� ����� �� ������ � ������.";
    public const string ru_RU_MessageAndrewArested = "������ �� ������ ��� ����� - ��� ����������! ��� ��� ��� ������ ������� � ���� ����� ����� ��� � �����.";

    static private Dictionary<string, bool> conditions = new Dictionary<string, bool>
    {
       {"FirstUpdate", false },         //1

       {"SomebodyOrderStuffs", false }, //2
       {"BadEquip", false },            //3
       {"AndrewInCountry", false },     //4
       {"GrandmaNumber", false },       //5

       {"AndrewArested", false },       //6
    };

    public static bool GetValue(string key)
    {
        if (conditions.ContainsKey(key))
        {
            return conditions[key];
        }
        else
        {
            //Debug.LogWarning("There's no key in conditions in Diary: " + key);
            return false;
        }
    }

    public static void SetValue(string key, bool value)
    {
        if (conditions.ContainsKey(key))
        {
            conditions[key] = value;
            //Debug.Log("Diary: Key " + key + " was changed to: " + value);


            if (value == true)
            {
                Message();

                if(key == "FirstUpdate")
                {
                    FirstUpdateMessage();
                }

                SetDiaryVisual(key);
            }
        }
        else
        {
            //Debug.LogWarning("There's no key in conditions in Diary: " + key);
        }

        if (value == true)
        {
            DialogueManager.Instance.SetVariableState(key, new Ink.Runtime.IntValue(1));
        }
        else
        {
            DialogueManager.Instance.SetVariableState(key, new Ink.Runtime.IntValue(0));
        }
    }

    public static void Message()
    {
        if (GameManager.Instance.GetLanguage() == "ru_RU")
        {
            MessageBoxes.Instance.OpenUpLeftMessage(ru_RU_message);
        }
        else if (GameManager.Instance.GetLanguage() == "en_EN")
        {
            MessageBoxes.Instance.OpenUpLeftMessage(en_EN_message);
        }
    }
    public static void FirstUpdateMessage()
    {
        if (GameManager.Instance.GetLanguage() == "ru_RU")
        {
            MessageBoxes.Instance.OpenDownMiddleMessage(ru_RU_FirstMessage);
        }
        else if (GameManager.Instance.GetLanguage() == "en_EN")
        {
            MessageBoxes.Instance.OpenDownMiddleMessage(en_EN_FirstMessage);
        }
    }

    public static void SetDiaryVisual(string key)
    {
        int index = 0;
        string message = "";
        switch (key)
        {
            case "FirstUpdate":
                index = 1;
                message = ru_RU_MessageFirstUpdate;
                break;
            case "SomebodyOrderStuffs":
                index = 2;
                message = ru_RU_MessageSomebodyOrderStuffs;
                break;
            case "BadEquip":
                index = 3;
                message = ru_RU_MessageBadEquip;
                break;
            case "AndrewInCountry":
                index = 4;
                message = ru_RU_MessageAndrewInCountry;
                break;
            case "GrandmaNumber":
                index = 5;
                message = ru_RU_MessageGrandmaNumber;
                break;
            case "AndrewArested":
                index = 6;
                message = ru_RU_MessageAndrewArested;
                break;
        }
        DiaryGUI.Instance.SetPlace(message, index);
    /*
       "FirstUpdate"-----------1
       "SomebodyOrderStuffs"---2
       "BadEquip"--------------3
       "AndrewInCountry"-------4
       "GrandmaNumber"---------5
       "AndrewArested"---------6
    */
    }
}
