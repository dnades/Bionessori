"use strict";

// Распределение роутов веб-приложения.
var main = new Vue({
	el: '#main',
	created() {
		RoleBase.initRole();

		// Если зашел админ.
		if (localStorage["roles"].includes("admin")) {
			window.location.href = "https://localhost:44312/Route/RouteAdmin";
		}
	},
	methods: {
		onRouteMatched(event) {
			let sRoute = event.target.value;

			switch (sRoute) {
				// Переходит на страницу карт пациентов.
				case "card":
					window.location.href = "https://localhost:44312/Route/RouteCard";
					break;				
			}
		}
	}
});