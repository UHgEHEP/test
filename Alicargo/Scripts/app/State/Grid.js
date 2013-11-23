﻿$(function() {
	var $a = Alicargo;

	$a.States = (function($states) {

		var $u = $a.Urls;
		var $l = $a.Localization;		
		
		var dataSource = {
			transport: {
				read: {
					dataType: "json",
					url: $u.State_List,
					type: "POST",
					cache: false
				}
			},
			schema: { model: { id: "Id" } },
			error: $a.ShowError
		};
		
		function getGrid() {
			var grid = $("#state-grid").data("kendoGrid");
			getGrid = function() { return grid; };

			return getGrid();
		};
		
		var updateGrid = function() {
			var grid = getGrid();
			grid.dataSource.read();
			grid.refresh();
		};
		
		function dataBound() {
			$("tr a.k-grid-custom-delete").each(function() {
				var button = $(this);
				var dataItem = getGrid().dataItem(button.closest("tr"));
				if (dataItem.IsSystem) {
					button.remove();
				}
			});
		}

		$("#state-grid").kendoGrid({
			dataSource: dataSource,
			pageable: false,
			dataBound: dataBound,
			columns: [
				{ field: "Name", title: $l.Pages_Name },
				{
					command: [{
						name: "custom-edit",
						text: "",
						click: function(e) {
							e.preventDefault();
							var tr = $(e.target).closest("tr");
							var data = this.dataItem(tr);
							var url = $u.State_Edit + "/" + data.Id;
							
							$a.LoadPage(url);
						}
					}], title: "&nbsp;", width: $a.DefaultGridButtonWidth
				}, {
					command: [{
						name: "custom-delete",
						text: "",
						click: function(e) {
							if ($a.Confirm($a.Localization.Pages_DeleteConfirm)) {
								var tr = $(e.target).closest("tr");
								var data = this.dataItem(tr);
								var url = $a.Urls.State_Delete;
								$.post(url, { id: data.Id }).done(updateGrid).fail(error);
							}
						}
					}],
					title: "&nbsp;",
					width: Alicargo.DefaultGridButtonWidth
				}]
		});

		return $states;
	})($a.States || {});
});