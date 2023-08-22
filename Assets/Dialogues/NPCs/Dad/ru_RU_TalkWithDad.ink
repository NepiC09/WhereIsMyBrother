===ru_RU===

Чего хотела? #speaker:Отец
    {isClosetViewed == 1: 
    -isKeyTalkedWithDad == 0:
    +Где ключ от шкафа? #speaker: Аня
        ->ru_RU_Dad_KeyFromCloset
    }
    {isOrderViewed == 1:
    -isOrderTalkedWithDad == 0:
    +обсудить чек 
        ->ru_RU_CheckFromBathroom
    }
    +ничего
        ->END
    
===ru_RU_Dad_KeyFromCloset===
Вы разговариваете с отцом на кухне #speaker: Система
Ключ от шкафа в сумке в прихожей 
~isTalkedWithDadAfterCloset = 1
~isKeyTalkedWithDad = 1
->ru_RU


===ru_RU_CheckFromBathroom===
отец не хочет обсуждать чек #speaker: Система
~isOrderTalkedWithDad = 1
->ru_RU