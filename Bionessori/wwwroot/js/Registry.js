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
		aNumbers: [],
		cardNumber: ""
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
		},

		// Функция ищет пациента по его полису и дате рождения.
		onSearchPatient() {
			let sUrl = "https://localhost:44312/api/data/registry/identity-patient";
			let sPolicy = $("#id-policy").val();	// Полис пациента.
			let sDay = $("#id-day").val();
			let sMonth = $("#id-month").val();
			let sYear = $("#id-year").val();
			let sJoinDate = sDay.concat(".") + sMonth.concat(".") + sYear;	// Склеивает дату.
			let formatDate = sJoinDate.split(".").reverse().join("-");	// Приводит дату к правильному формату для бэка.

			try {
				axios.post(sUrl, {
					DateOfBirth: formatDate,
					Policy: sPolicy
				})
					.then((response) => {
						this.cardNumber = response.data[0].cardNumber;
						console.log("Пациент существует", this.cardNumber);
					})
					.catch((XMLHttpRequest) => {
						throw new Error("Пациент не существует", XMLHttpRequest.response.data);
					});
			}
			catch (ex) {
				throw new Error(ex);
			}
		}
	}
});