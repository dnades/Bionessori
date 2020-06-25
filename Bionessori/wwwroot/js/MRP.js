"use strict";

var main_mrp = new Vue({
	el: '#main_mrp',
	created() {
		this.loadMaterials();
		this.loadRequests();
	},
	data: {
		aMaterials: [],
		aRequests: []
	},
	methods: {
		// Функция загружает список материалов.
		loadMaterials() {
			let sUrl = "https://localhost:44312/api/werehouse/product/get-products";

			axios.post(sUrl)
				.then((response) => {					
					this.aMaterials = response.data.rows;
					console.log("Список материалов на складах.", this.aMaterials);
				})
				.catch((XMLHttpRequest) => {
					console.log("Ошибка получения списка материалов.", XMLHttpRequest.response.data);
				});
		},

		// Функция загружает список заявок на потребности.
		loadRequests() {
			let sUrl = "https://localhost:44312/api/werehouse/request/get-requests";

			axios.post(sUrl, {})
				.then((response) => {
					this.aRequests = response.data;
					console.log("Список заявок.", this.aRequests);
				})
				.catch((XMLHttpRequest) => {
					console.log("Ошибка получения списка заявок.", XMLHttpRequest.response.data);
				});
		}
	}
});