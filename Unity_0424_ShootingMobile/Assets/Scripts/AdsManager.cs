
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace Z1HY4N9
{
    /// <summary>
    /// ���U�ݼs�i���s���[�ݼs�i
    /// �ݧ��s�i�K�[�����^�X
    /// </summary>
    public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        [SerializeField, Header("�ݼs�i������"), Range(5, 1000)]
        private int addCoinValue = 100;

        private int coinPlayer;

        // �s�i�K�[����
        private Button btnAdsAddCoin;

        private string gameIdAndroid = "4754885"; // ��xAndroid ID
        private string gameIdIos = "4754884";     // ��xiOS ID
        private string gameId;

        private string adsIdAndroid = "AddCoin";
        private string adsIdIos = "AddCoin";
        private string adsId;
        private Text textCoin; // ���a�����ƶq

        #region �s�i��l�Ƭ���
        // ��l�Ʀ��\�|���檺��k
        public void OnInitializationComplete()
        {
            print("<color=green>�s�i��l�Ʀ��\</color>");
        }

        // ��l�ƥ��ѷ|���檺��k
        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            print("<color=red>�s�i��l�ƥ��ѡA��] : " + message + "</color>");
        }
        #endregion

        #region �s�i���J����
        // �s�i���J���\�|���檺��k
        public void OnUnityAdsAdLoaded(string placementId)
        {
            print("<color=green>�s�i���J���\ " + placementId +"</color>");
        }

        //�s�i���J���ѷ|���檺��k
        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            print("<color=red>�s�i���J���ѡA��] : " + message + "</color>");
        }
        #endregion

        #region �s�i��ܬ���
        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            print("<color=red>�s�i��ܥ��ѡA��] : " + message + "</color>");
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            print("<color=green>�s�i��ܶ}�l " + placementId + "</color>");
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            print("<color=green>�s�i����I�� " + placementId + "</color>");
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            print("<color=green>�s�i��ܧ��� " + placementId + "</color>");

            coinPlayer += addCoinValue;
            textCoin.text = coinPlayer.ToString();
        }
        #endregion

        /// <summary>
        /// ���J�s�i
        /// </summary>
        private void LoadAds()
        {
            print("���J�s�i�AID : " + adsId);
            Advertisement.Load(adsId, this);
            ShowAds();
        }

        private void ShowAds()
        {
            Advertisement.Show(adsId, this);
        }

        private void Awake()
        {
            textCoin = GameObject.Find("���a�����ƶq").GetComponent<Text>();
            btnAdsAddCoin = GameObject.Find("�s�i���s�K�[����").GetComponent<Button>();
            btnAdsAddCoin.onClick.AddListener(LoadAds);
            InitializedAds();

            // #if �{���϶��P�_���A����F���~�|����Ӱ϶�
            // �p�G ���a �@�~�t�� �OiOS�N���w�� iOS�s�i
            // �_�h�p�G ���a �@�~�t�� �O Android �N���w�� Android�s�i
#if UNITY_IOS
            adsId = adsIdIos;
#elif UNITY_ANDROID
            adsId = adsIdAndroid;
#endif
            // PC�ݴ���
            adsId = adsIdAndroid;

        }


        // ��l�ƨt��
        private void InitializedAds()
        {
            gameId = gameIdAndroid;
            Advertisement.Initialize(gameId, true, this);
        }

        
    }
}

