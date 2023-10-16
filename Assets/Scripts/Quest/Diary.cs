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

    ///  ENGLISH
    public const string en_EN_MessageFirstUpdate = "Andrei's things were delivered today. I should look at them: maybe this way I can understand what happened to him.";
    public const string en_EN_MessageSomebodyOrderStuffs = "In the trash can was a receipt with a long list of purchases, none of which I saw in the apartment. Who bought them and why? Perhaps his father or grandmother were packing Andrei�s things for the front? All this looks very strange, but I will definitely get to the bottom of the truth.";
    public const string en_EN_MessageBadEquip = "������ ������������ ��� �������� ���� � ���������� ��������������. �� ������ ������ ��������, ��� ������ ������ ����� �����. �������� ������, ������� ������, ������ �������, ����� ��������� �������� � ���. �� ��������� �������� ������������ �������.";
    public const string en_EN_MessageAndrewInCountry = "The soldiers were used as cannon fodder and treated as such. They were given rusty machine guns and wore summer uniforms in winter. Half of the detachment, including Andrei, submitted reports asking to send the battalion to the rear. Surprisingly, the commander granted the request.";
    public const string en_EN_MessageGrandmaNumber = "When I called the number my grandmother gave me, a lawyer answered the phone. Citing he was busy, he hung up almost immediately: I didn�t have time to ask him anything. Why does grandma need his contact? You need to ask her, but be careful so that your father doesn�t hear, otherwise he will be furious.";
    public const string en_EN_MessageAndrewArested = "Andrei did not go missing - he was arrested! All these two months, my grandmother and father blatantly lied to my face.";
    /// 

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
        if (LanguageManager.Instance.language == LanguageManager.RU_LOCALIZATION)
        {
            MessageBoxes.Instance.OpenUpLeftMessage(ru_RU_message);
        }
        else if (LanguageManager.Instance.language == LanguageManager.EN_LOCALIZATION)
        {
            MessageBoxes.Instance.OpenUpLeftMessage(en_EN_message);
        }
    }
    public static void FirstUpdateMessage()
    {
        if (LanguageManager.Instance.language == LanguageManager.RU_LOCALIZATION)
        {
            MessageBoxes.Instance.OpenDownMiddleMessage(ru_RU_FirstMessage);
        }
        else if (LanguageManager.Instance.language == LanguageManager.EN_LOCALIZATION)
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
                if (LanguageManager.Instance.language == LanguageManager.RU_LOCALIZATION)
                {
                    message = ru_RU_MessageFirstUpdate;
                }
                else if (LanguageManager.Instance.language == LanguageManager.EN_LOCALIZATION)
                {
                    message = en_EN_MessageFirstUpdate;
                }
                break;
            case "SomebodyOrderStuffs":
                index = 2;
                if (LanguageManager.Instance.language == LanguageManager.RU_LOCALIZATION)
                {
                    message = ru_RU_MessageSomebodyOrderStuffs;
                }
                else if (LanguageManager.Instance.language == LanguageManager.EN_LOCALIZATION)
                {
                    message = en_EN_MessageSomebodyOrderStuffs;
                }
                break;
            case "BadEquip":
                index = 3;
                if (LanguageManager.Instance.language == LanguageManager.RU_LOCALIZATION)
                {
                    message = ru_RU_MessageBadEquip;
                }
                else if (LanguageManager.Instance.language == LanguageManager.EN_LOCALIZATION)
                {
                    message = en_EN_MessageBadEquip;
                }
                break;
            case "AndrewInCountry":
                index = 4;
                if (LanguageManager.Instance.language == LanguageManager.RU_LOCALIZATION)
                {
                    message = ru_RU_MessageAndrewInCountry;
                }
                else if (LanguageManager.Instance.language == LanguageManager.EN_LOCALIZATION)
                {
                    message = en_EN_MessageAndrewInCountry;
                }
                break;
            case "GrandmaNumber":
                index = 5;
                if (LanguageManager.Instance.language == LanguageManager.RU_LOCALIZATION)
                {
                    message = ru_RU_MessageGrandmaNumber;
                }
                else if (LanguageManager.Instance.language == LanguageManager.EN_LOCALIZATION)
                {
                    message = en_EN_MessageGrandmaNumber;
                }
                break;
            case "AndrewArested":
                index = 6;
                if (LanguageManager.Instance.language == LanguageManager.RU_LOCALIZATION)
                {
                    message = ru_RU_MessageAndrewArested;
                }
                else if (LanguageManager.Instance.language == LanguageManager.EN_LOCALIZATION)
                {
                    message = en_EN_MessageAndrewArested;
                }
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
