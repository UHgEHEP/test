﻿var Alicargo = (function($a) {
	var $l = $a.Localization;
	var $u = $a.Urls;
	var $r = $a.Roles;

	$a.Calculation = (function($c) {
		function dataSource(data) {
			var editTariffPerKg = $r.IsAdmin;
			var editScotchCost = $r.IsAdmin || $r.IsSender;
			var editFactureCost = $r.IsAdmin || $r.IsSender;
			var editWithdrawCost = $r.IsAdmin || $r.IsSender;
			var editTransitCost = $r.IsAdmin || $r.IsForwarder;

			return {
				schema: {
					model: {
						id: "ApplicationId",
						fields: {
							"ClientNic": { type: "string", editable: false },
							"DisplayNumber": { type: "string", editable: false },
							"Factory": { type: "string", editable: false },
							"Mark": { type: "string", editable: false },
							"Count": { type: "number", editable: false },
							"Weigth": { type: "number", editable: false },
							"Invoice": { type: "string", editable: false },
							"Value": { type: "number", editable: false },
							"TariffPerKg": { type: "number", editable: editTariffPerKg },
							"TotalTariffCost": { type: "number", editable: false },
							"ScotchCost": { type: "number", editable: editScotchCost },
							"FactureCost": { type: "number", editable: editFactureCost },
							"WithdrawCost": { type: "number", editable: editWithdrawCost },
							"TransitCost": { type: "number", editable: editTransitCost },
							"InsuranceCost": { type: "number", editable: false },
							"Profit": { type: "number", editable: false }
						}
					}
				},
				data: data.Rows,
				aggregate: [
					{ field: "Count", aggregate: "sum" },
					{ field: "Weigth", aggregate: "sum" },
					{ field: "Value", aggregate: "sum" },
					{ field: "TotalTariffCost", aggregate: "sum" },
					{ field: "ScotchCost", aggregate: "sum" },
					{ field: "FactureCost", aggregate: "sum" },
					{ field: "WithdrawCost", aggregate: "sum" },
					{ field: "TransitCost", aggregate: "sum" },
					{ field: "InsuranceCost", aggregate: "sum" },
					{ field: "Profit", aggregate: "sum" }]
			};
		}

		function columns() {
			var c = [
				{ field: "ClientNic", title: $l.Entities_Nic },
				{ field: "DisplayNumber", title: $l.Entities_DisplayNumber },
				{ field: "Factory", title: $l.Entities_FactoryName },
				{ field: "Mark", title: $l.Entities_Mark },
				{ field: "Count", title: $l.Entities_Count, footerTemplate: "#= sum #" },
				{ field: "Weigth", title: $l.Entities_Weigth, footerTemplate: "#= sum #" },
				{ field: "Invoice", title: $l.Entities_Invoice },
				{ field: "Value", title: $l.Entities_Value, template: "#= kendo.toString(Value, 'n2') + CurrencyType[ValueCurrencyId] #", footerTemplate: "#= kendo.toString(sum, 'n2') #" },
				{ field: "TariffPerKg", title: $l.Entities_TariffPerKg, format: "{0:n2}" },
				{ field: "TotalTariffCost", title: $l.Entities_TotalTariffCost, footerTemplate: "#= kendo.toString(sum, 'n2') #" },
				{ field: "ScotchCost", title: $l.Entities_ScotchCost, footerTemplate: "#= kendo.toString(sum, 'n0') #" },
				{ field: "FactureCost", title: $l.Entities_FactureCost, footerTemplate: "#= kendo.toString(sum, 'n0') #" },
				{ field: "WithdrawCost", title: $l.Entities_WithdrawCost, footerTemplate: "#= kendo.toString(sum, 'n0') #" },
				{ field: "TransitCost", title: $l.Entities_TransitCost, footerTemplate: "#= kendo.toString(sum, 'n0') #" },
				{ field: "InsuranceCost", title: $l.Entities_Insurance, template: "#= kendo.toString(InsuranceCost, 'n2') + CurrencyType[ValueCurrencyId] #", footerTemplate: "#= kendo.toString(sum, 'n2') #" },
				{ field: "Profit", title: $l.Entities_Profit, footerTemplate: "#= kendo.toString(sum, 'n2') #" }
			];
			if ($r.IsAdmin) {
				c.push({
					command: [{
						name: "calculate",
						text: $l.Pages_Calculate,
						click: function(e) {
							var tr = $(e.target).closest("tr");
							var trData = this.dataItem(tr);
							alert("implement " + trData.ApplicationId);
						}
					}],
					title: "&nbsp;"
				});
			}

			return c;
		}

		function initAdditionalCost(row, data) {
			row.find(".additional-cost input").kendoNumericTextBox({
				decimals: 2,
				spinners: false,
				change: function() {
					post($u.Calculation_SetAdditionalCost, {
						awbId: data.AwbId,
						additionalCost: this.value()
					}, data.AwbId);
				}
			});
		}

		function post(url, data, awbId) {
			$.post(url, data).success(function() {
				updateMailGrid(awbId);
			}).fail($a.ShowError);
		}
		
		function updateMailGrid(awbId) {
			$.post($u.Calculation_Row, { id: awbId }).success(function (data) {
				var grid = $c.GetMainGrid();
				var oldData = grid.dataSource.get(awbId);
				$.extend(oldData, data);
				grid.refresh();								
			}).fail($a.ShowError);
		};

		$c.InitDetails = function(row, data) {

			initAdditionalCost(row, data);
			
			function save(e) {
				if (e.values.TariffPerKg !== undefined) {
					post($u.Calculation_SetTariffPerKg, {
						id: e.model.ApplicationId,
						tariffPerKg: e.values.TariffPerKg
					}, data.AwbId);
				}
				if (e.values.ScotchCost !== undefined) {
					post($u.Calculation_SetScotchCostEdited, {
						id: e.model.ApplicationId,
						scotchCost: e.values.ScotchCost
					}, data.AwbId);
				}
				if (e.values.FactureCost !== undefined) {
					post($u.Calculation_SetFactureCostEdited, {
						id: e.model.ApplicationId,
						factureCost: e.values.FactureCost
					}, data.AwbId);
				}
				if (e.values.WithdrawCost !== undefined) {
					post($u.Calculation_SetWithdrawCostEdited, {
						id: e.model.ApplicationId,
						withdrawCost: e.values.WithdrawCost
					}, data.AwbId);
				}
				if (e.values.TransitCost !== undefined) {
					post($u.ApplicationUpdate_SetTransitCost, {
						id: e.model.ApplicationId,
						transitCost: e.values.TransitCost
					}, data.AwbId);
				}
			}
			
			row.find(".calculation-rows").kendoGrid({
				dataSource: dataSource(data),
				scrollable: false,
				resizable: true,
				editable: true,
				save: save,
				columns: columns()
			});
		};

		return $c;
	})($a.Calculation || {});

	return $a;
})(Alicargo || {});