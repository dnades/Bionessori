"use strict";

var app = new Vue({
	el: '#app',
	data: {},
	methods: {		
		// Функция отправляет данные регистрации на Back-end.
		onCheckIn() {
			BaseClass.onValidLogin();
			BaseClass.onValidEmail();
			BaseClass.onValidPassword();
			BaseClass.onCheckPasswordFields();

			// Делает кнопку регистрации не активной пока Back-end не получит данные.
			$(".btn-checkin").prop('disabled', true);

			var sLogin = $("#id-login").val();
			var sEmail = $("#id-email").val();
			var sPassword = $("#id-password").val();

			const sUrlCheckIn = "https://localhost:44350/api/data/auth/checkin";

			const oUser = {
				Login: sLogin,
				Email: sEmail,
				Password: sPassword
			};

			// Отправляет данные на Back-end.
			axios.post(sUrlCheckIn, oUser)
				.then((response) => {
					console.log("Регистрация нового пользователя прошла успешно.", response);

					$(".btn-checkin").prop('disabled', false);
				})
				.catch((XMLHttpRequest) => {
					console.log("request send error", XMLHttpRequest.response.data);

					$(".btn-checkin").prop('disabled', false);
				});
		},

		// Функция отправляет на Back-end данные авторизации.
		onSignIn() {
			$(".btn-login").prop('disabled', true);

			var sLogin = $("#exampleInputLogin").val();
			var sPassword = $("#exampleInputPassword").val();

			const sUrl = "https://localhost:44323/api/data/auth/signin";

			const oUserReg = {
				Login: sLogin,
				Password: sPassword
			};

			// Отправляет данные на Back-end.
			axios.post(sUrl, oUserReg)
				.then((response) => {
					$(".btn-login").prop('disabled', false);

					console.log(response);
				})
				.catch((XMLHttpRequest) => {
					$(".btn-login").prop('disabled', false);
					$("#id-error-login").addClass("check-authorization");
					$("#id-error-login").html("Логин или пароль введены не верно.");

					console.log("request send error", XMLHttpRequest.response.data);
				});
		}
	}
});