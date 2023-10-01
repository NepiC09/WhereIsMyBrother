using System.Collections.Generic;
using UnityEngine;

public struct Diary
{
    public const string ru_RU_message = "Новая запись в дневнике";
    public const string en_EN_message = "New record in diary";

    public const string ru_RU_FirstMessage = "Чтобы открыть дневник, нажмите [J]";
    public const string en_EN_FirstMessage = "Press [J] to open diary";

    public const string ru_RU_MessageFirstUpdate = "Сегодня привезли вещи Андрея. Я должна взглянуть на них: может, так удастся понять, что с ним стало.";
    public const string ru_RU_MessageSomebodyOrderStuffs = "В мусорном баке лежал чек с длинным списком покупок ни одну из которых я не видела в квартире. Кто их купил и зачем? Возможно, отец или бабушка собирали вещи Андрею на фронт? Все это выглядит очень странно, но я обязательно докопаюсь до правды.";
    public const string ru_RU_MessageBadEquip = "Солдат использовали как пушечное мясо и относились соответствующе. Им выдали ржавые автоматы, они носили летнюю форму зимой. Половина отряда, включая Андрея, подала рапорты, прося отправить батальон в тыл. На удивление командир удовлетворил просьбу.";
    public const string ru_RU_MessageAndrewInCountry = "Вскоре после того, как солдат переправил в тыл, Андрей узнал, что их хотят вернуть на фронт в сообщениях сослуживцу он упоминал, что хочет сообщить командиру о нежелании воевать, последнее сообщение в его телефоне - о выходе их роуминга. Вероятно, Андрей так и не вернулся на поле боя.";
    public const string ru_RU_MessageGrandmaNumber = "Когда я позвонила по номеру, который дала мне бабушку, трубку взял некий адвокат. Сославшись на занятость, он почти сразу бросил трубку: я не успела его ни о чем расспросить. Зачем бабушке его контакт? Нужно ее расспросить, но осторожно, чтобы отец не услышал иначе он придет в ярость.";
    public const string ru_RU_MessageAndrewArested = "Андрей не пропал без вести - его арестовали! Все эти два месяца бабушка и отец нагло врали мне в глаза.";

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
