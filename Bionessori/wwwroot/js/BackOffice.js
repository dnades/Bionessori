"use strict"

var admin = new Vue({
	el: '#admin',
	created() {
		this.onInit();
	},
	data: {
		aUsers: [],
		user_role: localStorage["changeRole"]
	},
	methods: {
		onInit() {
			const sUrl = "https://localhost:44312/api/back-office/get-users";

			axios.post(sUrl, {})
				.then((response) => {
					console.log("Список пользователей", response.data);

					this.aUsers = response.data;
				})
				.catch((XMLHttpRequest) => {
					console.log("Ошибка получения списка пользователей", XMLHttpRequest.response.data);
				});
		},

		// Функция подготавливает данные для назначения роли пользователю.
		setRole(event) {
			let userId = +$(event.target).parent().parent().parent()[0].textContent.split(" ")[0];
			let userName = $(event.target).parent().parent().parent()[0].textContent.split(" ")[1];
			localStorage["changeRole"] = userName;	// Пользователь, которому назначается роль.			

			// Создает и инициализирует новый объект в глобальном объекте Vue.
			admin.set_role = {
				userId: userId,
				userName: userName
			};
		},

		// Функция назначает роль.
		onGiveRole() {
			let sUrl = "https://localhost:44312/api/back-office/give-role";
			
			let sRole = $(".input-role").val();

			// Дозаписывает в глобальный объект Vue роль.
			admin.set_role.userRole  = sRole;
			
			let oRole = {
				UserId: admin.set_role.userId,
				UserName: admin.set_role.userName,
				Role: admin.set_role.userRole
			};

			axios.post(sUrl, oRole)
				.then((response) => {
					console.log("Роль успешно назначена.", response.data);
				})
				.catch((XMLHttpRequest) => {
					console.log("Ошибка назначения роли.", XMLHttpRequest.response.data);
				});
		}
	}
});