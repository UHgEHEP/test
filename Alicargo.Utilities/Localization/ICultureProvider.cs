﻿using System;

namespace Alicargo.Utilities.Localization
{
	public interface ICultureProvider
	{
		void Set(Func<string> language);
		string GetLanguage();
	}
}