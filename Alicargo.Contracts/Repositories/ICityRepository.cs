﻿using Alicargo.Contracts.Contracts;

namespace Alicargo.Contracts.Repositories
{
	public interface ICityRepository
	{
		CityData[] All(string language);
		long Add(string englishName, string russianName, int position);
		void Update(long id, string englishName, string russianName, int position);
	}
}