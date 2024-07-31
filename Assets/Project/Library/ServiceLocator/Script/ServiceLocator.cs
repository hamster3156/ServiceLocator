using System;
using System.Collections.Generic;
using UnityEngine;

namespace hamster.Library
{
    public static class ServiceLocator
    {
        // �P��C���X�^���X���Ǘ�����f�B�N�V���i��
        private static Dictionary<Type, object> singleInstanceDic = new Dictionary<Type, object>();

        // �s�x�����C���X�^���X���Ǘ�����f�B�N�V���i��
        private static Dictionary<Type, Type> factoryInstanceDic = new Dictionary<Type, Type>();

        /// <summary>
        /// �w�肳�ꂽ�^���o�^����Ă��邩���m�F����
        /// </summary>
        /// <typeparam name="T">�m�F����^��</typeparam>
        /// <returns>�o�^����Ă���ꍇ��true�A�����łȂ��ꍇ��false</returns>
        public static bool IsRegistered<T>() where T : class
        {
            Type type = typeof(T);
            return singleInstanceDic.ContainsKey(type) || factoryInstanceDic.ContainsKey(type);
        }

        /// <summary>
        /// �P��C���X�^���X��o�^����
        /// </summary>
        /// <typeparam name="T">�o�^����^��</typeparam>
        /// <param name="instance">�o�^����C���X�^���X</param>
        public static void Register<T>(T instance) where T : class
        {
            singleInstanceDic[typeof(T)] = instance;
        }

        /// <summary>
        /// �s�x�����C���X�^���X��o�^����
        /// </summary>
        /// <typeparam name="TRegisterClass">�o�^����^��</typeparam>
        /// <typeparam name="TInstance">�o�^����C���X�^���X</typeparam>
        public static void Register<TRegisterClass, TInstance>() where TRegisterClass : class
        {
            factoryInstanceDic[typeof(TRegisterClass)] = typeof(TInstance);
        }

        /// <summary>
        /// �o�^����Ă���C���X�^���X��P��܂��͓s�x���������ăC���X�^���X��Ԃ�
        /// </summary>
        /// <typeparam name="T">�擾�������^��</typeparam>
        /// <returns>�o�^����Ă���C���X�^���X</returns>
        public static T Resolve<T>() where T : class
        {
            T instance = default;
            Type type = typeof(T);

            // �����ꂩ�̃f�B�N�V���i���ɓo�^����Ă��邩�m�F����
            if (singleInstanceDic.ContainsKey(type))
            {
                // �o�^����Ă���C���X�^���X��Ԃ�
                instance = singleInstanceDic[type] as T;
                return instance;
            }

            if (factoryInstanceDic.ContainsKey(type))
            {
                // �o�^����Ă���C���X�^���X�𐶐����ĕԂ�
                instance = Activator.CreateInstance(factoryInstanceDic[type]) as T;
                return instance;
            }

            if (instance == null)
            {
                Debug.LogWarning($"{typeof(T).Name} ��ServiceLocator�ɓo�^����Ă��Ȃ��^�ł��B");
            }

            return instance;
        }
    }
}
