"use strict";

// Проверка ролей пользователя. В зависимости от них действия и кнопки доступны или не доступны.
class RoleBase {
	static initRole() {
		let aRoles;	// Все роли пользователя.

		if (localStorage["roles"]) {
			aRoles = JSON.parse(localStorage["roles"]);
		}

		// Если у пользователя есть все роли.
		if (aRoles.includes("all_roles")) {
			$("#id-card").prop("disabled", false);
			$("#id-complex").prop("disabled", false);
			$("#id-diagnostic").prop("disabled", false);
			$("#id-mrp").prop("disabled", false);
			$("#id-manage-resource").prop("disabled", false);
			$("#id-manage-personal").prop("disabled", false);
			$("#id-manage-shop").prop("disabled", false);
			$("#id-finance").prop("disabled", false);
			$("#id-anketa").prop("disabled", false);
			$("#id-manage-warehouse").prop("disabled", false);
			$("#id-quick-help").prop("disabled", false);
			$("#id-eating-service").prop("disabled", false);
			$("#id-indicator-service").prop("disabled", false);
			$("#id-statistic").prop("disabled", false);
			$("#id-registry").prop("disabled", false);
		}
	}
}