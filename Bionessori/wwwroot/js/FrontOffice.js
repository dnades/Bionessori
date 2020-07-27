"use strict";

var main = new Vue({
	el: '#front_office',
	created() {
		this.loadEmployeeInfo();

		// Если у пользователя есть роль для просмотра своих записей.
		if (JSON.parse(localStorage["user"]).role.length &&
			JSON.parse(localStorage["user"]).role.includes("main_doctor_ambulator_card") ||
			JSON.parse(localStorage["user"]).role.includes("admin")) {
			this.loadEmployeeReceptions();
		}
	},
	data: {
		aEmployeeInfo: [],
		aEmployeeReceptions: []
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
					})
					.catch((XMLHttpRequest) => {
						console.log("Ошибка получения записей сотрудника", XMLHttpRequest.response.data);
					})
			}
			catch (ex) {
				throw new Error(ex);
			}
		}
	}
});