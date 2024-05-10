namespace MusicShop.Application.Common.Errors
{
    public class CategoryReference:Exception
    {
        public override string Message => "A category can't refer to itself.";
    }
}
