using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gRPC.Console.Client.Handlers
{
    /// <summary>
    /// A delegating handler that adds a subdirectory to the URI of gRPC requests.
    /// </summary>
    public class SubdirectoryHandler : DelegatingHandler
    {
        private readonly string _subdirectory;
        public SubdirectoryHandler(HttpMessageHandler innerHandler, string subdirectory)
                    : base(innerHandler)
        {
            _subdirectory = subdirectory;
        }
        protected override Task<HttpResponseMessage> SendAsync(
                    HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var old = request.RequestUri;
            var url = $"{old.Scheme}://{old.Host}:{old.Port}";
            url += $"{_subdirectory}{request.RequestUri.AbsolutePath}";
            request.RequestUri = new Uri(url, UriKind.Absolute);
            return base.SendAsync(request, cancellationToken);
        }
    }
}
