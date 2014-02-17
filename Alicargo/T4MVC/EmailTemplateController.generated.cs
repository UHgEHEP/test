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
    public partial class EmailTemplateController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected EmailTemplateController(Dummy d) { }

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
        public virtual System.Web.Mvc.ViewResult Edit()
        {
            return new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Edit);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public EmailTemplateController Actions { get { return MVC.EmailTemplate; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "EmailTemplate";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "EmailTemplate";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Edit = "Edit";
            public readonly string Help = "Help";
            public readonly string Index = "Index";
            public readonly string List = "List";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Edit = "Edit";
            public const string Help = "Help";
            public const string Index = "Index";
            public const string List = "List";
        }


        static readonly ActionParamsClass_Edit s_params_Edit = new ActionParamsClass_Edit();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Edit EditParams { get { return s_params_Edit; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Edit
        {
            public readonly string id = "id";
            public readonly string lang = "lang";
            public readonly string model = "model";
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
                public readonly string Edit = "Edit";
                public readonly string Help = "Help";
                public readonly string Index = "Index";
            }
            public readonly string Edit = "~/Views/EmailTemplate/Edit.cshtml";
            public readonly string Help = "~/Views/EmailTemplate/Help.cshtml";
            public readonly string Index = "~/Views/EmailTemplate/Index.cshtml";
            static readonly _EditorTemplatesClass s_EditorTemplates = new _EditorTemplatesClass();
            public _EditorTemplatesClass EditorTemplates { get { return s_EditorTemplates; } }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public partial class _EditorTemplatesClass
            {
                public readonly string EventTemplateModel = "EventTemplateModel";
            }
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_EmailTemplateController : Alicargo.Controllers.EmailTemplateController
    {
        public T4MVC_EmailTemplateController() : base(Dummy.Instance) { }

        [NonAction]
        partial void EditOverride(T4MVC_System_Web_Mvc_ViewResult callInfo, Alicargo.DataAccess.Contracts.Enums.EventType id, string lang);

        [NonAction]
        public override System.Web.Mvc.ViewResult Edit(Alicargo.DataAccess.Contracts.Enums.EventType id, string lang)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Edit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "lang", lang);
            EditOverride(callInfo, id, lang);
            return callInfo;
        }

        [NonAction]
        partial void EditOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Alicargo.ViewModels.EmailTemplate.EventTemplateModel model);

        [NonAction]
        public override System.Web.Mvc.ActionResult Edit(Alicargo.ViewModels.EmailTemplate.EventTemplateModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            EditOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void HelpOverride(T4MVC_System_Web_Mvc_ViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ViewResult Help()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Help);
            HelpOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ViewResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ViewResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ListOverride(T4MVC_System_Web_Mvc_JsonResult callInfo);

        [NonAction]
        public override System.Web.Mvc.JsonResult List()
        {
            var callInfo = new T4MVC_System_Web_Mvc_JsonResult(Area, Name, ActionNames.List);
            ListOverride(callInfo);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
