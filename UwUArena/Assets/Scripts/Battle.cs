using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// TODO Certain Functions and objects are kept server side
public class Battle: MonoBehaviour {
    private List<Player> players = new List<Player>();
    private List<KeyValuePair<List<Player>, string>> battleRecord;
    private bool DEBUG_MESSAGES_ENABLED = true;
    private float ANIMATION_SPEED = 0.1F;
    private float TRANSLATE_SPEED = 0.01F;

    public void AddToBattleRecord(Player player1, Player player2, string message) {
        List<Player> record = new List<Player>();
        if (battleRecord.Count > 1) {
            string name = battleRecord[battleRecord.Count - 2].Key[0].GetName();
            if (player1.GetName() == name) {
                record.Add(player1.Clone(keepID:true));
                record.Add(player2.Clone(keepID:true));
            } else {
                record.Add(player2.Clone(keepID:true));
                record.Add(player1.Clone(keepID:true));
            }
        } else {
            record.Add(player1.Clone(keepID:true));
            record.Add(player2.Clone(keepID:true));
        }
        battleRecord.Add(new KeyValuePair<List<Player>,string>(record, message));
    }

    public List<Player> GetPlayers() {
        return players;
    }

    private bool IsBattleOver(Player player1, Player player2) {
        return !((player1.GetBattleRosterSize() > 0) && (player2.GetBattleRosterSize() > 0));
    }

    private string DebugMinion(Minion minion) {
        return "Name: " + minion.GetName() + ", Attack: " + minion.GetAttack() + ", Health: " + minion.GetHealth() + "\n";
    }

    private string DebugTraps(Player player) {
        List<List<Effect>> traps = player.GetTraps();
        if (traps.Count <= 0) return "";
        int numberOfDifferentTraps = traps[0].Count;
        List<int> trapsAmounts = new List<int>();
        for (int i = 0; i < numberOfDifferentTraps; i++) {
            trapsAmounts.Add(0);
            for(int j = 0; j < traps.Count; j ++) {
                if (traps[j].Count <= i + 1) {
                    trapsAmounts[i] ++;
                }
            }
        }
        string debug = "Traps: ";
        foreach (int trapCount in trapsAmounts) {
           debug += trapCount + ",";
        }
        debug += "\n";
        return debug;
    }

    private string DebugGifts(Player player) {
        List<List<Effect>> gifts = player.GetGifts();
        if (gifts.Count <= 0) return "";
        int numberOfDifferentGifts = gifts[0].Count;
        List<int> giftsAmounts = new List<int>();
        for (int i = 0; i < numberOfDifferentGifts; i++) {
            giftsAmounts.Add(0);
            for(int j = 0; j < gifts.Count; j ++) {
                if (gifts[j].Count <= i + 1) {
                    giftsAmounts[i] ++;
                }
            }
        }
        string debug = "Gifts: ";
        foreach (int giftCount in giftsAmounts) {
           debug += giftCount + ",";
        }
        debug += "\n";
        return debug;
    }

    public IEnumerator AnimateTranslate(GameObject gameObject, int x, int y) {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        float scale = 20.0f;
        float incrementerX;
        float incrementerY;
        incrementerX = (x - rectTransform.localPosition.x);
        incrementerY = (y - rectTransform.localPosition.y);
        float denominator = Math.Abs(incrementerX) + Math.Abs(incrementerY);
        incrementerX /= denominator;
        incrementerY /= denominator;
        incrementerX *= scale;
        incrementerY *= scale;
        Debug.Log("Destination: x: " + x + " y: " + y
        + "\nLocalPosition: localPosition.x: " + rectTransform.localPosition.x + " localPosition.y: " + rectTransform.localPosition.y
        + "\nLocalPosition: incrementerY: " + incrementerY + " incrementerX: " + incrementerX);
        while ((rectTransform.localPosition.y < y && rectTransform.localPosition.y + incrementerY < y)
            || (rectTransform.localPosition.y > y && rectTransform.localPosition.y + incrementerY > y)
            || (rectTransform.localPosition.x < x && rectTransform.localPosition.x + incrementerX < x)
            || (rectTransform.localPosition.x > x && rectTransform.localPosition.x + incrementerX > x)) {
                rectTransform.localPosition = new Vector3(rectTransform.localPosition.x + incrementerX,
                    rectTransform.localPosition.y + incrementerY, 0);
                yield return new WaitForSeconds(TRANSLATE_SPEED);
        }
        rectTransform.localPosition = new Vector3(x, y, 0);
    }

    public IEnumerator AnimateBattle(Main main) {
        //List<GameObject> player1BattleRoster = new List<GameObject>();
        //List<GameObject> player2BattleRoster = new List<GameObject>();
        Player lastPlayer1 = null;
        Player lastPlayer2 = null;
        foreach(KeyValuePair<List<Player>,string> valuePair in battleRecord) {
            List<Player> playerList = valuePair.Key;
            Player player1 = playerList[0];
            Player player2 = playerList[1];
            // First Pass
            if (lastPlayer1 == null || lastPlayer2 == null) {
                int i = 0;
                int xPosition = 0;
                int yPosition = 200;
                // TODO (Do this step once at the beggining and then update player1 and player2 with lastPlayers objects)
                // No need for player1BattleRoster and player2BattleRoster at that point
                foreach (Minion minion in player1.GetBattleRoster()) {
                    if (i > 0) yPosition = 600;
                    if (i == 1) xPosition = 800;
                    //if (player1BattleRoster.Count <= i) {
                        //player1BattleRoster.Add(minion.CreateMinionObject(xPosition, yPosition));
                    minion.CreateMinionObject(xPosition, yPosition);
                    //} else {
                        //minion.UpdateMinionObject(player1BattleRoster[i]);
                        //if (minion.GetName() != player1BattleRoster[i-1].transform.Find("Name").GetComponent<Text>().text) {
                            //yield return StartCoroutine(AnimateTranslate(player1BattleRoster[i], xPosition, yPosition));
                        //}
                    // }
                    i++;
                    xPosition -= 400;
                }
                /*for (int j = i; j < player1BattleRoster.Count; j++) {
                    if (player1BattleRoster[j] != null) GameObject.Destroy(player1BattleRoster[j]);
                    player1BattleRoster.RemoveAt(j);
                }*/
                i = 0;
                xPosition = 0;
                yPosition = -200;
                foreach (Minion minion in player2.GetBattleRoster()) {
                    if (i > 0) yPosition = -600;
                    if (i == 1) xPosition = 800;
                    minion.CreateMinionObject(xPosition, yPosition);
                    //if (player2BattleRoster.Count <= i) {
                       // player2BattleRoster.Add(minion.CreateMinionObject(xPosition, yPosition));
                    //} else {
                        //minion.UpdateMinionObject(player2BattleRoster[i]);
                        //if (minion.GetName() == player2BattleRoster[i-1].transform.Find("Name").GetComponent<Text>().text) {
                            // yield return StartCoroutine(AnimateTranslate(player2BattleRoster[i], xPosition, yPosition));
                        //}
                    //}
                    i++;
                    xPosition -= 400;
                }
                /*for (int j = i; j < player2BattleRoster.Count; j++) {
                    if (player2BattleRoster[j] != null) GameObject.Destroy(player2BattleRoster[j]);
                    player2BattleRoster.RemoveAt(j);
                }*/
            } else {
                int i = 0;
                int xPosition = 0;
                int yPosition = 200;
                foreach (Minion minion in player1.GetBattleRoster()) {
                    if (i > 0) yPosition = 600;
                    if (i == 1) xPosition = 800;
                    bool foundMinion = false;
                    foreach (Minion lastMinion in lastPlayer1.GetBattleRoster()) {
                        if (minion.GetID() == lastMinion.GetID()) {
                            foundMinion = true;
                            if (minion.IsDead()) {
                                GameObject.Destroy(lastMinion.GetMinionObject());
                            } else {
                                minion.UpdateMinionObject(lastMinion.GetMinionObject());
                                yield return StartCoroutine(AnimateTranslate(minion.GetMinionObject(), xPosition, yPosition));
                            }
                        }
                    }
                    if (!foundMinion) {
                        minion.CreateMinionObject(xPosition, yPosition);
                    }
                    i++;
                    xPosition -= 400;
                }
                i = 0;
                xPosition = 0;
                yPosition = -200;
                foreach (Minion minion in player2.GetBattleRoster()) {
                    if (i > 0) yPosition = -600;
                    if (i == 1) xPosition = 800;
                    bool foundMinion = false;
                    foreach (Minion lastMinion in lastPlayer2.GetBattleRoster()) {
                        if (minion.GetID() == lastMinion.GetID()) {
                            foundMinion = true;
                            if (minion.IsDead()) {
                                GameObject.Destroy(lastMinion.GetMinionObject());
                            } else {
                                minion.UpdateMinionObject(lastMinion.GetMinionObject());
                                yield return StartCoroutine(AnimateTranslate(minion.GetMinionObject(), xPosition, yPosition));
                            }
                        }
                    }
                    if (!foundMinion) {
                        minion.CreateMinionObject(xPosition, yPosition);
                    }
                    i++;
                    xPosition -= 400;
                }
            }
            GameObject.Find("Info").GetComponent<Text>().text = valuePair.Value;
            lastPlayer1 = player1;
            lastPlayer2 = player2;
            yield return new WaitForSeconds(ANIMATION_SPEED);
        }
        //Player lastPlayer1 = battleRecord[battleRecord.Count - 1].Key[0];
        //Player lastPlayer2 = battleRecord[battleRecord.Count - 1].Key[1];
    }

    private void FightDebugLogs() {
        if (!DEBUG_MESSAGES_ENABLED) return;
        string debug = "";
        foreach(KeyValuePair<List<Player>,string> valuePair in battleRecord) {
            List<Player> playerList = valuePair.Key;
            Player player1 = playerList[0];
            Player player2 = playerList[1];
            debug += valuePair.Value;
            debug += DebugTraps(player1);
            debug += DebugGifts(player1);
            debug += player1.GetName() + "'s Minions:\n";
            foreach (Minion minion in player1.GetBattleRoster()) {
                debug += DebugMinion(minion);
            }
            debug += "\n";
            debug += DebugTraps(player2);
            debug += DebugGifts(player2);
            debug += player2.GetName() + "'s Minions:\n";
            foreach (Minion minion in player2.GetBattleRoster()) {
                debug += DebugMinion(minion);
            }
            debug += "\n==========================================\n";
        }
        Player lastPlayer1 = battleRecord[battleRecord.Count - 1].Key[0];
        Player lastPlayer2 = battleRecord[battleRecord.Count - 1].Key[1];
        debug += lastPlayer1.GetName() + "'s Health: " + lastPlayer1.GetHealth() + "\n";
        debug += lastPlayer2.GetName() + "'s Health: " + lastPlayer2.GetHealth() + "\n";
        Debug.Log(debug);
    }
    public void Fight(Player player1, Player player2) {
        players = new List<Player>();
        battleRecord = new  List<KeyValuePair<List<Player>, string>>();
        players.Add(player1);
        players.Add(player2);
        player1.StartBattle(this);
        player2.StartBattle(this);

        while (!IsBattleOver(player1, player2)) {
            player1.GetBattlingMinion().Fight(player2.GetBattlingMinion());
            if(IsBattleOver(player1, player2)) break;
            player2.GetBattlingMinion().Fight(player1.GetBattlingMinion());
        }
        // Tie
        if (player1.GetBattleRosterSize() == player2.GetBattleRosterSize()) return;

        Player victor = player1.GetBattleRosterSize() > player2.GetBattleRosterSize() ? player1 : player2;
        Player loser = player1.GetBattleRosterSize() > player2.GetBattleRosterSize() ? player2 : player1;
        loser.TakeDamage(victor.GetBattleRosterSize());

        AddToBattleRecord(player1, player2, "End of Battle:\n");
        FightDebugLogs();
    }
}