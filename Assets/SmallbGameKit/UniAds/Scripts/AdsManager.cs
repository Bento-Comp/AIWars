using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace UniAds
{
	[DefaultExecutionOrder(-32000)]
	[AddComponentMenu("UniAds/AdsManager")]
	public class AdsManager : MonoBehaviour
	{
		static public System.Action<bool> onRewardedAvailable;

		AdsCaller caller;

        static AdsManager instance;

		public static AdsManager Instance
		{
			get
			{
				return instance;
			}
		}

		public bool RewardedAdAvailable
		{
			get
			{
				bool available = false;
				if(Caller != null)
				{
					available = Caller.RewardedAdAvailable;
				}

				return available;
			}
		}

		AdsCaller Caller
		{
			get
			{
				return caller;
			}
		}

		public void Select(AdsCaller caller)
		{
			if(this.caller != null)
			{
				this.caller.Terminate();
			}

			this.caller = caller;

			if(this.caller != null)
			{
				this.caller.Initialize(this);
			}
		}

        public void ShowRewardedAd(string rewardedAdId, System.Action<bool> onRewardedEnd)
		{
			Debug.Log("AdsManager : ShowRewardedAd");

			if(Caller == null)
				return;

			Caller.ShowRewardedAd(rewardedAdId, onRewardedEnd);
		}

		public void NotifyInterstitialPlacementOpportunity()
		{
			Debug.Log("AdsManager : InterstitialPlacementOpportunity");

			if(RemoveAdsManager.Instance.AdsRemoved)
				return;

			if(Caller == null)
				return;

			Caller.NotifyInterstitialOpportunity();
		}

		public void NotifyRewardedOpportunityStart(string rewardedId)
		{
			if(Caller == null)
				return;

			Caller.NotifyRewardedOpportunityStart(rewardedId);
		}

		public void NotifyRewardedOpportunityEnd(string rewardedId)
		{
			if(Caller == null)
				return;

			Caller.NotifyRewardedOpportunityEnd(rewardedId);
		}

		public void NotifyRewardedAvailable(bool available)
		{
			//Debug.Log("prtInvoke : " + available);
			onRewardedAvailable?.Invoke(available);
		}

		void Awake()
		{
			if(instance == null)
			{
				instance = this;
			}
			else
			{
				Debug.LogWarning("A singleton can only be instantiated once!");
				Destroy(gameObject);
				return;
			}

			RemoveAdsManager.onRemoveAds += OnRemoveAds;
		}

		void OnDestroy()
		{
			RemoveAdsManager.onRemoveAds -= OnRemoveAds;

			if(instance == this)
			{
				instance = null;
			}
		}

		void OnRemoveAds()
		{
			if(Caller == null)
				return;

			Caller.DestroyBanner();
			Caller.DestroyInterstitial();
		}
	}
}
