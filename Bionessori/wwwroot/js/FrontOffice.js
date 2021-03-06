﻿"use strict";

var main = new Vue({
	el: '#front_office',
	created() {
		this.loadEmployeeInfo();
		//this.loadEmployeeReceptions();

		// Если у пользователя есть роль для просмотра своих записей.
		if (JSON.parse(localStorage["user"]).role.length &&
			JSON.parse(localStorage["user"]).role.includes("main_doctor_ambulator_card") ||
			JSON.parse(localStorage["user"]).role.includes("admin")) {
			this.loadEmployeeReceptions();	// Подгружает список записей на прием.
		}
	},
	data: {
		aEmployeeInfo: [],
		aEmployeeReceptions: [],
		errorPatient: true,
		checkReception: true,
		aSelectedReception: [],
		visibleEmployeeInfo: true,
		visibleDataEmployee: true
	},
	methods: {
		// Функция подгружает информацию о сотруднике в личный кабинет.
		loadEmployeeInfo() {
			let sUrl = "https://localhost:44312/api/front-office/get-employee-info";

			try {
				axios.post(sUrl, { Login: JSON.parse(localStorage["user"]).username })
					.then((response) => {
						this.aEmployeeInfo = response.data;

						// Форматирует дату рождения.
						let temp = new Date(this.aEmployeeInfo[0].dateBirth).toLocaleDateString();
						this.aEmployeeInfo[0].dateBirth = temp;

						// Форматирует дату найма.
						let temp1 = new Date(this.aEmployeeInfo[0].startDateWork).toLocaleDateString();
						this.aEmployeeInfo[0].startDateWork = temp1;

						console.log("Информация о сотруднике", this.aEmployeeInfo);
					})
					.catch((XMLHttpRequest) => {
						this.visibleEmployeeInfo = false;
						console.log("Ошибка получения информации о сотруднике", XMLHttpRequest.response.data);
					})
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		// Функция получает записи на прием конкретного врача.
		loadEmployeeReceptions() {
			let sUrl = "https://localhost:44312/api/front-office/employee-receptions";

			try {
				axios.post(sUrl, { Login: JSON.parse(localStorage["user"]).username })
					.then((response) => {
						this.aEmployeeReceptions = response.data;
						console.log("Записи сотрудника", this.aEmployeeReceptions);

						!main.aEmployeeReceptions.length ? this.visibleDataEmployee = false : this.visibleDataEmployee = true;
					})
					.catch((XMLHttpRequest) => {					
						this.visibleDataEmployee = false;
						console.log("Ошибка получения записей сотрудника", XMLHttpRequest.response.data);
					})
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		// Функция создает новое расписание.
		onCreateSchedule() {
			let sUrl = "https://localhost:44312/api/front-office/add-schedule";

			try {
				axios.post(sUrl, {
					EmployeeName: JSON.parse(localStorage["user"]).username,
					DateSchedule: new Date($("#id-date").val()).toLocaleString()
				})
					.then((response) => {
						console.log(response.data);
					})
					.catch((XMLHttpRequest) => {
						console.log("Ошибка создания расписания", XMLHttpRequest.response.data);
					})
			}
			catch (ex) {
				throw new Error(ex);
			}
		},
		
		// Функция удаляет записи на прием.
		onDeleteReceptionEmployee() {
			let aItems = main.aSelectedReception;
			console.log(aItems);

			let sUrl = "https://localhost:44312/api/front-office/delete-reception";

			try {
				axios.post(sUrl, {
					Id: aItems[0]	// TODO: переделать на множественное удаление.
				})
					.then((response) => {
						console.log(response.data);
					})
					.catch((XMLHttpRequest) => {
						console.log("Ошибка удаления записи", XMLHttpRequest.response.data);
					})
			}
			catch (ex) {
				throw new Error(ex);
			}
		}
	}
});