using System;
using System.Collections.Generic;
using UnityEngine;
public static class ServiceLocator
{
    // �P��C���X�^���X���Ǘ�����f�B�N�V���i��
    private static Dictionary<Type, object> singleInstanceDic = new Dictionary<Type, object>();

    // �s�x�����C���X�^���X���Ǘ�����f�B�N�V���i��
    private static Dictionary<Type, Type> factoryInstanceDic = new Dictionary<Type, Type>();

    /// <summary>
    /// �w�肳�ꂽ�^���o�^����Ă��邩�m�F����
    /// </summary>
    /// <typeparam name="TCheck">�m�F�������^��</typeparam>
    /// <returns>�o�^�ς݂Ȃ�true�A���݂��Ȃ����false���Ԃ��Ă���</returns>
    public static bool IsRegistered<TCheck>()
    {
        Type type = typeof(TCheck);
        return singleInstanceDic.ContainsKey(type) || factoryInstanceDic.ContainsKey(type);
    }

    /// <summary>
    /// �P��C���X�^���X��o�^����
    /// </summary>
    /// <typeparam name="T">�o�^����^��</typeparam>
    /// <param name="registerInstance">�o�^����C���X�^���X</param>
    public static void Register<T>(T registerInstance) where T : class
    {
        singleInstanceDic[typeof(T)] = registerInstance;
    }

    /// <summary>
    /// �s�x�����C���X�^���X��o�^����
    /// </summary>
    /// <typeparam name="TRegisterType">�o�^����^</typeparam>
    /// <typeparam name="TRegisterInstance">�o�^����C���X�^���X</typeparam>
    public static void Register<TRegisterType, TRegisterInstance>() where TRegisterType : class
    {
        factoryInstanceDic[typeof(TRegisterType)] = typeof(TRegisterInstance);
    }

    /// <summary>
    /// �o�^����Ă���P��܂��͓s�x�����C���X�^���X��Ԃ�
    /// </summary>
    /// <typeparam name="TGetInstance">�擾�������C���X�^���X</typeparam>
    public static TGetInstance Resolve<TGetInstance>() where TGetInstance : class
    {
        TGetInstance instance = default;
        Type type = typeof(TGetInstance);

        // �o�^����Ă��Ȃ��ꍇ��null��Ԃ�
        if (IsRegistered<TGetInstance>() == false)
        {
            Debug.LogWarning($"{typeof(TGetInstance).Name} ��ServiceLocator�ɓo�^����Ă��Ȃ��^�ł��B");
            return instance;
        }

        // �o�^����Ă���C���X�^���X��Ԃ�
        if (singleInstanceDic.ContainsKey(type))
        {
            instance = singleInstanceDic[type] as TGetInstance;
        }

        // �o�^����Ă���C���X�^���X�𐶐����ĕԂ�
        if (factoryInstanceDic.ContainsKey(type))
        {
            instance = Activator.CreateInstance(factoryInstanceDic[type]) as TGetInstance;
        }

        return instance;
    }

    /// <summary>
    /// �o�^�����C���X�^���X����������
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
