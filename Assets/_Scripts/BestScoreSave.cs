using UnityEngine;

[CreateAssetMenu(order = 220)]
public class BestScoreSave : ScriptableObject {

    public int bestScore = 0;

    public void CheckUpdateBestScore(int perhapsNew) {
        if (perhapsNew > bestScore) {
            bestScore = perhapsNew;
        }
    }

}