===ru_RU===
Чего тебе? #speaker: Малой
    {isFirstTalkedWithLittleBoy == 0:
    +Мне нужна Каска #speaker: Аня
        ->ru_RU_LittleBoy_NeedHelmet
    }
    {isGotIcon == 1 && isGivenIconAndGottenHelmet == 0:
    +Давай обменяемся на значок #speaker: Аня
        ->ru_RU_LittleBoy_Exchange
    }
    {isKnifeViewed == 1 && isGivenIconAndGottenHelmet == 1 && isRequestedToLittleBoy == 0:
    +Отвлеки батю #speaker: Аня
        ->ru_RU_LittleBoy_Request
    }
    +Пока
        ->END
===ru_RU_LittleBoy_NeedHelmet===
Первый разговор с малым #speaker:Система
Ему нужно что-то крутое #speaker:Система
~isFirstTalkedWithLittleBoy = 1
->END

===ru_RU_LittleBoy_Exchange===
Вы обменялись с Малым #speaker:Система
~isGivenIconAndGottenHelmet = 1
->END
===ru_RU_LittleBoy_Request===
Вы попросили Малого отвлечь отца #speaker:Система
~isRequestedToLittleBoy = 1
->END