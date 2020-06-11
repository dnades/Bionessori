"use strict";

var main = new Vue({
	el: '#main',
	methods: {
		// Функция распределяет роуты веб-приложения.
		onRouteCard(event) {
			let sRoute = event.target.value;

			switch (sRoute) {
				// Переходит на страницу карточек.
				case "card":
					window.location.href = "https://localhost:44312/Route/RouteCard";
					break;
			}
		}
	}
});