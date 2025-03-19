using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUSYNCSaveDecode.Model;

public class SongSaveInfo
{
    public bool isUploadCount;

    public int SongId { get; set; }

    public int SpeedStall { get; set; }

    public int SyncNumber { get; set; }

    public float UploadScore { get; set; }

    public int PlayCount { get; set; }

    public bool Isfav { get; set; }

    public int CrcInt => SongId + SyncNumber + PlayCount;

    public SongSaveInfo()
    {
        SpeedStall = 8;
        SyncNumber = 0;
        UploadScore = 0f;
        PlayCount = 0;
        Isfav = false;
    }

    public SongSaveInfo(int songId, int defaultSpeedStall)
    {
        SongId = songId;
        SpeedStall = defaultSpeedStall;
        SyncNumber = 0;
        UploadScore = 0f;
        PlayCount = 0;
        Isfav = false;
    }

    public SongSaveInfo(SongSaveInfo songSaveInfo)
    {
        SongId = songSaveInfo.SongId;
        SpeedStall = songSaveInfo.SpeedStall;
        SyncNumber = songSaveInfo.SyncNumber;
        UploadScore = songSaveInfo.UploadScore;
        PlayCount = songSaveInfo.PlayCount;
        isUploadCount = songSaveInfo.isUploadCount;
        Isfav = songSaveInfo.Isfav;
    }
}