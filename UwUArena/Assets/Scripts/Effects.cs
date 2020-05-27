using System;
using System.Collections;
using System.Collections.Generic;
public class Effects {
    public delegate void Effect(Minion minion);
    private static Dictionary<string,Effect> entryEffects;

    public static void ExecuteEntry(Minion minion) {
        //return entryEffects[minion.GetName()];
    }

}
