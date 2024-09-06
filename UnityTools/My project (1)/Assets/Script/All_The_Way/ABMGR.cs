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
    private Dictionary<string,AssetBundle> abDic = new Dictionary<string, AssetBundle>();
    public void LoadRes(string abname, string resName)
    {
        //加载AB包
        //获取依赖包相关信息
        //加载主包
        //加载主包中的关键配置文件 获取依赖包
        //加载依赖包
        //加载目标包
        //
        AssetBundle AB = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + abname);
        GameObject a = AB.LoadAsset<GameObject>(resName);
    } 
    IEnumerator LoadResource(string abname, string resName)
    {
        AssetBundleCreateRequest ascs = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/" + abname);
        yield return ascs;
        AssetBundleRequest abcs = ascs.assetBundle.LoadAssetAsync<Sprite>(resName);
        yield return abcs;
    }
}
