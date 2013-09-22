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
    public partial class CalculationController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected CalculationController(Dummy d) { }

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

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult List()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.List);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.JsonResult Row()
        {
            return new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.Row);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.HttpStatusCodeResult SetTariffPerKg()
        {
            return new T4MVC_System_Web_Mvc_HttpStatusCodeResult(Area, Name, ActionNames.SetTariffPerKg);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.HttpStatusCodeResult SetScotchCostEdited()
        {
            return new T4MVC_System_Web_Mvc_HttpStatusCodeResult(Area, Name, ActionNames.SetScotchCostEdited);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.HttpStatusCodeResult SetFactureCostEdited()
        {
            return new T4MVC_System_Web_Mvc_HttpStatusCodeResult(Area, Name, ActionNames.SetFactureCostEdited);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.HttpStatusCodeResult SetWithdrawCostEdited()
        {
            return new T4MVC_System_Web_Mvc_HttpStatusCodeResult(Area, Name, ActionNames.SetWithdrawCostEdited);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.HttpStatusCodeResult SetAdditionalCost()
        {
            return new T4MVC_System_Web_Mvc_HttpStatusCodeResult(Area, Name, ActionNames.SetAdditionalCost);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public CalculationController Actions { get { return MVC.Calculation; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Calculation";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Calculation";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string List = "List";
            public readonly string Row = "Row";
            public readonly string SetTariffPerKg = "SetTariffPerKg";
            public readonly string SetScotchCostEdited = "SetScotchCostEdited";
            public readonly string SetFactureCostEdited = "SetFactureCostEdited";
            public readonly string SetWithdrawCostEdited = "SetWithdrawCostEdited";
            public readonly string SetAdditionalCost = "SetAdditionalCost";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string List = "List";
            public const string Row = "Row";
            public const string SetTariffPerKg = "SetTariffPerKg";
            public const string SetScotchCostEdited = "SetScotchCostEdited";
            public const string SetFactureCostEdited = "SetFactureCostEdited";
            public const string SetWithdrawCostEdited = "SetWithdrawCostEdited";
            public const string SetAdditionalCost = "SetAdditionalCost";
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
        static readonly ActionParamsClass_Row s_params_Row = new ActionParamsClass_Row();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Row RowParams { get { return s_params_Row; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Row
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_SetTariffPerKg s_params_SetTariffPerKg = new ActionParamsClass_SetTariffPerKg();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SetTariffPerKg SetTariffPerKgParams { get { return s_params_SetTariffPerKg; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SetTariffPerKg
        {
            public readonly string id = "id";
            public readonly string tariffPerKg = "tariffPerKg";
        }
        static readonly ActionParamsClass_SetScotchCostEdited s_params_SetScotchCostEdited = new ActionParamsClass_SetScotchCostEdited();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SetScotchCostEdited SetScotchCostEditedParams { get { return s_params_SetScotchCostEdited; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SetScotchCostEdited
        {
            public readonly string id = "id";
            public readonly string scotchCost = "scotchCost";
        }
        static readonly ActionParamsClass_SetFactureCostEdited s_params_SetFactureCostEdited = new ActionParamsClass_SetFactureCostEdited();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SetFactureCostEdited SetFactureCostEditedParams { get { return s_params_SetFactureCostEdited; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SetFactureCostEdited
        {
            public readonly string id = "id";
            public readonly string factureCost = "factureCost";
        }
        static readonly ActionParamsClass_SetWithdrawCostEdited s_params_SetWithdrawCostEdited = new ActionParamsClass_SetWithdrawCostEdited();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SetWithdrawCostEdited SetWithdrawCostEditedParams { get { return s_params_SetWithdrawCostEdited; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SetWithdrawCostEdited
        {
            public readonly string id = "id";
            public readonly string withdrawCost = "withdrawCost";
        }
        static readonly ActionParamsClass_SetAdditionalCost s_params_SetAdditionalCost = new ActionParamsClass_SetAdditionalCost();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SetAdditionalCost SetAdditionalCostParams { get { return s_params_SetAdditionalCost; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SetAdditionalCost
        {
            public readonly string awbId = "awbId";
            public readonly string additionalCost = "additionalCost";
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
                public readonly string Details = "Details";
                public readonly string Index = "Index";
            }
            public readonly string Details = "~/Views/Calculation/Details.cshtml";
            public readonly string Index = "~/Views/Calculation/Index.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_CalculationController : Alicargo.Controllers.CalculationController
    {
        public T4MVC_CalculationController() : base(Dummy.Instance) { }

        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        partial void ListOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, int take, long skip);

        public override System.Web.Mvc.JsonResult List(int take, long skip)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.List);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "take", take);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "skip", skip);
            ListOverride(callInfo, take, skip);
            return callInfo;
        }

        partial void RowOverride(T4MVC_System_Web_Mvc_JsonResult callInfo, long id);

        public override System.Web.Mvc.JsonResult Row(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.Row);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            RowOverride(callInfo, id);
            return callInfo;
        }

        partial void SetTariffPerKgOverride(T4MVC_System_Web_Mvc_HttpStatusCodeResult callInfo, long id, decimal? tariffPerKg);

        public override System.Web.Mvc.HttpStatusCodeResult SetTariffPerKg(long id, decimal? tariffPerKg)
        {
            var callInfo = new T4MVC_System_Web_Mvc_HttpStatusCodeResult(Area, Name, ActionNames.SetTariffPerKg);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "tariffPerKg", tariffPerKg);
            SetTariffPerKgOverride(callInfo, id, tariffPerKg);
            return callInfo;
        }

        partial void SetScotchCostEditedOverride(T4MVC_System_Web_Mvc_HttpStatusCodeResult callInfo, long id, decimal? scotchCost);

        public override System.Web.Mvc.HttpStatusCodeResult SetScotchCostEdited(long id, decimal? scotchCost)
        {
            var callInfo = new T4MVC_System_Web_Mvc_HttpStatusCodeResult(Area, Name, ActionNames.SetScotchCostEdited);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "scotchCost", scotchCost);
            SetScotchCostEditedOverride(callInfo, id, scotchCost);
            return callInfo;
        }

        partial void SetFactureCostEditedOverride(T4MVC_System_Web_Mvc_HttpStatusCodeResult callInfo, long id, decimal? factureCost);

        public override System.Web.Mvc.HttpStatusCodeResult SetFactureCostEdited(long id, decimal? factureCost)
        {
            var callInfo = new T4MVC_System_Web_Mvc_HttpStatusCodeResult(Area, Name, ActionNames.SetFactureCostEdited);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "factureCost", factureCost);
            SetFactureCostEditedOverride(callInfo, id, factureCost);
            return callInfo;
        }

        partial void SetWithdrawCostEditedOverride(T4MVC_System_Web_Mvc_HttpStatusCodeResult callInfo, long id, decimal? withdrawCost);

        public override System.Web.Mvc.HttpStatusCodeResult SetWithdrawCostEdited(long id, decimal? withdrawCost)
        {
            var callInfo = new T4MVC_System_Web_Mvc_HttpStatusCodeResult(Area, Name, ActionNames.SetWithdrawCostEdited);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "withdrawCost", withdrawCost);
            SetWithdrawCostEditedOverride(callInfo, id, withdrawCost);
            return callInfo;
        }

        partial void SetAdditionalCostOverride(T4MVC_System_Web_Mvc_HttpStatusCodeResult callInfo, long awbId, decimal? additionalCost);

        public override System.Web.Mvc.HttpStatusCodeResult SetAdditionalCost(long awbId, decimal? additionalCost)
        {
            var callInfo = new T4MVC_System_Web_Mvc_HttpStatusCodeResult(Area, Name, ActionNames.SetAdditionalCost);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "awbId", awbId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "additionalCost", additionalCost);
            SetAdditionalCostOverride(callInfo, awbId, additionalCost);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
