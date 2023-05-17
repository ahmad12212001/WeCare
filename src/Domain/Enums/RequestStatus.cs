namespace WeCare.Domain.Enums;
public enum RequestStatus
{
    Created = 1,
    Accepted = 2,
    AcceptedByVolunteer = 4,
    ReAssigning = 8,
    Done = 16
}
