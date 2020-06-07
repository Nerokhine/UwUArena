using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// TODO Certain Functions and objects are kept server side
public class Animations: MonoBehaviour{
    private float ANIMATION_SPEED = 0.1F;
    private float TRANSLATE_SPEED = 0.01F;

    public IEnumerator AnimateTranslate(GameObject gameObject, int x, int y) {
        Debug.Log("number of threads started");
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        float scale = 60.0f;
        float incrementerX;
        float incrementerY;
        incrementerX = (x - rectTransform.localPosition.x);
        incrementerY = (y - rectTransform.localPosition.y);
        float denominator = Math.Abs(incrementerX) + Math.Abs(incrementerY);
        incrementerX /= denominator;
        incrementerY /= denominator;
        incrementerX *= scale;
        incrementerY *= scale;
        /*Debug.Log("Destination: x: " + x + " y: " + y
        + "\nLocalPosition: localPosition.x: " + rectTransform.localPosition.x + " localPosition.y: " + rectTransform.localPosition.y
        + "\nLocalPosition: incrementerY: " + incrementerY + " incrementerX: " + incrementerX);*/
        while ((rectTransform.localPosition.y < y && rectTransform.localPosition.y + incrementerY < y)
            || (rectTransform.localPosition.y > y && rectTransform.localPosition.y + incrementerY > y)
            || (rectTransform.localPosition.x < x && rectTransform.localPosition.x + incrementerX < x)
            || (rectTransform.localPosition.x > x && rectTransform.localPosition.x + incrementerX > x)) {
                rectTransform.localPosition = new Vector3(rectTransform.localPosition.x + incrementerX,
                    rectTransform.localPosition.y + incrementerY, 0);
                yield return new WaitForSeconds(TRANSLATE_SPEED);
        }
        rectTransform.localPosition = new Vector3(x, y, 0);
        Debug.Log("number of threads finished");
    }

    public IEnumerator AnimateBattle(List<KeyValuePair<List<Player>, string>> battleRecord) {
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
                int yPosition = 250;
                // TODO (Do this step once at the beggining and then update player1 and player2 with lastPlayers objects)
                // No need for player1BattleRoster and player2BattleRoster at that point
                foreach (Minion minion in player1.GetBattleRoster()) {
                    if (i > 0) yPosition = 750;
                    if (i == 1) xPosition = 900;
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
                    xPosition -= 450;
                }
                /*for (int j = i; j < player1BattleRoster.Count; j++) {
                    if (player1BattleRoster[j] != null) GameObject.Destroy(player1BattleRoster[j]);
                    player1BattleRoster.RemoveAt(j);
                }*/
                i = 0;
                xPosition = 0;
                yPosition = -250;
                foreach (Minion minion in player2.GetBattleRoster()) {
                    if (i > 0) yPosition = -750;
                    if (i == 1) xPosition = 900;
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
                    xPosition -= 450;
                }
                /*for (int j = i; j < player2BattleRoster.Count; j++) {
                    if (player2BattleRoster[j] != null) GameObject.Destroy(player2BattleRoster[j]);
                    player2BattleRoster.RemoveAt(j);
                }*/
            } else {
                int i = 0;
                int xPosition = 0;
                int yPosition = 250;
                foreach (Minion minion in player1.GetBattleRoster()) {
                    if (i > 0) yPosition = 750;
                    if (i == 1) xPosition = 900;
                    bool foundMinion = false;
                    foreach (Minion lastMinion in lastPlayer1.GetBattleRoster()) {
                        if (minion.GetID() == lastMinion.GetID()) {
                            foundMinion = true;
                            if (minion.GetFinishedDeath()) {
                                Debug.Log("finishDeath");
                                GameObject.Destroy(lastMinion.GetMinionObject());
                            } else {
                                minion.UpdateMinionObject(lastMinion.GetMinionObject());
                                if (minion.GetMinionObject().GetComponent<RectTransform>().localPosition.x != xPosition ||
                                    minion.GetMinionObject().GetComponent<RectTransform>().localPosition.y != yPosition) {
                                    yield return StartCoroutine(AnimateTranslate(minion.GetMinionObject(), xPosition, yPosition));
                                }
                            }
                        }
                    }
                    if (!foundMinion) {
                        minion.CreateMinionObject(xPosition, yPosition);
                    }
                    i++;
                    xPosition -= 450;
                }
                i = 0;
                xPosition = 0;
                yPosition = -250;
                foreach (Minion minion in player2.GetBattleRoster()) {
                    if (i > 0) yPosition = -750;
                    if (i == 1) xPosition = 900;
                    bool foundMinion = false;
                    foreach (Minion lastMinion in lastPlayer2.GetBattleRoster()) {
                        if (minion.GetID() == lastMinion.GetID()) {
                            foundMinion = true;
                            if (minion.GetFinishedDeath()) {
                                Debug.Log("finishDeath");
                                GameObject.Destroy(lastMinion.GetMinionObject());
                            } else {
                                minion.UpdateMinionObject(lastMinion.GetMinionObject());
                                if (minion.GetMinionObject().GetComponent<RectTransform>().localPosition.x != xPosition ||
                                    minion.GetMinionObject().GetComponent<RectTransform>().localPosition.y != yPosition) {
                                    yield return StartCoroutine(AnimateTranslate(minion.GetMinionObject(), xPosition, yPosition));
                                }
                            }
                        }
                    }
                    if (!foundMinion) {
                        minion.CreateMinionObject(xPosition, yPosition);
                    }
                    i++;
                    xPosition -= 450;
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
}