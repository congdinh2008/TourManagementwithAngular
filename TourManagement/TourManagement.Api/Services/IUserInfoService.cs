namespace TourManagement.Api.Services
{
    public interface IUserInfoService
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string UserId { get; set; }
    }
}