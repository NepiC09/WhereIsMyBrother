===en_EN===

What did you want? #speaker:Father
    {isClosetViewed == 1:
    -isKeyTalkedWithDad == 0:
    +Where is the key to the closet? #speaker: Anya
        ->en_EN_Dad_KeyFromCloset
    }
    {isOrderViewed == 1:
    -isOrderTalkedWithDad == 0:
    +discuss the check
        ->en_EN_CheckFromBathroom
    }
    +nothing
        ->END
    
===en_EN_Dad_KeyFromCloset===
You are talking to your father in the kitchen #speaker: System
The key to the closet is in the bag in the hallway
~isTalkedWithDadAfterCloset = 1
~isKeyTalkedWithDad = 1
->en_EN


===en_EN_CheckFromBathroom===
father doesn't want to discuss the check #speaker: System
~isOrderTalkedWithDad = 1
->en_EN