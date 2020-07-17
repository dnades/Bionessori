"use strict";

var registry = new Vue({
	el: "#registry",
	created() {
		this.loadNumbersCards();
	},
	data: {
		visibleGroup: false,
		visibleMaterial: false,
		visibleMeasure: false,
		werehouseNum: false,
		aNumbers: []
	},
	methods: {
		// Функция передает роут в точку распределения роутов.
		onRouteReception(event) {
			main.onRouteMatched(event);
		},

		// Функция получает номера карт.
		loadNumbersCards() {
			let sUrl = "https://localhost:44312/api/data/registry/get-numbers-cards";

			try {
				axios.post(sUrl)
					.then((response) => {
						this.aNumbers = response.data;
						console.log("Список номеров карт", this.aNumbers);
					})
					.catch((XMLHttpRequest) => {
						throw new Error("Ошибка получения списка номеров карт.", XMLHttpRequest.response.data);
					});
			}
			catch (ex) {
				throw new Error(ex);
			}
		}
	}
});