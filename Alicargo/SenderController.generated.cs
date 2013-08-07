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
    public partial class SenderController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected SenderController(Dummy d) { }

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
        public virtual System.Web.Mvc.ViewResult ApplicationCreate()
        {
            return new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.ApplicationCreate);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ViewResult ApplicationEdit()
        {
            return new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.ApplicationEdit);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public SenderController Actions { get { return MVC.Sender; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Sender";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Sender";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string ApplicationCreate = "ApplicationCreate";
            public readonly string ApplicationEdit = "ApplicationEdit";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string ApplicationCreate = "ApplicationCreate";
            public const string ApplicationEdit = "ApplicationEdit";
        }


        static readonly ActionParamsClass_ApplicationCreate s_params_ApplicationCreate = new ActionParamsClass_ApplicationCreate();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ApplicationCreate ApplicationCreateParams { get { return s_params_ApplicationCreate; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ApplicationCreate
        {
            public readonly string id = "id";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_ApplicationEdit s_params_ApplicationEdit = new ActionParamsClass_ApplicationEdit();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ApplicationEdit ApplicationEditParams { get { return s_params_ApplicationEdit; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ApplicationEdit
        {
            public readonly string id = "id";
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
                public readonly string ApplicationCreate = "ApplicationCreate";
                public readonly string ApplicationEdit = "ApplicationEdit";
            }
            public readonly string ApplicationCreate = "~/Views/Sender/ApplicationCreate.cshtml";
            public readonly string ApplicationEdit = "~/Views/Sender/ApplicationEdit.cshtml";
            static readonly _EditorTemplatesClass s_EditorTemplates = new _EditorTemplatesClass();
            public _EditorTemplatesClass EditorTemplates { get { return s_EditorTemplates; } }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public partial class _EditorTemplatesClass
            {
                public readonly string ApplicationSenderEdit = "ApplicationSenderEdit";
            }
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_SenderController : Alicargo.Controllers.SenderController
    {
        public T4MVC_SenderController() : base(Dummy.Instance) { }

        partial void ApplicationCreateOverride(T4MVC_System_Web_Mvc_ViewResult callInfo, long id);

        public override System.Web.Mvc.ViewResult ApplicationCreate(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.ApplicationCreate);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ApplicationCreateOverride(callInfo, id);
            return callInfo;
        }

        partial void ApplicationEditOverride(T4MVC_System_Web_Mvc_ViewResult callInfo, long id);

        public override System.Web.Mvc.ViewResult ApplicationEdit(long id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.ApplicationEdit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ApplicationEditOverride(callInfo, id);
            return callInfo;
        }

        partial void ApplicationEditOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long id, Alicargo.ViewModels.Application.ApplicationSenderEdit model);

        public override System.Web.Mvc.ActionResult ApplicationEdit(long id, Alicargo.ViewModels.Application.ApplicationSenderEdit model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ApplicationEdit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ApplicationEditOverride(callInfo, id, model);
            return callInfo;
        }

        partial void ApplicationCreateOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, long id, Alicargo.ViewModels.Application.ApplicationSenderEdit model);

        public override System.Web.Mvc.ActionResult ApplicationCreate(long id, Alicargo.ViewModels.Application.ApplicationSenderEdit model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ApplicationCreate);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ApplicationCreateOverride(callInfo, id, model);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
