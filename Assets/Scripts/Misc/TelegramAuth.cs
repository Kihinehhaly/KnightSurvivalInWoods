using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class TelegramAuth : MonoBehaviour
{
    private const string ServerUrl = "http://127.0.0.1:8000";
    private const string BotUsername = "KnightSurvivalInWoodsBot";

    private string _playerUnityID;
    private bool _isLinked = false;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("UnityPlayerID"))
        {
            string newID = Guid.NewGuid().ToString();
            PlayerPrefs.SetString("UnityPlayerID", newID);
            PlayerPrefs.Save();
        }

        _playerUnityID = PlayerPrefs.GetString("UnityPlayerID");
        Debug.Log($"[Unity ID Игрока]: {_playerUnityID}");

        StartCoroutine(RegisterIdOnServer());
    }

    public void OnClickConnectTelegram()
    {
        if (_isLinked)
        {
            Debug.Log("Аккаунт уже привязан!");
            return;
        }

        string link = $"https://t.me/{BotUsername}?start={_playerUnityID}";
        Application.OpenURL(link);

        StartCoroutine(CheckAuthStatusRoutine());
    }

    private IEnumerator RegisterIdOnServer()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get($"{ServerUrl}/register_id/{_playerUnityID}"))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("ID успешно зарегистрирован на сервере!");
            }
        }
    }

    private IEnumerator CheckAuthStatusRoutine()
    {
        while (!_isLinked)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get($"{ServerUrl}/check_status/{_playerUnityID}"))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    string responseText = webRequest.downloadHandler.text;

                    if (responseText.Contains("\"linked\":true"))
                    {
                        _isLinked = true;

                        Debug.Log("🎉 Ура! Сервер подтвердил привязку к Telegram!");

                        StopCoroutine(CheckAuthStatusRoutine());
                    }
                }
            }

            yield return new WaitForSeconds(3f);
        }
    }
}