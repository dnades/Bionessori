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

	// Сортирует карту пациента по ее номеру.
	static sortByCardNumber(d1, d2) {
		return (d1.cardNumber > d2.cardNumber) ? 1 : -1;
	}

	// Сортирует по ФИО.
	static sortByFio(d1, d2) {
		return (d1.fullName.toLowerCase() > d2.fullName.toLowerCase()) ? 1 : -1;
	}

	// Сортирует по дате рождения.
	static sortByYearOfBirth(d1, d2) {
		return (d1.dateOfBirth > d2.dateOfBirth) ? 1 : -1;
	}

	// Сортирует по адресу.
	static sortByAddress(d1, d2) {
		return (d1.address.toLowerCase() > d2.address.toLowerCase()) ? 1 : -1;
	}

	// Сортирует по номеру телефону.
	static sortByNumber(d1, d2) {
		return (d1.number > d2.number) ? 1 : -1;
	}

	// Сортирует по полису.
	static sortByPolicy(d1, d2) {
		return (d1.policy > d2.policy) ? 1 : -1;
	}

	// Сортирует по СНИЛС.
	static sortBySnails(d1, d2) {
		return (d1.snails > d2.snails) ? 1 : -1;
	}

	// Сортирует по дате и времени записи на процедуры.
	static sortByDateTimeProc(d1, d2) {
		return (d1.timeProcRecommend > d2.timeProcRecommend) ? 1 : -1;
	}

	// Сортирует по прописанным лекарствам.
	static sortByDrugs(d1, d2) {
		return (d1.prescriptionDrugs.toLowerCase() > d2.prescriptionDrugs.toLowerCase()) ? 1 : -1;
	}

	// Сортирует по диагнозу.
	static sortByDiagnosis(d1, d2) {
		return (d1.diagnosis.toLowerCase() > d2.diagnosis.toLowerCase()) ? 1 : -1;
	}

	// Сортирует по рекомендациям.
	static sortByRecommends(d1, d2) {
		return (d1.recipesRecommend.toLowerCase() > d2.recipesRecommend.toLowerCase()) ? 1 : -1;
	}

	// Сортирует по истории болезни.
	static sortByHistory(d1, d2) {
		return (d1.medicalHistory.toLowerCase() > d2.medicalHistory.toLowerCase()) ? 1 : -1;
	}

	// Сортирует по доктору.
	static sortByDoctor(d1, d2) {
		return (d1.doctor.toLowerCase() > d2.doctor.toLowerCase()) ? 1 : -1;
	}
}