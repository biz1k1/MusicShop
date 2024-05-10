namespace MusicShop.Application.Common.Errors
{
    public class InvalidRoleUser:Exception
    {
        public override string Message => "A role doesn't exist.";
    }
}
