using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour ,IUnityAdsListener
{

    public string AndroidID = "";
    private string Reward_Ad = "Rewarded_Android";

    private void Awake()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(AndroidID,true);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { Play_Rewarded_Ad(); }
    }

    public void Play_Rewarded_Ad() {
        if (!Advertisement.IsReady(Reward_Ad)) { return; }
        Advertisement.Show(Reward_Ad);
    }

    //-----------------------------------------
    public void OnUnityAdsReady(string placementId)
    {
       // throw new System.NotImplementedException();
    }
    public void OnUnityAdsDidError(string message)
    {
       // throw new System.NotImplementedException();
    }
    public void OnUnityAdsDidStart(string placementId)
    {
        //throw new System.NotImplementedException();
    }
    //-----------------------------------------
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult) {

            case ShowResult.Failed:break;
            case ShowResult.Skipped:break;
            case ShowResult.Finished:
                if (placementId == Reward_Ad) {

                    FindObjectOfType<GameManager>().Auto_Flag_Mine();
                
                }
                break;
        
        
        }

    }
}
