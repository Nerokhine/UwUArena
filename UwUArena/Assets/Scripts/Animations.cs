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
    }

    public IEnumerator AnimateBattle(List<KeyValuePair<List<Player>, string>> battleRecord) {
        Player lastPlayer1 = null;
        Player lastPlayer2 = null;
        foreach(KeyValuePair<List<Player>,string> valuePair in battleRecord) {
            List<Player> playerList = valuePair.Key;
            Player player1 = playerList[0];
            Player player2 = playerList[1];
            // First Pass
            /*if (lastPlayer1 == null || lastPlayer2 == null) {
                int i = 0;
                int xPosition = 0;
                int yPosition = 250;
                foreach (Minion minion in player1.GetBattleRoster()) {
                    if (i > 0) yPosition = 750;
                    if (i == 1) xPosition = 900;
                    minion.CreateMinionObject(xPosition, yPosition);
                    i++;
                    xPosition -= 450;
                }
                i = 0;
                xPosition = 0;
                yPosition = -250;
                foreach (Minion minion in player2.GetBattleRoster()) {
                    if (i > 0) yPosition = -750;
                    if (i == 1) xPosition = 900;
                    minion.CreateMinionObject(xPosition, yPosition);
                    i++;
                    xPosition -= 450;
                }
            } else {*/
                int i = 0;
                int xPosition = 0;
                int yPosition = 250;
                foreach (Minion minion in player1.GetBattleRoster()) {
                    if (i > 0) yPosition = 750;
                    if (i == 1) xPosition = 900;
                    bool foundMinion = false;
                    if (lastPlayer1 != null) {
                        foreach (Minion lastMinion in lastPlayer1.GetBattleRoster()) {
                            if (minion.GetID() == lastMinion.GetID()) {
                                foundMinion = true;
                                if (minion.GetFinishedDeath()) {
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
                    if (lastPlayer2 != null) {
                        foreach (Minion lastMinion in lastPlayer2.GetBattleRoster()) {
                            if (minion.GetID() == lastMinion.GetID()) {
                                foundMinion = true;
                                if (minion.GetFinishedDeath()) {
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
                    }
                    if (!foundMinion) {
                        minion.CreateMinionObject(xPosition, yPosition);
                    }
                    i++;
                    xPosition -= 450;
                }
            //}
            GameObject.Find("Info").GetComponent<Text>().text = valuePair.Value;
            lastPlayer1 = player1;
            lastPlayer2 = player2;
            yield return new WaitForSeconds(ANIMATION_SPEED);
        }
        //Player lastPlayer1 = battleRecord[battleRecord.Count - 1].Key[0];
        //Player lastPlayer2 = battleRecord[battleRecord.Count - 1].Key[1];
    }
}