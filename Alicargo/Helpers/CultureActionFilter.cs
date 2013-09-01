﻿using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace Alicargo.Helpers
{
    internal sealed class CultureActionFilter : ActionFilterAttribute
	{
		private readonly Func<string> _getTwoLetterISOLanguageName;

		public CultureActionFilter(Func<string> getTwoLetterISOLanguageName)
		{
			_getTwoLetterISOLanguageName = getTwoLetterISOLanguageName;
		}

		private void SetCulture()
		{
			var languageName = _getTwoLetterISOLanguageName();

			var culture = CultureInfo.GetCultures(CultureTypes.NeutralCultures).First(x =>
				x.TwoLetterISOLanguageName.Equals(languageName,
					StringComparison.InvariantCultureIgnoreCase));

			// todo: 3.5 create ambient context
			Thread.CurrentThread.CurrentCulture = culture;
			Thread.CurrentThread.CurrentUICulture = culture;
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			SetCulture();
		}
	}
}