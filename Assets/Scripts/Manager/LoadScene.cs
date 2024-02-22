using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System;

public class LoadScene : MonoBehaviour
{
    
    private float time;
    public Slider slider;
    private string sceneName = "02MainScene";
    [SerializeField] private GameObject LoadPopUp;
    
    async void Start()
    {
        LoadPopUp.SetActive(true);
        await LoadAsyncScene();
    }


    //make await in here to control downloading asset
    // it seems need multiThread with unitask

    private async UniTask<GameObject> GetData()
    {
        var asset =  await Resources.LoadAsync<GameObject>(StringManager.Instance.buffData) as GameObject;
        await UniTask.WaitUntil(() => asset != null);
        return asset;
    }
    IEnumerator LoadAsyncScene()
    {
        //여기서 에러가 뜸.
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        asyncOperation.allowSceneActivation = false;

        while(!asyncOperation.isDone)
        {
            time += Time.time;
            slider.value = time / 10f;

            //fake loading
            if(time > 100)
            {
                //yield return new WaitForSeconds(1f);
                asyncOperation.allowSceneActivation = true;
                LoadPopUp.SetActive(false);
            }
            yield return null;

        }

    }
    
}
