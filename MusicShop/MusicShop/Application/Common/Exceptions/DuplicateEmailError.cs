using System.Net;
using FluentResults;

namespace MusicShop.Application.Common.Errors
{
    public class DuplicateEmailError : Exception
    {
        public override string Message => "API error: Email already exist.";
    }
}
