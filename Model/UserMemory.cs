using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MUSYNCSaveDecode.Model;

public class UserMemory
{
    private static UserMemory? _instance;
    private static readonly Object _lock = new object();

    public int version;
    public List<SongSaveInfo> saveInfoList = new List<SongSaveInfo>();
    public string[] purchaseIds = Array.Empty<string>();
    public int crc;
    public long saveDate = DateTime.Now.Millisecond;
    public int songIndex = 1;
    public int isHard;
    public int buttonNumber = 4;
    public int sortNum;
    public bool missVibrate;
    public int soundHelper = 3;
    public int displayAdjustment;
    public int judgeCompensate;
    public string advSceneSettringString = "";
    public string metronomeSquipment = string.Empty;
    public int playTimeUIA;
    public int playTimeUIB;
    public int playTimeUIC;
    public int playTimeUID;
    public int playTimeUIE;
    public int playTimeUIF;
    public int playTimeRankEX;
    public int playTimeKnockEX;
    public int playTimeKnockNote;
    public bool playVsync = true;
    public int[]? buttonSetting4K;
    public int[]? buttonSetting6K;
    public bool hiddenUnlockSongs;
    public bool hideLeaderboardMini = true;
    public string playingSceneName = "";
    public string selectSongName = "luobi";
    public string sceneName = "SelectSongScene";
    public float busVolume;
    public string advSceneSettingString = "\n";
    public int dropSpeed = 8;
    public bool isUseUserMemoryDropSpeed = true;
    public float dropSpeedFloat;
    public bool isOpenVSync = true;
    public bool hadSaveFpsAndVSync;
    public int fps = 60;

    public int SongCout => (saveInfoList != null) ? saveInfoList.Count : 0;

    public static UserMemory Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock) { _instance ??= new UserMemory(); }
            }
            return _instance;
        }
    }

    public static int SaveVersion => 2;

    public static int AppVersion => 153;

    public int[] ButtonSetting4K
    {
        get
        {
            buttonSetting4K ??= new int[9] { 0, 0, 0, 28, 30, 34, 35, 0, 0 };
            return buttonSetting4K;
        }
    }

    public int[] ButtonSetting6K
    {
        get
        {
            buttonSetting6K ??= new int[9] { 0, 0, 43, 28, 30, 34, 35, 36, 0 };
            return buttonSetting6K;
        }
    }

    public string SelectSongName
    {
        get
        {
            if (string.IsNullOrEmpty(selectSongName)) { selectSongName = "\n"; }
            return selectSongName;
        }
    }

    public float BusVolume
    {
        get => 1f - busVolume;
        set => busVolume = Math.Min(Math.Max(1f - value, 0f), 1f);
    }

    public string AdvSceneSettingString
    {
        get
        {
            if (string.IsNullOrEmpty(advSceneSettingString)) { advSceneSettingString = "\n"; }
            return advSceneSettingString;
        }
    }

    public float DropSpeed
    {
        get
        {
            if (dropSpeedFloat == 0f) { dropSpeedFloat = (float)dropSpeed / 2f; }
            return dropSpeedFloat;
        }
    }

    public bool IsOpenVSync
    {
        get
        {
            if (!hadSaveFpsAndVSync)
            {
                hadSaveFpsAndVSync = true;
                isOpenVSync = true;
            }
            return isOpenVSync;
        }
    }

    public int Fps
    {
        get
        {
            if (fps != 60 && fps != 120) { fps = 60; }
            return fps;
        }
    }
}