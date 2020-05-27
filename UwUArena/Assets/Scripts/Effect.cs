using System;
using System.Collections;
using System.Collections.Generic;
public class Effects {
    public delegate void Effect(Game game);
    private static Dictionary<string,Effect> entryEffects;

    public static void ExecuteEntry(Minion minion, Game game) {
        //return entryEffects[minion.GetName()];
    }

}
