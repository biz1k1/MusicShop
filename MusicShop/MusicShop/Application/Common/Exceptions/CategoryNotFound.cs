namespace MusicShop.Application.Common.Errors
{
    public class CategoryNotFound:Exception
    {
        public override string Message => "Category was not found";
        public override string? StackTrace => base.StackTrace;
    }
}
