using System.Collections;
using UnityEngine;
using UnityEngine.Monetization;

public class RewardedAdsPlacement : MonoBehaviour
{

    public string placementId = "rewardedVideo";

    public void ShowAd()
    {
        StartCoroutine(WaitForAd());
    }

    IEnumerator WaitForAd()
    {
        while (!Monetization.IsReady(placementId))
        {
            yield return null;
        }

        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;

        if (ad != null)
        {
            ad.Show(AdFinished);
        }
    }

    void AdFinished(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            // Reward the player
        }
    }
    
}