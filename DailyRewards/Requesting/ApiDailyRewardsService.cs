using System;
using System.Collections;
using Api.Requesting;
using Game.DailyRewarding;
using Game.DailyRewarding.Api;
using Game.DailyRewarding.Api.Data;
using Newtonsoft.Json;
using SocialPlatforms.GameCenter;
using SocialPlatforms.ServerApi;
using UnityEngine;
using UnityEngine.Networking;
using Utils;

namespace Api
{
    public class ApiDailyRewardsService : Singleton<ApiDailyRewardsService>, IRequestService
    {
        private const string _listLink = "example link";
        private const string _takeLink = "example link";

        private string UserId => UserIdProvider.Instance.LocalUserId;
        
        public MonoBehaviour Behaviour => this;
        
        public bool CanRequest()
        {
            return UserIdProvider.Instance.IsLogged && Application.internetReachability != NetworkReachability.NotReachable;
        }

        public void GetRewardsArray(Action<DailyRewardsModel> onDataReceived)
        {
            var builtLink = BuildLink(_listLink, UserId);
            StartCoroutine(SendRequest(builtLink, onDataReceived));
        }
        
        public void TakeReward(Action<DailyRewardsModel> onDataReceived)
        {
            var builtLink = BuildLink(_takeLink, UserId);
            StartCoroutine(SendRequest(builtLink, onDataReceived));
        }

        private IEnumerator SendRequest<T>(string link, Action<T> onDataReceived) where T : class
        {
            if (CanRequest() == false)
            {
                onDataReceived?.Invoke(null);
                yield break;
            }
            
            var request = UnityWebRequest.Get(link);
            
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                T obj = null;
                try
                {
                    obj = JsonConvert.DeserializeObject<T>(request.downloadHandler.text);
                }
                catch (Exception e)
                {
                    // ignored
                }

                onDataReceived?.Invoke(obj);
            }
            

#if UNITY_EDITOR
            Debug.Log(request.downloadHandler.text);
#endif
        }

        private string BuildLink(string link, string playerId)
        {
            return string.Format(link, playerId);
        }

    }
}