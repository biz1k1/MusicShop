namespace MusicShop.Application.Common.Errors
{
    public class InvalidUserData : Exception
    {
        public override string Message => "Login or password is incorrect or does not exist.";
    }
}
