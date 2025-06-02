namespace PubSub.OcppServer.Models.Ocpp.v201;

public enum MessageTriggerEnum
{
    BootNotification, LogStatusNotification, FirmwareStatusNotification, Heartbeat, MeterValues,
    SignChargingStationCertificate, SignV2GCertificate, StatusNotification, TransactionEvent,
    SignCombinedCertificate, PublishFirmwareStatusNotification
}