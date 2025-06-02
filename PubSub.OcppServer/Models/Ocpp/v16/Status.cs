using System.Text.Json.Serialization;

namespace PubSub.OcppServer.Models.Ocpp.v16
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OldStatus
    {
        Accepted, 
        Availsble,
        Blocked, 
        Charging,
        ConcurrentTx,
        DownloadFailed, Downloaded, Downloading,
        Expired, 
        Failed,
        Faulted,
        Finishing,
        Idle,
        InstallationFailed, Installed, Installing,
        Invalid,
        NotSupported,
        Occupied,
        Preparing,
        RebootRequired,
        Rejected, 
        Reserved,
        Scheduled,
        SuspendedEv, SuspendedEvse,
        Unavailable,
        Unknown, UnknownMessageId, UnknownVendorId,
        UnlockFailed, Unlocked,
        UploadFailed, Uploaded, Uploading,
        VersionMismatch
    };

}
