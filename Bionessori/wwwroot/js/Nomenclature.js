"use strict";

var main = new Vue({
	el: '#nomenclature',
	created() {
		this.loadMaterials();
	},
	data: {
		aMaterials: [],
		weight: false,
		weightMeasurement: false,
		vat: false
	},
	methods: {
		// Функция загружает список материалов.
		loadMaterials() {
			let sUrl = "https://localhost:44312/api/werehouse/material/get-materials";

			try {
				axios.post(sUrl)
					.then((response) => {
						this.aMaterials = response.data;
						console.log("Список материалов на складах", this.aMaterials);
					})
					.catch((XMLHttpRequest) => {
						throw new Error("Ошибка получения списка материалов", XMLHttpRequest.response.data);
					});
			}
			catch (ex) {
				throw new Error(ex);
			}
		},
		onRouteCreateNomenclature() {
			window.location.href = "https://localhost:44312/create-nomenclature";
		},		
		onCheckedReq() {
			console.log(this.selectedRequests);
		}
	}
});