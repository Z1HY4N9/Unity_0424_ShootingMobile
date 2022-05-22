using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

namespace Z1HY4N9
{
	public class IAPManager : MonoBehaviour
	{
		[SerializeField, Header("購買皮膚按鈕")]
		private IAPButton iapBuySkingRed;
		[SerializeField, Header("購買提示訊息")]
		private Text textIAPTip;

		private void Awake()
		{
			// 紅色皮膚內購按鈕 購買成功後 添加監聽器 (購買成功方法)
			iapBuySkingRed.onPurchaseComplete.AddListener(PurchaseCompleteSkinRed);
			// 紅色皮膚內購按鈕 購買失敗後 添加監聽器 (購買失敗方法)
			iapBuySkingRed.onPurchaseFailed.AddListener(PurchaseFailedSkinRed);
		}



		// 購買成功
		private void PurchaseCompleteSkinRed(Product product)
		{
			textIAPTip.text = product.ToString() + " 購買成功 " ;
		}

		// 購買失敗
		private void PurchaseFailedSkinRed(Product product, PurchaseFailureReason reason) 
		{
			textIAPTip.text = product + " 購買失敗 " + reason;
		}







	}

}



