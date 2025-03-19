using System.Reflection;
using MUSYNCSaveDecode.Model;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Collections.Generic;
using System;
using Microsoft.VisualBasic.FileIO;

namespace MUSYNCSaveDecode;

public class Decode
{
    private readonly Assembly assembly;
    private readonly Type songSaveInfo;
    private readonly Object userMemoryObj;
    private readonly Type userMemoryType;
    private readonly UserMemory userMemory = UserMemory.Instance;
    public Decode(String savePath)
    {
        // 加载程序集
        assembly = Assembly.LoadFrom(Path.Combine("D:\\Program Files\\steam\\steamapps\\common\\MUSYNX\\", "MUSYNX_Data/Managed/Assembly-CSharp.dll"));
        songSaveInfo = assembly.GetType("SongSaveInfo")
            ?? throw new ArgumentException($"Type 'Assembly-CSharp.SongSaveInfo' not found.");

        // 读取文件内容
        string base64EncodedData = File.ReadAllText(savePath);

        // 解码Base64字符串为字节数组
        byte[] decodedBytes = Convert.FromBase64String(base64EncodedData);

        MemoryStream stream = new MemoryStream(decodedBytes);
        userMemoryObj = new BinaryFormatter().Deserialize(stream);
        userMemoryType = userMemoryObj.GetType();
    }

    // 通用方法：获取私有字段的值
    private object GetNonPublicField(string field, Type fieldType)
    {
        object fieldValue;
        try
        {
            FieldInfo fieldInfo = userMemoryType.GetField(field, BindingFlags.NonPublic | BindingFlags.Instance)
                ?? throw new ArgumentException($"Field '{field}' not found.");
            fieldValue = fieldInfo.GetValue(userMemoryObj)
                ?? throw new InvalidOperationException($"Field '{field}' is null.");
            if (!fieldType.IsInstanceOfType(fieldValue))
            {
                throw new InvalidOperationException($"Field '{field}' is not of type {fieldType.Name}.");
            }
        }
        catch
        {
            fieldValue = new();
        }
        return fieldValue;
    }

    // 特定类型的方法
    private int GetNonPublicIntField(string field) => (int)GetNonPublicField(field, typeof(int));
    private float GetNonPublicFloatField(string field) => (float)GetNonPublicField(field, typeof(float));
    private bool GetNonPublicBoolField(string field) => (bool)GetNonPublicField(field, typeof(bool));
    private String GetNonPublicStringField(string field) => (String)GetNonPublicField(field, typeof(String));
    private int[] GetNonPublicIntArrayField(string field) => (int[])GetNonPublicField(field, typeof(int[]));
    private string[] GetNonPublicStringArrayField(string field) => (string[])GetNonPublicField(field, typeof(string[]));
    private List<SongSaveInfo> GetNonPublicListField(string field)
    {
        FieldInfo songSaveInfoInfo = userMemoryType.GetField(field, BindingFlags.NonPublic | BindingFlags.Instance)
            ?? throw new ArgumentException($"Field '{field}' not found.");
        object saveInfoListValue = songSaveInfoInfo.GetValue(userMemoryObj)
            ?? throw new InvalidOperationException($"Field '{field}' is null.");
        Type songSaveInfoType = songSaveInfoInfo.FieldType;

        List<SongSaveInfo> songInfoList = new List<SongSaveInfo>();
        // 检查字段值是否为 List<SongSaveInfo>
        if (saveInfoListValue is System.Collections.IEnumerable enumerable)
        {
            foreach (var item in enumerable)
            {
                // 映射字段值到本项目的 SongSaveInfo 类
                SongSaveInfo songInfo = new SongSaveInfo
                {
                    SongId = (int)(songSaveInfoType.GetField("SongId", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(item) ?? 0),
                    SpeedStall = (int)(songSaveInfoType.GetField("SpeedStall", BindingFlags.Instance | BindingFlags.Public)?.GetValue(item) ?? 0),
                    SyncNumber = (int)(songSaveInfoType.GetField("SyncNumber", BindingFlags.Instance | BindingFlags.Public)?.GetValue(item) ?? 0),
                    UploadScore = (float)(songSaveInfoType.GetField("UploadScore", BindingFlags.Instance | BindingFlags.Public)?.GetValue(item) ?? 0.0f),
                    PlayCount = (int)(songSaveInfoType.GetField("PlayCount", BindingFlags.Instance | BindingFlags.Public)?.GetValue(item) ?? 0),
                    Isfav = (bool)(songSaveInfoType.GetField("Isfav", BindingFlags.Instance | BindingFlags.Public)?.GetValue(item) ?? false)
                };
                songInfoList.Add(songInfo);
            }
        }
        return songInfoList;
    }

    public void Function()
    {
        userMemory.version = GetNonPublicIntField("version");
        //userMemory.saveInfoList = (object)GetNonPublicField("saveInfoList", typeof(object));
        userMemory.saveInfoList = GetNonPublicListField("saveInfoList");
        userMemory.purchaseIds = GetNonPublicStringArrayField("purchaseIds");
        userMemory.crc = GetNonPublicIntField("crc");
        userMemory.songIndex = GetNonPublicIntField("songIndex");
        userMemory.isHard = GetNonPublicIntField("isHard");
        userMemory.buttonNumber = GetNonPublicIntField("buttonNumber");
        userMemory.sortNum = GetNonPublicIntField("sortNum");
        userMemory.missVibrate = GetNonPublicBoolField("missVibrate");
        userMemory.soundHelper = GetNonPublicIntField("soundHelper");
        userMemory.displayAdjustment = GetNonPublicIntField("displayAdjustment");
        userMemory.judgeCompensate = GetNonPublicIntField("judgeCompensate");
        userMemory.advSceneSettringString = GetNonPublicStringField("advSceneSettringString");
        userMemory.metronomeSquipment = GetNonPublicStringField("metronomeSquipment");
        userMemory.playTimeUIA = GetNonPublicIntField("playTimeUIA");
        userMemory.playTimeUIB = GetNonPublicIntField("playTimeUIB");
        userMemory.playTimeUIC = GetNonPublicIntField("playTimeUIC");
        userMemory.playTimeUID = GetNonPublicIntField("playTimeUID");
        userMemory.playTimeUIE = GetNonPublicIntField("playTimeUIE");
        userMemory.playTimeUIF = GetNonPublicIntField("playTimeUIF");
        userMemory.playTimeRankEX = GetNonPublicIntField("playTimeRankEX");
        userMemory.playTimeKnockEX = GetNonPublicIntField("playTimeKnockEX");
        userMemory.playTimeKnockNote = GetNonPublicIntField("playTimeKnockNote");
        userMemory.playVsync = GetNonPublicBoolField("playVsync");
        userMemory.buttonSetting4K = GetNonPublicIntArrayField("buttonSetting4K");
        userMemory.buttonSetting6K = GetNonPublicIntArrayField("buttonSetting6K");
        userMemory.hiddenUnlockSongs = GetNonPublicBoolField("hiddenUnlockSongs");
        userMemory.hideLeaderboardMini = GetNonPublicBoolField("hideLeaderboardMini");
        userMemory.playingSceneName = GetNonPublicStringField("playingSceneName");
        userMemory.selectSongName = GetNonPublicStringField("selectSongName");
        userMemory.sceneName = GetNonPublicStringField("sceneName");
        userMemory.busVolume = GetNonPublicFloatField("busVolume");
        userMemory.advSceneSettingString = GetNonPublicStringField("advSceneSettingString");
        userMemory.dropSpeed = GetNonPublicIntField("dropSpeed");
        UserMemory.isUseUserMemoryDropSpeed = (bool)((userMemoryType.GetField("isUseUserMemoryDropSpeed", BindingFlags.Public | BindingFlags.Static))?.GetValue(userMemoryObj) ?? false);
        userMemory.dropSpeedFloat = GetNonPublicFloatField("dropSpeedFloat");
        userMemory.isOpenVSync = GetNonPublicBoolField("isOpenVSync");
        userMemory.hadSaveFpsAndVSync = GetNonPublicBoolField("hadSaveFpsAndVSync");
        userMemory.fps = GetNonPublicIntField("fps");
    }
}