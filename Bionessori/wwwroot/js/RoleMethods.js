"use strict";

// Проверка ролей пользователя. В зависимости от них действия и кнопки доступны или не доступны.
class RoleBase {
	static initRole() {
		console.log("check user role");
		let aRoles;	// Все роли пользователя.

		if (localStorage["roles"]) {
			aRoles = JSON.parse(localStorage["roles"]);
		}
		else {
			$("#id-icon-edit").hide();
			$("#id-icon-delete").hide();
			$(".export-excel").hide();
			$(".print").hide();
			$("#id-create-card").hide();

			$("#id-card").prop("disabled", true);
			$("#id-complex").prop("disabled", true);
			$("#id-diagnostic").prop("disabled", true);
			$("#id-mrp").prop("disabled", true);
			$("#id-manage-resource").prop("disabled", true);
			$("#id-manage-personal").prop("disabled", true);
			$("#id-manage-shop").prop("disabled", true);
			$("#id-finance").prop("disabled", true);
			$("#id-anketa").prop("disabled", true);
			$("#id-nomenclature").prop("disabled", true);
			$("#id-quick-help").prop("disabled", true);
			$("#id-eating-service").prop("disabled", true);
			$("#id-indicator-service").prop("disabled", true);
			$("#id-statistic").prop("disabled", true);
			$("#id-registry").prop("disabled", true);
		}

		// Есть ли вообще роли у пользователя.
		if (aRoles) {
			// Если регистратор.
			if (aRoles.includes("registrar")) {
				$("#id-icon-edit").hide();
				$("#id-icon-delete").hide();
				$(".export-excel").hide();
				$(".print").hide();
			}

			// Если нет даже роли регистратора, то не даст создать карту.
			if (!aRoles.includes("registrar")) {
				$("#id-create-card").hide();
			}

			// Если у пользователя есть все роли.
			if (aRoles.includes("admin")) {
				$("#id-card").prop("disabled", false);
				$("#id-complex").prop("disabled", false);
				$("#id-diagnostic").prop("disabled", false);
				$("#id-mrp").prop("disabled", false);
				$("#id-manage-resource").prop("disabled", false);
				$("#id-manage-personal").prop("disabled", false);
				$("#id-manage-shop").prop("disabled", false);
				$("#id-finance").prop("disabled", false);
				$("#id-anketa").prop("disabled", false);
				$("#id-nomenclature").prop("disabled", false);
				$("#id-quick-help").prop("disabled", false);
				$("#id-eating-service").prop("disabled", false);
				$("#id-indicator-service").prop("disabled", false);
				$("#id-statistic").prop("disabled", false);
				$("#id-registry").prop("disabled", false);

				$("#id-icon-edit").show();
				$("#id-icon-delete").show();
				$(".export-excel").show();
				$(".print").show();
				$("#id-create-card").show();
			}
		}				
	}
}