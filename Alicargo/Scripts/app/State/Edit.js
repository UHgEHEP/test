﻿$(function() {
	var $a = Alicargo;
	var $u = $a.Urls;

	var stateId = $("#Id").val();

	$("#Language").change(function(e) {
		e.preventDefault();
		var lang = $(e.target).val();
		var url = $u.State_Edit + "/" + stateId + "?lang=" + lang;

		$a.LoadPage(url);
	});
});