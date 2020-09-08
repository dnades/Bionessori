"use strict";

var main = new Vue({
	el: '#nomenclature',
	created() {
		this.loadMaterials();
		this.loadRequests();
	},
	data: {
		aMaterials: [],
		weight: false,
		weightMeasurement: false,
		vat: false,
		aRequests: [],
		selectedRequests: []
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

		// Функция загружает список заявок на потребности.
		loadRequests() {
			let sUrl = "https://localhost:44312/api/purchase/get-requests";

			try {
				axios.get(sUrl)
					.then((response) => {
						this.aRequests = response.data;

						console.log("Список заявок", this.aRequests);
					})
					.catch((XMLHttpRequest) => {
						throw new Error("Ошибка получения списка заявок", XMLHttpRequest.response.data);
					});
			}
			catch (ex) {
				throw new Error(ex);
			}
		},
		onCheckedReq() {
			console.log(this.selectedRequests);
		}
	}
});