"use strict";

// Распределение роутов веб-приложения.
var main = new Vue({
	el: '#main',
	created() {
		RoleBase.initRole();		
	},
	methods: {
		onRouteMatched(event) {
			let sRoute = event.target.value;

			switch (sRoute) {
				// Переходит на страницу карт пациентов.
				case "card":
					window.location.href = "https://localhost:44312/route/card";
					break;		
					
				// Переходит на страницу ведения MRP
				case "mrp":
					window.location.href = "https://localhost:44312/route/mrp";
					break;

				// Переходит на страницу списка заявок на потребности MRP.
				case "mrp_requests_list":
					window.location.href = "https://localhost:44312/view/request";
					break;

				// Переходит на страницу списка материалов на складах MRP.
				case "mrp_material_list":
					window.location.href = "https://localhost:44312/view/material";
					break;
			}
		}
	}
});