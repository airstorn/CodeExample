using System.Collections.Generic;
using Api;
using Core;
using Game.DailyRewards.Api.Data;
using Game.UI.MainScreen.Theft;
using Game.UI.Pages;
using SocialPlatforms.GameCenter;
using SocialPlatforms.ServerApi;
using UnityEngine;
using Utils.UI;

namespace Game.DailyRewards.Api
{
    [RequireComponent(typeof(ContentBuilder))]
    public class DailyRewardChecker : MonoBehaviour, ISceneEntryPoint
    {
        private ContentBuilder _builder;
        
        public void Boot()
        {
            _builder = GetComponent<ContentBuilder>();
            Validate();
        }

        private void Validate()
        {
            if (ApiDailyRewardsService.IsInitialized == false)
            {
#if UNITY_EDITOR
                Debug.LogWarning("api not initialized");
#endif
                return;
            }

            if (UserIdProvider.Instance.IsLogged == false)
            {
                UserDataService.Instance.UpdateSuccess += WaitForDataUpdate;
#if UNITY_EDITOR
                Debug.Log("daily rewards: player not initialized, wait for update");
#endif
            }
            else
            {
                ApiDailyRewardsService.Instance.TakeReward(OnDataReceived);
#if UNITY_EDITOR
                Debug.Log("daily rewards: player initialized, send reward request");
#endif
            }
        }

        private void WaitForDataUpdate()
        {
            UserDataService.Instance.UpdateSuccess -= WaitForDataUpdate;
            
            ApiDailyRewardsService.Instance.TakeReward(OnDataReceived);
#if UNITY_EDITOR
            Debug.Log("daily rewards: player updated, wait data");
#endif
        }

        private void OnDataReceived(DailyRewardsModel obj)
        {
            if(obj == null) return;

            if (obj.Status == false)
            {
#if UNITY_EDITOR
                Debug.Log("status false");
#endif
                return;
            }
            
            if (obj.Data.Reward != null)
            {
                var data = _builder.ApplyData(obj);
                var current = _builder.ApplyData(obj.Data.Reward);
                DisplayRewards(data, current);
                return;
            }
        }

        private void DisplayRewards(List<DailyReward> model, DailyReward rewardPosition)
        {
            UserDataService.Instance.UpdatePlayerData();
            Debug.Log("show screen");

            DailyRewardsScreen.Show();
            
            DailyRewardsScreen.Instance.FillRewards(model);
            DailyRewardsScreen.Instance.SetCurrentReward(rewardPosition);
        }

        public void Unload()
        {
        }
    }
}