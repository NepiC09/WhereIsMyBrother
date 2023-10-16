===en_EN===
What do you want? #speaker: Maloy
    {isFirstTalkedWithLittleBoy == 0:
    +I need a Helmet #speaker: Anya
        ->en_EN_LittleBoy_NeedHelmet
    }
    {isGotIcon == 1 && isGivenIconAndGottenHelmet == 0:
    +Let's exchange for a #speaker badge: Anya
        ->en_EN_LittleBoy_Exchange
    }
    {isKnifeViewed == 1 && isGivenIconAndGottenHelmet == 1 && isRequestedToLittleBoy == 0:
    +Distract Dad #speaker: Anya
        ->en_EN_LittleBoy_Request
    }
    +Bye
        ->END
===en_EN_LittleBoy_NeedHelmet===
First conversation with a small #speaker:System
He needs something cool #speaker:System
~isFirstTalkedWithLittleBoy = 1
->END

===en_EN_LittleBoy_Exchange===
You exchanged with Small #speaker:System
~isGivenIconAndGottenHelmet = 1
->END
===en_EN_LittleBoy_Request===
You asked Maly to distract his father #speaker:System
~isRequestedToLittleBoy = 1
->END