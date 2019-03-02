using Sitecore.Pipelines;
using System.Web.Mvc;
using System.Web.Routing;

namespace Locksmith.Pipelines.Initialize
{
    public class RegisterWebApiRoutes
    {
        public void Process(PipelineArgs args)
        {
            RouteTable.Routes.MapRoute("Feature.Locksmith.Unlock.Api", "api/locksmith/{action}", new { controller = "locksmith" });
        }
    }
}