﻿"use strict";

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
					window.location.href = "https://localhost:44312/card";
					break;		

				case "create_card":
					window.location.href = "https://localhost:44312/create-card";
					break;
					
				// Переходит на страницу ведения MRP
				case "mrp":
					window.location.href = "https://localhost:44312/mrp";
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
					window.location.href = "https://localhost:44312/create-request";
					break;

				case "registry":
					window.location.href = "https://localhost:44312/registry";
					break;

				case "route_reception":
					window.location.href = "https://localhost:44312/add-registry";
					break;

				case "route_direction":
					window.location.href = "https://localhost:44312/direction";
					break;

				case "create_direction":
					window.location.href = "https://localhost:44312/create-direction";
					break;

				case "edit_direction":
					window.location.href = "https://localhost:44312/edit-direction";
					break;			

				case "view_nomenclature":
					window.location.href = "https://localhost:44312/view-nomenclature";
					break; 

				case "view_purchases":
					window.location.href = "https://localhost:44312/view-manage-purchases";
					break; 
			}
		}
	}
});