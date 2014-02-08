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
namespace Alicargo.Controllers.Awb
{
    public partial class AwbFileController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected AwbFileController(Dummy d) { }

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
        public virtual System.Web.Mvc.FileResult AWB()
        {
            return new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.AWB);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.FileResult GTDAdditional()
        {
            return new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.GTDAdditional);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.FileResult GTD()
        {
            return new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.GTD);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.FileResult Invoice()
        {
            return new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.Invoice);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.FileResult Packing()
        {
            return new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.Packing);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.FileResult Draw()
        {
            return new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.Draw);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public AwbFileController Actions { get { return MVC.AwbFile; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "AwbFile";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "AwbFile";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string AWB = "AWB";
            public readonly string GTDAdditional = "GTDAdditional";
            public readonly string GTD = "GTD";
            public readonly string Invoice = "Invoice";
            public readonly string Packing = "Packing";
            public readonly string Draw = "Draw";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string AWB = "AWB";
            public const string GTDAdditional = "GTDAdditional";
            public const string GTD = "GTD";
            public const string Invoice = "Invoice";
            public const string Packing = "Packing";
            public const string Draw = "Draw";
        }


        static readonly ActionParamsClass_AWB s_params_AWB = new ActionParamsClass_AWB();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AWB AWBParams { get { return s_params_AWB; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AWB
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_GTDAdditional s_params_GTDAdditional = new ActionParamsClass_GTDAdditional();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GTDAdditional GTDAdditionalParams { get { return s_params_GTDAdditional; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GTDAdditional
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_GTD s_params_GTD = new ActionParamsClass_GTD();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GTD GTDParams { get { return s_params_GTD; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GTD
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_Invoice s_params_Invoice = new ActionParamsClass_Invoice();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Invoice InvoiceParams { get { return s_params_Invoice; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Invoice
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_Packing s_params_Packing = new ActionParamsClass_Packing();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Packing PackingParams { get { return s_params_Packing; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Packing
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_Draw s_params_Draw = new ActionParamsClass_Draw();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Draw DrawParams { get { return s_params_Draw; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Draw
        {
            public readonly string id = "id";
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
            }
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_AwbFileController : Alicargo.Controllers.Awb.AwbFileController
    {
        public T4MVC_AwbFileController() : base(Dummy.Instance) { }

        partial void AWBOverride(T4MVC_System_Web_Mvc_FileResult callInfo, long id);

        public override System.Web.Mvc.FileResult AWB(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.AWB);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            AWBOverride(callInfo, id);
            return callInfo;
        }

        partial void GTDAdditionalOverride(T4MVC_System_Web_Mvc_FileResult callInfo, long id);

        public override System.Web.Mvc.FileResult GTDAdditional(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.GTDAdditional);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            GTDAdditionalOverride(callInfo, id);
            return callInfo;
        }

        partial void GTDOverride(T4MVC_System_Web_Mvc_FileResult callInfo, long id);

        public override System.Web.Mvc.FileResult GTD(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.GTD);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            GTDOverride(callInfo, id);
            return callInfo;
        }

        partial void InvoiceOverride(T4MVC_System_Web_Mvc_FileResult callInfo, long id);

        public override System.Web.Mvc.FileResult Invoice(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.Invoice);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            InvoiceOverride(callInfo, id);
            return callInfo;
        }

        partial void PackingOverride(T4MVC_System_Web_Mvc_FileResult callInfo, long id);

        public override System.Web.Mvc.FileResult Packing(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.Packing);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            PackingOverride(callInfo, id);
            return callInfo;
        }

        partial void DrawOverride(T4MVC_System_Web_Mvc_FileResult callInfo, long id);

        public override System.Web.Mvc.FileResult Draw(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_FileResult(Area, Name, ActionNames.Draw);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            DrawOverride(callInfo, id);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
