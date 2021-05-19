//using DevTrends.MvcDonutCaching;
using Structure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Core.Logging;
using Umbraco.Web;
using Umbraco.Web.Models;
using Structure.Core.Utilities;

namespace Structure.Core.Controllers.Base
{
    public abstract class BaseSurfaceController : SurfaceController, IRenderMvcController
    {
        #region Render MVC

        /// <summary>
        /// Checks to make sure the physical view file exists on disk.
        /// </summary>
        /// <param name="template">
        /// The Umbraco template.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected bool EnsurePhysicalViewExists(string template)
        {
            var result = ViewEngines.Engines.FindView(ControllerContext, template, null);
            if (result.View == null)
            {
                Logger.Warn<RenderMvcController>("No physical template file was found for template " + template);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Returns an ActionResult based on the template name found in the route values and the given model.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>
        /// If the template found in the route values doesn't physically exist, then an empty ContentResult will be returned.
        /// </remarks>
        protected ActionResult CurrentTemplate<T>(T model)
        {
            var template = ControllerContext.RouteData.Values["action"].ToString();
            if (!EnsurePhysicalViewExists(template))
            {
                return Content("");
            }
            return View(template, model);
        }

        /// <summary>
        /// The default action to render the front-end view.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual ActionResult Index(ContentModel model)
        {
            return CurrentTemplate(model);
        }

        #endregion

        #region BaseModel

        /// <summary>
        /// Return the base model which needs to be used everywhere.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        protected T GetModel<T>()
            where T : BaseModel, new()
        {
            var model = new T();
            model.MenuItems = GetMenuItems();

            return model;
        }

        private IEnumerable<MenuItem> GetMenuItems()
        {
            return
            (
                from n in CurrentPage.TopPage().Children
                where n.HasProperty("menuTitle")
                && !n.Value<bool>("hideInMenu")
                select new MenuItem()
                {
                    Id = n.Id,
                    Title = n.Value<string>("menuTitle"),
                    Url = n.Url(),
                    ActiveClass = CurrentPage.Path.Contains(n.Id.ToString()) ? "active" : null
                }
            );
        }

        #endregion

        #region Override

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            //Log the exception.
            Umbraco.LogException(filterContext.Exception);

            //Clear the cache if an error occurs.
            //var cacheManager = new OutputCacheManager();
            //cacheManager.RemoveItems();

            //Show the view error.
            //filterContext.Result = View("Error");
            //filterContext.ExceptionHandled = true;
        }
        #endregion
    }
}
