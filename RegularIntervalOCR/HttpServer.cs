using Grapevine.Interfaces.Server;
using Grapevine.Server;
using Grapevine.Server.Attributes;
using Grapevine.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using NLog.Extensions.Logging;

namespace RegularIntervalOCR
{
    [RestResource]
    public class TestResource
    {

        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/")]
        public IHttpContext Root(IHttpContext context)
        {
            var filepath = Path.Combine(context.Server.PublicFolder.FolderPath,
                                        context.Server.PublicFolder.IndexFileName);

            var lastModified = File.GetLastWriteTimeUtc(filepath).ToString("R");
            context.Response.AddHeader("Last-Modified", lastModified);

            if (context.Request.Headers.AllKeys.Contains("If-Modified-Since"))
            {
                if (context.Request.Headers["If-Modified-Since"].Equals(lastModified))
                {
                    context.Response.SendResponse(HttpStatusCode.NotModified);
                    return context;
                }
            }

            context.Response.ContentType = ContentType.DEFAULT.FromExtension(filepath);
            context.Response.SendResponse(new FileStream(filepath, FileMode.Open));

            return context;
        }
    }
}
