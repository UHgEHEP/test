﻿using System.Collections.Generic;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Enums;

namespace Alicargo.Contracts.Repositories
{
	public interface IApplicationFileRepository
	{
		IReadOnlyDictionary<long, string> GetNames(long applicationId, ApplicationFileType type);
		IReadOnlyDictionary<long, FileInfo[]> GetInfo(long[] applicationIds, ApplicationFileType type);
		FileHolder Get(long id);
		long Add(long applicationId, ApplicationFileType type, string fileName, byte[] bytes);
		void Delete(long id);
	}
}