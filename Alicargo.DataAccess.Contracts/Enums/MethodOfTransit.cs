﻿using Alicargo.Utilities.Localization;

namespace Alicargo.DataAccess.Contracts.Enums
{
	public enum MethodOfTransit
	{
		[DisplayNameLocalized(typeof(Resources.Enums), "Avia")] Avia = 0,

		[DisplayNameLocalized(typeof(Resources.Enums), "Auto")] Auto = 1
	}
}