"use strict";

var main = new Vue({
	el: '#front_office',
	created() {
		this.loadEmployeeInfo();
	},
	data: {
		aEmployeeInfo: []
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
		}
	}
});