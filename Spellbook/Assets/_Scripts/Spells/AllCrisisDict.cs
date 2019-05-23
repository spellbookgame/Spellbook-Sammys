using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCrisisDict 
{
    public static readonly Dictionary<string, Action> CallCrisis = new Dictionary<string, Action>
    {
        {"Tsunami", CrisisHandler.instance.CallTsunami},
        {"Plague", CrisisHandler.instance.CallPlague },
        {"Boss Battle", CrisisHandler.instance.CallBossBattle }
    };


    public static readonly Dictionary<string, Action> FinishCrisis = new Dictionary<string, Action>
    {
        {"Tsunami", CrisisHandler.instance.FinishTsunami},
        {"Plague", CrisisHandler.instance.FinishPlague },
        {"Boss Battle", CrisisHandler.instance.FinishBossBattle }
    };
}
