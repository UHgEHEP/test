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
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace Alicargo.Areas.Admin.Controllers
{
    public partial class BillController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public BillController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected BillController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Preview()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Preview);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public BillController Actions { get { return MVC.Admin.Bill; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Admin";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Bill";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Bill";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Preview = "Preview";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Preview = "Preview";
        }


        static readonly ActionParamsClass_Preview s_params_Preview = new ActionParamsClass_Preview();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Preview PreviewParams { get { return s_params_Preview; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Preview
        {
            public readonly string applicationId = "applicationId";
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
                public readonly string Preview = "Preview";
            }
            public readonly string Preview = "~/Areas/Admin/Views/Bill/Preview.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_BillController : Alicargo.Areas.Admin.Controllers.BillController
    {
        public T4MVC_BillController() : base(Dummy.Instance) { }

        [NonAction]
        partial void PreviewOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long applicationId);

        [NonAction]
        public override System.Web.Mvc.ActionResult Preview(long applicationId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Preview);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "applicationId", applicationId);
            PreviewOverride(callInfo, applicationId);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
