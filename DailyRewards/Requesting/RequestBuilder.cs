using System;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Api.Requesting
{
    public static class RequestBuilder
    {
        public static void OnPostRequest<T>(this IRequestService runner, string link, Func<bool> canRequestDelegate,
            Action<T> onDataReceived, Action onFailed = null) where T : class
        {
            runner.Behaviour.StartCoroutine(SendRequest(UnityWebRequest.Get(link), canRequestDelegate, onDataReceived, onFailed));
        }

        public static void OnGetRequest<T>(this IRequestService runner, string link, Func<bool> canRequestDelegate,
            Action<T> onDataReceived, Action onFailed = null) where T : class
        {
            runner.Behaviour.StartCoroutine(SendRequest(UnityWebRequest.Get(link), canRequestDelegate, onDataReceived, onFailed));
        }

        private static IEnumerator SendRequest<T>(UnityWebRequest request, Func<bool> requestValidation,
            Action<T> onDataReceived, Action onFailed = null) where T : class
        {
            if (requestValidation() == false)
            {
                onFailed?.Invoke();
                yield break;
            }

            yield return request.SendWebRequest();

            bool success = false; 
            T data = null;


            if (request.result == UnityWebRequest.Result.Success)
            {
                try
                {
                    data = JsonConvert.DeserializeObject<T>(request.downloadHandler.text);
                    success = true;
                }
                catch (Exception e)
                {
                }
            }
            
            if(success)
                onDataReceived?.Invoke(data);
            else
                onFailed?.Invoke();

#if UNITY_EDITOR
            Debug.Log(request.downloadHandler.text);
#endif
        }
    }
}
