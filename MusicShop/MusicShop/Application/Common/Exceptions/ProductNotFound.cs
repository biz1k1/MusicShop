namespace MusicShop.Application.Common.Errors
{
    public class ProductNotFound:Exception
    {
        public override string Message => "Product was not found.";
    }
}
