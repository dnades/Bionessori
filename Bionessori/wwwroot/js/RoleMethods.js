"use strict";

// Проверка ролей пользователя. В зависимости от них действия и кнопки доступны или не доступны.
class RoleBase {
	static initRole() {
		console.log("check user role");
		let aRoles;	// Все роли пользователя.

		if (localStorage["roles"]) {
			aRoles = JSON.parse(localStorage["roles"]);
		}

		// Если регистратор.
		if (aRoles.includes("registrar")) {
			$("#id-icon-edit").hide();
			$("#id-icon-delete").hide();
			$(".export-excel").hide(); 
			$(".print").hide();
		}
	}
}