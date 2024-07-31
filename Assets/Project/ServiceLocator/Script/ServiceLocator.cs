using System;
using System.Collections.Generic;
using UnityEngine;

namespace hamster.Library
{
    public static class ServiceLocator
    {
        // 単一インスタンスを管理するディクショナリ
        private static Dictionary<Type, object> singleInstanceDic = new Dictionary<Type, object>();

        // 都度生成インスタンスを管理するディクショナリ
        private static Dictionary<Type, Type> factoryInstanceDic = new Dictionary<Type, Type>();

        /// <summary>
        /// 指定された型が登録されているかを確認する
        /// </summary>
        /// <typeparam name="T">確認する型名</typeparam>
        /// <returns>登録されている場合はtrue、そうでない場合はfalse</returns>
        public static bool IsRegistered<T>() where T : class
        {
            Type type = typeof(T);
            return singleInstanceDic.ContainsKey(type) || factoryInstanceDic.ContainsKey(type);
        }

        /// <summary>
        /// 単一インスタンスを登録する
        /// </summary>
        /// <typeparam name="T">登録する型名</typeparam>
        /// <param name="instance">登録するインスタンス</param>
        public static void Register<T>(T instance) where T : class
        {
            singleInstanceDic[typeof(T)] = instance;
        }

        /// <summary>
        /// 都度生成インスタンスを登録する
        /// </summary>
        /// <typeparam name="TRegisterClass">登録する型名</typeparam>
        /// <typeparam name="TInstance">登録するインスタンス</typeparam>
        public static void Register<TRegisterClass, TInstance>() where TRegisterClass : class
        {
            factoryInstanceDic[typeof(TRegisterClass)] = typeof(TInstance);
        }

        /// <summary>
        /// 登録されているインスタンスを単一または都度生成をしてインスタンスを返す
        /// </summary>
        /// <typeparam name="T">取得したい型名</typeparam>
        /// <returns>登録されているインスタンス</returns>
        public static T Resolve<T>() where T : class
        {
            T instance = default;
            Type type = typeof(T);

            // いずれかのディクショナリに登録されているか確認する
            if (singleInstanceDic.ContainsKey(type))
            {
                // 登録されているインスタンスを返す
                instance = singleInstanceDic[type] as T;
                return instance;
            }

            if (factoryInstanceDic.ContainsKey(type))
            {
                // 登録されているインスタンスを生成して返す
                instance = Activator.CreateInstance(factoryInstanceDic[type]) as T;
                return instance;
            }

            if (instance == null)
            {
                Debug.LogWarning($"{typeof(T).Name} はServiceLocatorに登録されていない型です。");
            }

            return instance;
        }
    }
}
