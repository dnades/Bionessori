"use strict";

// Распределение роутов веб-приложения.
var main = new Vue({
	el: '#main',	
	created() {
		if (localStorage["user"]) {
			RoleBase.initRole();
		}
	},
	methods: {		
		onRouteCard(event) {
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