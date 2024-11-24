using System;
using System.Collections.Generic;
using UnityEngine;
public static class ServiceLocator
{
    // 単一インスタンスを管理するディクショナリ
    private static Dictionary<Type, object> singleInstanceDic = new Dictionary<Type, object>();

    // 都度生成インスタンスを管理するディクショナリ
    private static Dictionary<Type, Type> factoryInstanceDic = new Dictionary<Type, Type>();

    /// <summary>
    /// 指定された型が登録されているか確認する
    /// </summary>
    /// <typeparam name="TCheck">確認したい型名</typeparam>
    /// <returns>登録済みならtrue、存在しなければfalseが返ってくる</returns>
    public static bool IsRegistered<TCheck>()
    {
        Type type = typeof(TCheck);
        return singleInstanceDic.ContainsKey(type) || factoryInstanceDic.ContainsKey(type);
    }

    /// <summary>
    /// 単一インスタンスを登録する
    /// </summary>
    /// <typeparam name="T">登録する型名</typeparam>
    /// <param name="registerInstance">登録するインスタンス</param>
    public static void Register<T>(T registerInstance) where T : class
    {
        singleInstanceDic[typeof(T)] = registerInstance;
    }

    /// <summary>
    /// 都度生成インスタンスを登録する
    /// </summary>
    /// <typeparam name="TRegisterType">登録する型</typeparam>
    /// <typeparam name="TRegisterInstance">登録するインスタンス</typeparam>
    public static void Register<TRegisterType, TRegisterInstance>() where TRegisterType : class
    {
        factoryInstanceDic[typeof(TRegisterType)] = typeof(TRegisterInstance);
    }

    /// <summary>
    /// 登録されている単一または都度生成インスタンスを返す
    /// </summary>
    /// <typeparam name="TGetInstance">取得したいインスタンス</typeparam>
    public static TGetInstance Resolve<TGetInstance>() where TGetInstance : class
    {
        TGetInstance instance = default;
        Type type = typeof(TGetInstance);

        // 登録されていない場合はnullを返す
        if (IsRegistered<TGetInstance>() == false)
        {
            Debug.LogWarning($"{typeof(TGetInstance).Name} はServiceLocatorに登録されていない型です。");
            return instance;
        }

        // 登録されているインスタンスを返す
        if (singleInstanceDic.ContainsKey(type))
        {
            instance = singleInstanceDic[type] as TGetInstance;
        }

        // 登録されているインスタンスを生成して返す
        if (factoryInstanceDic.ContainsKey(type))
        {
            instance = Activator.CreateInstance(factoryInstanceDic[type]) as TGetInstance;
        }

        return instance;
    }

    /// <summary>
    /// 登録したインスタンスを解除する
    /// </summary>
    public static void Unregister<TUnregister>() where TUnregister : class
    {
        Type type = typeof(TUnregister);
        if (singleInstanceDic.ContainsKey(type))
        {
            singleInstanceDic.Remove(type);
        }

        if (factoryInstanceDic.ContainsKey(type))
        {
            factoryInstanceDic.Remove(type);
        }
    }
}
