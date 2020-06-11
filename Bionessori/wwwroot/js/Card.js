"use strict";

var list_card = new Vue({
	el: '#list_card',
	created() {
		this.onLoadCards();
	},
	data: {
		aCards: []
	},
	methods: {
		onLoadCards() {
			let sUrl = "https://localhost:44312/api/data/card/get-cards";

			// Отправляет данные на Back-end.
			axios.post(sUrl, {})
				.then((response) => {
					console.log("Карточки загружены", response);
					this.aCards = response.data;
				})
				.catch((XMLHttpRequest) => {
					console.log("Ошибка получения списка карт пациентов.", XMLHttpRequest.response.data);
				});
		}
	}
});