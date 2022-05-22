
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace Z1HY4N9
{
    /// <summary>
    /// 按下看廣告按鈕後觀看廣告
    /// 看完廣告添加金幣回饋
    /// </summary>
    public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        [SerializeField, Header("看廣告的金幣"), Range(5, 1000)]
        private int addCoinValue = 100;

        private int coinPlayer;

        // 廣告添加金幣
        private Button btnAdsAddCoin;

        private string gameIdAndroid = "4754885"; // 後台Android ID
        private string gameIdIos = "4754884";     // 後台iOS ID
        private string gameId;

        private string adsIdAndroid = "AddCoin";
        private string adsIdIos = "AddCoin";
        private string adsId;
        private Text textCoin; // 玩家金幣數量

        #region 廣告初始化相關
        // 初始化成功會執行的方法
        public void OnInitializationComplete()
        {
            print("<color=green>廣告初始化成功</color>");
        }

        // 初始化失敗會執行的方法
        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            print("<color=red>廣告初始化失敗，原因 : " + message + "</color>");
        }
        #endregion

        #region 廣告載入相關
        // 廣告載入成功會執行的方法
        public void OnUnityAdsAdLoaded(string placementId)
        {
            print("<color=green>廣告載入成功 " + placementId +"</color>");
        }

        //廣告載入失敗會執行的方法
        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            print("<color=red>廣告載入失敗，原因 : " + message + "</color>");
        }
        #endregion

        #region 廣告顯示相關
        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            print("<color=red>廣告顯示失敗，原因 : " + message + "</color>");
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            print("<color=green>廣告顯示開始 " + placementId + "</color>");
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            print("<color=green>廣告顯示點擊 " + placementId + "</color>");
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            print("<color=green>廣告顯示完成 " + placementId + "</color>");

            coinPlayer += addCoinValue;
            textCoin.text = coinPlayer.ToString();
        }
        #endregion

        /// <summary>
        /// 載入廣告
        /// </summary>
        private void LoadAds()
        {
            print("載入廣告，ID : " + adsId);
            Advertisement.Load(adsId, this);
            ShowAds();
        }

        private void ShowAds()
        {
            Advertisement.Show(adsId, this);
        }

        private void Awake()
        {
            textCoin = GameObject.Find("玩家金幣數量").GetComponent<Text>();
            btnAdsAddCoin = GameObject.Find("廣告按鈕添加金幣").GetComponent<Button>();
            btnAdsAddCoin.onClick.AddListener(LoadAds);
            InitializedAds();

            // #if 程式區塊判斷式，條件達成才會執行該區塊
            // 如果 玩家 作業系統 是iOS就指定為 iOS廣告
            // 否則如果 玩家 作業系統 是 Android 就指定為 Android廣告
#if UNITY_IOS
            adsId = adsIdIos;
#elif UNITY_ANDROID
            adsId = adsIdAndroid;
#endif
            // PC端測試
            adsId = adsIdAndroid;

        }


        // 初始化系統
        private void InitializedAds()
        {
            gameId = gameIdAndroid;
            Advertisement.Initialize(gameId, true, this);
        }

        
    }
}

