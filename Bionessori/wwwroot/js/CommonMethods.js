"use strict";

class BaseClass {
	// Валидация почты.
	static onValidEmail() {
		let reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
		let checkField = $("#id-email").val();

		if (reg.test(checkField) === false) {
			$("#id-error-email").html("Введите корректный email.");
			$("#id-error-email").addClass("check-email");
			throw new Error("Введите корректный email.");
		}
	}

	// Валидация логина.
	static onValidLogin() {
		let fieldLogin = $("#id-login").val();

		if (fieldLogin === "") {
			$("#id-error-login").html("Поле логина не может быть пустым.");
			$("#id-error-login").addClass("check-login");
			throw new Error("Поле логина не может быть пустым.");
		}
	}

	// Валидация пароля.
	static onValidPassword() {
		let fieldPassword = $("#id-password").val();

		if (fieldPassword === "") {
			$("#id-error-password").html("Поле пароля не может быть пустым");
			$("#id-error-password").addClass("check-password");
			throw new Error("Поле пароля не может быть пустым.");
		}
	}

	// Проверка на совпадение паролей.
	static onCheckPasswordFields() {
		let sPasswordFirstField = $("#id-password").val();
		let sPasswordSecondField = $("#id-password-repeat").val();

		if (sPasswordFirstField !== sPasswordSecondField) {
			$("#id-error-password").html("Пароли не совпадают");
			$("#id-error-password").addClass("check-password");
			throw new Error("Пароли не совпадают.");
		}
	}

	static onRemember() {
		// Метод не реализован.
	}
}