"use strict";

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

				case "create_card":
					window.location.href = "https://localhost:44312/route/create-card";
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

				// Переходит к созданию заявки.
				case "create_request":
					window.location.href = "https://localhost:44312/route/create-request";
					break;

				case "registry":
					window.location.href = "https://localhost:44312/route/route-registry";
					break;

				case "route_reception":
					window.location.href = "https://localhost:44312/add-registry";
					break;

				case "route_direction":
					window.location.href = "https://localhost:44312/route-direction";
					break;
			}
		}
	}
});