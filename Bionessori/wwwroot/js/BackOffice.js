"use strict"

var admin = new Vue({
	el: '#admin',
	created() {
		this.onInit();
	},
	data: {
		aUsers: []
	},
	methods: {
		onInit() {
			const sUrl = "https://localhost:44312/api/back-office/get-users";

			// Отправляет данные на Back-end.
			axios.post(sUrl, {})
				.then((response) => {
					console.log("Список пользователей", response.data);

					this.aUsers = response.data;					
				})
				.catch((XMLHttpRequest) => {
					console.log("Ошибка получения списка пользователей", XMLHttpRequest.response.data);
				});
		}
	}
});