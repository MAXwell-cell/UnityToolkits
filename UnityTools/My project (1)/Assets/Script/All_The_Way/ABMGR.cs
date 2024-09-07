using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABMGR : MonoBehaviour
{
    //AB包管理器 目的是
    //让外部更方便的进行资源加载
    //AB包不能重复加载,重复加载会报错
    //用字典存储加载过的AB包
    private AssetBundle ABMain = null;
    //依赖包使用的配置文件
    private Dictionary<string, AssetBundle> abDic = new Dictionary<string, AssetBundle>();
    /// <summary>
    /// AB包存放路径 方便修改
    /// </summary>
    private AssetBundleManifest mainfest = null;
    private string pathUrl
    {
        get
        {
            return Application.streamingAssetsPath + "/";
        }
    }
    private string mainName
    {
        get
        {
#if UNITY_EDITOR
            return "PC";
#elif UNITY_WEBGL
            return "Webgl";
#else
            return "PC";
#endif
        }
    }
    public void LoadRes(string abname, string resName)
    {
        //加载AB包
        //获取依赖包相关信息
        //加载主包
        //加载主包中的关键配置文件 获取依赖包
        //加载依赖包
        //加载目标包
        //
        
        //主包和配置文件只加载一次
        if (ABMain == null)
        {
            ABMain = AssetBundle.LoadFromFile(pathUrl + mainName);
            mainfest = ABMain.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        AssetBundle AB = AssetBundle.LoadFromFile(pathUrl + abname);
        GameObject a = AB.LoadAsset<GameObject>(resName);
    }
    public void asynLoadRes(string abname, string resName)
    {

    }
    IEnumerator LoadResource(string abname, string resName)
    {
        AssetBundleCreateRequest ascs = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/" + abname);
        yield return ascs;
        AssetBundleRequest abcs = ascs.assetBundle.LoadAssetAsync<Sprite>(resName);
        yield return abcs;
    }
}
