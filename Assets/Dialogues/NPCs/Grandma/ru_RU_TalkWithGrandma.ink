===ru_RU===
Ты уже покушала? #speaker:Бабушка
ИДИ КУШАТЬ! #speaker:Бабушка
    {isOrderViewed == 1:
    -isOrderTalkedWithGrandma == 0:
    +обсудить чек 
        ->ru_RU_CheckFromBathroom_Grandma
    }
    {isGreenStarted == 1:
    -isTalkedAboutAndrewWithGrandma == 0:
    +Обсудить Андрея
        ->ru_RU_TalkAboutAndrew
    }
    {isSewingKitGotten == 1 && isTriedToTalkAboutPresents == 0:
    +Попытаться поговорить о подарках
        ->ru_RU_TryToTalkAboutPresents
    }
    {isSlept == 1:
    -isTalkedAfterSleepWithGrandma == 0:
    +Говорить о чём-то после сна
        ->ru_RU_TalkAfterSleepWithGrandma
    }
    {isClosetViewed == 1:
    -isKeyTalkedWithGrandma == 0:
    +Где ключи?
        ->ru_RU_TalkAboutKeyToCloset
    }
    {isLawyerCalled == 1:
    -isTalkedAboutLawyerWithGrandma == 0:
    +Обсудить адвоката
        ->ru_RU_talkAboutLawyer
    }
    +Ладно
        ->END

===ru_RU_CheckFromBathroom_Grandma===
Вы узнали от бабушки информацию о чеке #speaker: Система
~isOrderTalkedWithGrandma = 1
->ru_RU

===ru_RU_TalkAboutAndrew===
Вы разговариваете с бабушкой об Андрее #speaker: Система
В разговор вмешивается батя 
А теперь идите спать
~isTalkedAboutAndrewWithGrandma = 1
->END

===ru_RU_TryToTalkAboutPresents===
Вы попытались поговорить о подарках
~isTriedToTalkAboutPresents = 1
->END


===ru_RU_TalkAfterSleepWithGrandma===
Поговорили о чём-то после сна #speaker: Система
~isTalkedAfterSleepWithGrandma = 1
->END

===ru_RU_TalkAboutKeyToCloset===
Бабушка не говорит где ключи от шкафа #speaker: Система
~isKeyTalkedWithGrandma = 1
->END

===ru_RU_talkAboutLawyer===
Вы обсуждаете с бабушкой адвоката #speaker: Система
Отец недоволен
~isBubbleStarted = 1
~isTalkedAboutLawyerWithGrandma = 1
...
......
........
->END