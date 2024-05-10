namespace MusicShop.Application.Common.Errors
{
    public class UserNotFound : Exception
    {
        public override string Message => "User was not found.";
    }
}
