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
        List<Player> previousPlayerList = new List<Player>();
        foreach(KeyValuePair<List<Player>,string> valuePair in battleRecord) {
            List<Player> playerList = valuePair.Key;
            int playerIndex = 0;
            foreach (Player player in playerList) {
                int battleRosterIndex = 0;
                int xPosition = 0;
                int yPosition = playerIndex == 1 ? -250 : 250;
                foreach (Minion minion in player.GetBattleRoster()) {
                    if (battleRosterIndex > 0) yPosition = playerIndex == 1 ? -750 : 750;
                    if (battleRosterIndex == 1) xPosition = 900;
                    bool foundMinion = false;
                    if (previousPlayerList.Count == 2) {
                        foreach (Minion lastMinion in previousPlayerList[playerIndex].GetBattleRoster()) {
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
                    battleRosterIndex++;
                    xPosition -= 450;
                }
                if (previousPlayerList.Count < 2) {
                    previousPlayerList.Add(player);
                } else {
                    previousPlayerList[playerIndex] = player;
                }
                playerIndex ++;
            }
            GameObject.Find("Info").GetComponent<Text>().text = valuePair.Value;
            yield return new WaitForSeconds(ANIMATION_SPEED);
        }
    }
}