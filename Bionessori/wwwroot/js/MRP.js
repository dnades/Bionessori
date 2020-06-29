"use strict";

var main_mrp = new Vue({
	el: '#main_mrp',
	created() {
		this.loadMaterials();
		this.loadRequests();
	},
	data: {
		aMaterials: [],
		aRequests: [],
		concreteRequest: [],
		concreteMaterial: []
	},
	methods: {
		// Функция загружает список материалов.
		loadMaterials() {
			let sUrl = "https://localhost:44312/api/werehouse/product/get-products";

			axios.post(sUrl)
				.then((response) => {					
					this.aMaterials = response.data;
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
		},

		// Функция получает конкретную заявку.
		onGetRequest(event) {
			let reqId = $(event.target).parent().parent().parent()[0].textContent.split(" ")[0];

			// Находит заявку, на которую нажали.
			this.concreteRequest = this.aRequests.filter(el => el.number  == reqId);			
		},

		// Функция получает конкретный материал склада.
		onGetMaterial(event) {
			let materialId = $(event.target).parent().parent()[0].textContent.split(" ")[0];

			this.concreteMaterial = this.aMaterials.filter(el => el.id == materialId);

			console.log("show material");
		},

		// Функция сортирует список материалов на складах.
		sortedMaterial(event) {
			let sParam = event.target.value;

			switch (sParam) {
				case "material":
					return this.aMaterials.sort(BaseClass.sortByMaterial);

				case "group":
					return this.aMaterials.sort(BaseClass.sortByMaterialGroup);
			}
		},

		// Функция ищет материал по его названию или группе.
		onSearchMaterial() {
			let sMaterial = $("#id-search-material").val();	// Что ищет.
			let aMaterial, aMaterialGroup;

			if (sMaterial == "")
				this.loadMaterials();

			aMaterial = this.aMaterials.filter(el => el.material.includes(sMaterial));

			// Если искали по названию.
			if (aMaterial.length !== 0) {
				this.aMaterials = aMaterial;
			}
			// Если по названию не нашли, значит искали по группе.
			else {
				aMaterialGroup = this.aMaterials.filter(el => el.materialGroup.includes(sMaterial));
				this.aMaterials = aMaterialGroup;
			}
		}
	}
});