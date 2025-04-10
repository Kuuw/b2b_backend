using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace PL.Helpers
{
    public class StreamInputFormatter : InputFormatter
    {
        public StreamInputFormatter()
          => SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("image/jpeg"));

        protected override bool CanReadType(Type ArgType) => ArgType == typeof(Stream);

        public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext ArgContext)
          => InputFormatterResult.SuccessAsync(ArgContext.HttpContext.Request.Body);
    }
}
