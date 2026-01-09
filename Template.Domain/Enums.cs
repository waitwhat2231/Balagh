namespace Template.Domain;

public enum ComplaintStatus
{
    New,
    InProcessing,
    Done,
    Declined
}

public enum ChangeType
{
    AddNote,
    RequestMoreInformation,
    UpdateDescription,
    UpdateLocation,
    GovermentalEntityChange,
    UpdateStatus,
    AddFile,
    DeleteFile
}

public enum NotificationType
{
    ExtraInformationRequest,
    ComplaintCreated
}
