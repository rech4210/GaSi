using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class RotateManager : MonoBehaviour
{

    [SerializeField] private AsyncAwait[] _awaits;


    int asd = 0;


    private CancellationTokenSource _cancellationTokenSource;

    public void Run()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
        _cancellationTokenSource = new CancellationTokenSource();
        RunTask(_cancellationTokenSource.Token);
    }

    private async void RunTask(CancellationToken token)
    {
        try
        {
            await Task.Delay(TimeSpan.FromSeconds(10), token);
        }
        catch (Exception)
        {

            throw;
        }
    }
 

    private async void Start()
    {
        Test();
        await Uni_task();
    }
    public async UniTask<string> Uni_task()
    {
        var asset = await Resources.LoadAsync<TextAsset>("buff");
        var txt = (await UnityWebRequest.Get("https").SendWebRequest()).downloadHandler.text;
        await SceneManager.LoadSceneAsync(StringManager.Instance.MainScene);

        var asset2 = await Resources.LoadAsync<TextAsset>("bar").WithCancellation(this.GetCancellationTokenOnDestroy());
        var asset3 = await Resources.LoadAsync<TextAsset>("baz").ToUniTask(Progress.Create<float>((x) => Debug.Log(x)));

        await UniTask.DelayFrame(100);
        await UniTask.Delay(TimeSpan.FromSeconds(10), ignoreTimeScale: true);
        await UniTask.Yield(PlayerLoopTiming.PreLateUpdate);

        // yield return null
        await UniTask.Yield();
        await UniTask.NextFrame();


        //need CorutineRunner
        await UniTask.WaitForEndOfFrame(this); // mono
        await UniTask.WaitForFixedUpdate();

        await UniTask.WaitUntil(() => isActiveAndEnabled == true);
        await UniTask.WaitUntilValueChanged<RotateManager,bool>(this, x => x.isActiveAndEnabled);

        await Mycorutine(); // can await corutine

        var a = await Task<int>.Run(() => 100);

        // Multithreading, run on ThreadPool under this code
        await UniTask.SwitchToThreadPool();

        /* work on ThreadPool */

        // return to MainThread(same as `ObserveOnMainThread` in UniRx)
        await UniTask.SwitchToMainThread();

        // get async webrequest
        async UniTask<string> GetTextAsync(UnityWebRequest req)
        {
            var op = await req.SendWebRequest();
            return op.downloadHandler.text;
        }

        var task1 = GetTextAsync(UnityWebRequest.Get("http://google.com"));
        var task2 = GetTextAsync(UnityWebRequest.Get("http://bing.com"));
        var task3 = GetTextAsync(UnityWebRequest.Get("http://yahoo.com"));

        // concurrent async-wait and get results easily by tuple syntax
        var (google, bing, yahoo) = await UniTask.WhenAll(task1, task2, task3);

        // shorthand of WhenAll, tuple can await directly
        var (google2, bing2, yahoo2) = await (task1, task2, task3);

        // return async-value.(or you can use `UniTask`(no result), `UniTaskVoid`(fire and forget)).
        return  (asset as TextAsset)?.text ?? throw new InvalidOperationException("Asset not found");
    }

    IEnumerator Mycorutine()
    {
        yield return null;
    }

    public async void Test()
    {
        // 1. Task await

        Task someTask = Task.Run(() => Debug.Log("Use Task"));
        var ggd = someTask;
        await someTask;

        // 2. Task Generic await
        int number = 0;
        Task<int> intTask = Task<int>.Run(() => number++);
        var ads  = await intTask;

        // 3. Task New action collback

        Task sometask2 = Task.Factory.StartNew(() => number++);
        var asd = sometask2;

        Task<int> intTaskWithNewCallBack = Task<int>.Factory.StartNew(() => number++);


        Stack<Task> taskStacks = new Stack<Task>();
        taskStacks.Push(ggd); taskStacks.Push(intTask); taskStacks.Push(asd);
        var tasts = taskStacks.Pop();
        tasts.Wait(5000);


        await Task.Run(() => { Debug.Log("12321"); });




        //Debug.Log("stars tasks");

        //await _awaits[0].RotateForSeconds(1);

        //var tasks = new List<Task>();

        //for (var i = 1; i < _awaits.Length; i++)
        //{
        //    tasks.Add(_awaits[i].RotateForSeconds(1 + 1 * i));
        //}

        //await Task.WhenAll(tasks);

        //Debug.Log("end tasks");

        //var randomnum = GetRandomNumber().Result;

        //print(randomnum);
    }

    //async Task<int> GetRandomNumber()
    //{
    //    //var randomnum = Random.Range(100, 300);
    //    //await Task.Delay(randomnum);
    //    //return randomnum;
    //}
}
