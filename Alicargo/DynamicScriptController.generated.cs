// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace Alicargo.Controllers
{
    public partial class DynamicScriptController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public DynamicScriptController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected DynamicScriptController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }


        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public DynamicScriptController Actions { get { return MVC.DynamicScript; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "DynamicScript";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "DynamicScript";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Roles = "Roles";
            public readonly string Urls = "Urls";
            public readonly string Localization = "Localization";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Roles = "Roles";
            public const string Urls = "Urls";
            public const string Localization = "Localization";
        }


        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string Localization = "Localization";
                public readonly string Roles = "Roles";
                public readonly string Urls = "Urls";
            }
            public readonly string Localization = "~/Views/DynamicScript/Localization.cshtml";
            public readonly string Roles = "~/Views/DynamicScript/Roles.cshtml";
            public readonly string Urls = "~/Views/DynamicScript/Urls.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_DynamicScriptController : Alicargo.Controllers.DynamicScriptController
    {
        public T4MVC_DynamicScriptController() : base(Dummy.Instance) { }

        partial void RolesOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo);

        public override System.Web.Mvc.PartialViewResult Roles()
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.Roles);
            RolesOverride(callInfo);
            return callInfo;
        }

        partial void UrlsOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo);

        public override System.Web.Mvc.PartialViewResult Urls()
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.Urls);
            UrlsOverride(callInfo);
            return callInfo;
        }

        partial void LocalizationOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo);

        public override System.Web.Mvc.PartialViewResult Localization()
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.Localization);
            LocalizationOverride(callInfo);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
