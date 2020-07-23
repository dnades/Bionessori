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
						console.log("Информация о сотруднике", response.data);
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