﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Alicargo.Contracts.Helpers;
using Alicargo.DataAccess.DbContext;

namespace Alicargo.DataAccess.Helpers
{
	internal sealed class ApplicationRepositoryOrderer : IApplicationRepositoryOrderer
	{
		public IQueryable<Application> Order(IQueryable<Application> applications, IEnumerable<Order> orders)
		{
			if (orders == null)
			{
				return applications;
			}

			var isFirst = true;

			foreach (var order in orders)
			{
				applications = Order(applications, order.OrderType, order.Desc, isFirst);

				isFirst = false;
			}
			return applications;
		}

		private static IQueryable<Application> Order(IQueryable<Application> applications,
			OrderType field, bool desc, bool isFirst)
		{
			switch (field)
			{
				case OrderType.Id:
					applications = ById(applications, desc, isFirst);
					break;

				case OrderType.AirWaybill:
					applications = ByAirWaybillBill(applications, desc, isFirst);
					break;

				case OrderType.State:
					applications = ByState(applications, desc, isFirst);
					break;

				case OrderType.LegalEntity:
					applications = ByLegalEntity(applications, desc, isFirst);
					break;

				default:
					throw new ArgumentOutOfRangeException("field");
			}
			return applications;
		}

		// todo: 3. test
		private static IQueryable<Application> ById(IQueryable<Application> applications, bool desc, bool isFirst)
		{
			return Order(applications, desc, isFirst, x => x.Id);
		}

		// todo: 3. test
		private static IQueryable<Application> ByAirWaybillBill(IQueryable<Application> applications, bool desc, bool isFirst)
		{
			var ordered = Order(applications, desc, isFirst, x => x.AirWaybillId.HasValue);

			if (desc) // todo: 3. test for implicit direction
			{
				return ordered.ThenBy(x => x.AirWaybill.CreationTimestamp);
			}

			return ordered.ThenByDescending(x => x.AirWaybill.CreationTimestamp);
		}

		private static IQueryable<Application> ByLegalEntity(IQueryable<Application> applications, bool desc, bool isFirst)
		{
			return Order(applications, desc, isFirst, a => a.Client.LegalEntity);
		}

		private static IQueryable<Application> ByState(IQueryable<Application> applications, bool desc, bool isFirst)
		{
			return Order(applications, desc, isFirst, a => a.State.Name);
		}

		private static IOrderedQueryable<Application> Order<T>(
			IQueryable<Application> applications, bool desc, bool isFirst,
			Expression<Func<Application, T>> expression)
		{
			if (isFirst)
			{
				return desc
					? applications.OrderByDescending(expression)
					: applications.OrderBy(expression);
			}

			var ordered = (IOrderedQueryable<Application>)applications;
			return desc
				? ordered.ThenByDescending(expression)
				: ordered.ThenBy(expression);
		}
	}
}
