"use strict";

var purchases = new Vue({
	el: '#purchases',
	created() {
		this.loadRequests();
	},
	data: {		
		aRequests: [],
		selectedRequests: []
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
		}
	}
});