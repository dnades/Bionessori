"use strict";

var purchases = new Vue({
	el: '#purchases',
	created() {
		this.loadRequests();
		this.loadDistinctMaterials();
		this.loadGroupsMaterials();
		this.loadMeasures();
	},
	data: {		
		aRequests: [],
		aMaterials: [],
		aMaterialsGroups: [],
		aDistinctMaterials: [],
		selectedRequests: [],
		aAddedMaterials: [],
		aGroups: [],
		aCountMaterials: [],
		aMeasures: [],
		aDates: [],
		aSums: []
	},
	methods: {		
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
		},

		// Функция переходит на страницу КП.
		onRouteCommerceOffer() {
			window.location.href = "https://localhost:44312/commerce-offer";
		},

		// Функция переходит к формированию нового коммерческого предложения поставщикам.
		onCreateOffer() {
			window.location.href = "https://localhost:44312/create-commerce-offer";
		},

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

		// Функция получает список материалов без дублей.
		loadDistinctMaterials() {
			let sUrl = "https://localhost:44312/api/werehouse/material/get-distinct-materials";

			try {
				axios.post(sUrl, {})
					.then((response) => {
						this.aDistinctMaterials = response.data;
						console.log("Список материалов без дублей", this.aDistinctMaterials);
					})
					.catch((XMLHttpRequest) => {
						throw new Error("Ошибка получения списка материалов без дублей", XMLHttpRequest.response.data);
					});
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		// Функция получает список ед.изм.
		loadMeasures() {
			let sUrl = "https://localhost:44312/api/werehouse/material/get-measures";

			try {
				axios.post(sUrl, {})
					.then((response) => {
						this.aMeasures = response.data;
						console.log("Список ед.изм", this.aMeasures);
					})
					.catch((XMLHttpRequest) => {
						throw new Error("Ошибка получения ед.изм", XMLHttpRequest.response.data);
					});
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		// Функция получает список групп материалов.
		loadGroupsMaterials() {
			let sUrl = "https://localhost:44312/api/werehouse/material/get-groups";

			try {
				axios.post(sUrl, {})
					.then((response) => {
						this.aMaterialsGroups = response.data;
						console.log("Список групп", this.aMaterialsGroups);
					})
					.catch((XMLHttpRequest) => {
						throw new Error("Ошибка получения списка групп", XMLHttpRequest.response.data);
					});
			}
			catch (ex) {
				throw new Error(ex);
			}
		},
	}
});