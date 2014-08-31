// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
#pragma warning disable 1591, 3008, 3009
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
namespace Alicargo.Controllers.Calculation
{
    public partial class ClientCalculationController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected ClientCalculationController(Dummy d) { }

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
        public virtual System.Web.Mvc.ViewResult History()
        {
            return new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.History);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult List()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.List);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ClientCalculationController Actions { get { return MVC.ClientCalculation; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "ClientCalculation";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "ClientCalculation";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string History = "History";
            public readonly string List = "List";
            public readonly string Excel = "Excel";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string History = "History";
            public const string List = "List";
            public const string Excel = "Excel";
        }


        static readonly ActionParamsClass_History s_params_History = new ActionParamsClass_History();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_History HistoryParams { get { return s_params_History; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_History
        {
            public readonly string clientId = "clientId";
        }
        static readonly ActionParamsClass_List s_params_List = new ActionParamsClass_List();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_List ListParams { get { return s_params_List; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_List
        {
            public readonly string take = "take";
            public readonly string skip = "skip";
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
                public readonly string History = "History";
                public readonly string Index = "Index";
            }
            public readonly string History = "~/Views/ClientCalculation/History.cshtml";
            public readonly string Index = "~/Views/ClientCalculation/Index.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_ClientCalculationController : Alicargo.Controllers.Calculation.ClientCalculationController
    {
        public T4MVC_ClientCalculationController() : base(Dummy.Instance) { }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void HistoryOverride(T4MVC_System_Web_Mvc_ViewResult callInfo, long clientId);

        [NonAction]
        public override System.Web.Mvc.ViewResult History(long clientId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.History);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "clientId", clientId);
            HistoryOverride(callInfo, clientId);
            return callInfo;
        }

        [NonAction]
        partial void ListOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, int take, long skip);

        [NonAction]
        public override System.Web.Mvc.JsonResult List(int take, long skip)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.List);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "take", take);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "skip", skip);
            ListOverride(callInfo, take, skip);
            return callInfo;
        }

        [NonAction]
        partial void ExcelOverride(T4MVC_System_Web_Mvc_FileResult callInfo);

        [NonAction]
        public override System.Web.Mvc.FileResult Excel()
        {
            var callInfo = new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.Excel);
            ExcelOverride(callInfo);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009
