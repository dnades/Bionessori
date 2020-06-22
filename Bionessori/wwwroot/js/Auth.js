"use strict";

var app = new Vue({
	el: '#app',	
	created() {
		$(".state").hide();	// Изначально скрывает иконку пользователя.

		if (localStorage["user"]) {
			$(".state-log").hide();
			$(".state").show();
		}
	},
	methods: {		
		// Функция отправляет данные регистрации на Back-end.
		onCheckIn() {
			BaseClass.onValidLogin();
			BaseClass.onValidEmail();
			BaseClass.onValidPassword();
			BaseClass.onCheckPasswordFields();

			// Делает кнопку регистрации не активной пока Back-end не получит данные.
			$(".btn-checkin").prop('disabled', true);

			let sLogin = $("#id-login").val();
			let sEmail = $("#id-email").val();
			let sNumber = $("#id-number").val();
			let sPassword = $("#id-password").val();

			const sUrlCheckIn = "https://localhost:44312/api/data/auth/checkin";

			const oUser = {
				Login: sLogin,
				Email: sEmail,
				Number: sNumber,
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

			let sLogin = $("#id-sign-login").val();
			let sPassword = $("#id-sign-password").val();
			let aRoles;

			const sUrl = "https://localhost:44312/api/data/auth/signin";

			const oUserReg = {
				Login: sLogin,
				Password: sPassword
			};

			// Отправляет данные на Back-end.
			axios.post(sUrl, oUserReg)
				.then((response) => {
					$(".btn-login").prop('disabled', false);

					// Записывает роли пользователя.
					aRoles = response.data.role;
					localStorage["roles"] = JSON.stringify(aRoles);

					// Записывает данные авторизованного пользователя в кэш.
					localStorage["user"] = JSON.stringify(response.data);

					// Если зашел админ.
					if (localStorage["roles"].includes("admin")) {
						window.location.href = "https://localhost:44312/Route/RouteAdmin";
					}

					$(".state-log").hide();
					$(".state").show();

					$("#id-card").prop("disabled", false);
					$("#id-complex").prop("disabled", false);
					$("#id-diagnostic").prop("disabled", false);
					$("#id-mrp").prop("disabled", false);
					$("#id-manage-resource").prop("disabled", false);
					$("#id-manage-personal").prop("disabled", false);
					$("#id-manage-shop").prop("disabled", false);
					$("#id-finance").prop("disabled", false);
					$("#id-anketa").prop("disabled", false);
					$("#id-manage-warehouse").prop("disabled", false);
					$("#id-quick-help").prop("disabled", false);
					$("#id-eating-service").prop("disabled", false);
					$("#id-indicator-service").prop("disabled", false);
					$("#id-statistic").prop("disabled", false);
					$("#id-registry").prop("disabled", false);
				
					console.log("Пользователь успешно авторизован.", response);
				})
				.catch((XMLHttpRequest) => {
					$(".btn-login").prop('disabled', false);
					$("#id-error-login").addClass("check-authorization");
					$("#id-error-login").html("Логин или пароль введены не верно.");

					console.log("Ошибка авторизации пользователя.", XMLHttpRequest.response.data);
				});
		}
	}
});