"use strict";

var main_mrp = new Vue({
	el: '#main_mrp',
	created() {
		this.loadMaterials();
	},
	data: {
		aMeterials: []
	},
	methods: {
		loadMaterials() {
			let sUrl = "https://localhost:44312/api/werehouse/product/get-products";

			axios.post(sUrl)
				.then((response) => {					
					this.aMeterials = response.data.rows;
					console.log("Список материалов на складах.", this.aMeterials);
					console.log("test material");
				})
				.catch((XMLHttpRequest) => {
					console.log("Ошибка получения списка материалов.", XMLHttpRequest.response.data);
				});
		}
	}
});