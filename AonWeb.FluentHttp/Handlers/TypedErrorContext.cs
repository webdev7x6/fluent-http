using System.Net;
using System.Net.Http;
using AonWeb.FluentHttp.Helpers;

namespace AonWeb.FluentHttp.Handlers
{
    public class TypedErrorContext<TError> : TypedErrorContext
    {
        public TypedErrorContext(ITypedBuilderContext context, HttpRequestMessage request, HttpResponseMessage response, TError error)
            : base(context, request, response, error)
        { }

        public TypedErrorContext(TypedErrorContext context)
            : base(context)
        { }

        public TError Error => ObjectHelpers.CheckType<TError>(ErrorInternal, SuppressTypeMismatchExceptions);
    }

    public abstract class TypedErrorContext : TypedHandlerContext
    {
        private readonly Modifiable<bool> _errorHandled;

        protected TypedErrorContext(ITypedBuilderContext context, HttpRequestMessage request, HttpResponseMessage response, object error)
            : base(context, request)
        {
            Response = response;
            ErrorInternal = error;
            _errorHandled = new Modifiable<bool>(false);
        }

        protected TypedErrorContext(TypedErrorContext context)
            : base(context)
        {
            Response = context.Response;
            ErrorInternal = context.ErrorInternal;
            _errorHandled = context._errorHandled;
        }

        public HttpResponseMessage Response { get; }
        public HttpStatusCode StatusCode => Response.StatusCode;

        public bool ErrorHandled
        {
            get { return _errorHandled.Value; }
            set {  _errorHandled.Value = value; }
        }

        protected object ErrorInternal { get; }

        public override Modifiable GetHandlerResult()
        {
            return _errorHandled.ToResult();
        }
    }
}