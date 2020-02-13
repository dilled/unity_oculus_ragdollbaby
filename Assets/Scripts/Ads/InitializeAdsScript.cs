using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAdsScript : MonoBehaviour
{

    //string gameId = "3196173"; 
    string gameId = "3196173";
    bool testMode = false;

    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
    }
}