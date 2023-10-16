===en_EN===
Have you already eaten? #speaker:Granny
GO EAT! #speaker:Granny
    {isOrderViewed == 1:
    -isOrderTalkedWithGrandma == 0:
    +discuss the check
        ->en_EN_CheckFromBathroom_Grandma
    }
    {isGreenStarted == 1:
    -isTalkedAboutAndrewWithGrandma == 0:
    +Discuss Andrey
        ->en_EN_TalkAboutAndrew
    }
    {isSewingKitGotten == 1 && isTriedToTalkAboutPresents == 0:
    +Try to talk about gifts
        ->en_EN_TryToTalkAboutPresents
    }
    {isSlept == 1:
    -isTalkedAfterSleepWithGrandma == 0:
    +Talking about something after sleep
        ->en_EN_TalkAfterSleepWithGrandma
    }
    {isClosetViewed == 1:
    -isKeyTalkedWithGrandma == 0:
    +Where are the keys?
        ->en_EN_TalkAboutKeyToCloset
    }
    {isLawyerCalled == 1:
    -isTalkedAboutLawyerWithGrandma == 0:
    +Discuss a lawyer
        ->en_EN_talkAboutLawyer
    }
    +Okay
        ->END

===en_EN_CheckFromBathroom_Grandma===
You learned information about the check from your grandmother #speaker: System
~isOrderTalkedWithGrandma = 1
->en_EN

===en_EN_TalkAboutAndrew===
You are talking to your grandmother about Andrey #speaker: System
Dad intervenes in the conversation
Now go to sleep
~isTalkedAboutAndrewWithGrandma = 1
->END

===en_EN_TryToTalkAboutPresents===
You tried to talk about gifts
~isTriedToTalkAboutPresents = 1
->END


===en_EN_TalkAfterSleepWithGrandma===
We talked about something after sleep #speaker: System
~isTalkedAfterSleepWithGrandma = 1
->END

===en_EN_TalkAboutKeyToCloset===
Grandma won't say where the keys to the closet are #speaker: System
~isKeyTalkedWithGrandma = 1
->END

===en_EN_talkAboutLawyer===
You are discussing a lawyer with your grandmother #speaker: System
Father is unhappy
~isBubbleStarted = 1
~isTalkedAboutLawyerWithGrandma = 1
...
......
........
->END