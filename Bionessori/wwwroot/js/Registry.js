"use strict";

var registry = new Vue({
	el: "#registry",
	created() {
		this.loadNumbersCards();
		this.onGetEmployee();
		this.loadEmployees();
	},
	data: {
		visibleGroup: false,
		visibleMaterial: false,
		visibleMeasure: false,
		werehouseNum: false,
		aNumbers: [],
		cardNumber: "",
		errorPatient: false,
		aEmployee: [],
		aSchedules: [],
		aEmployees: []
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
						this.errorPatient = false;
						this.cardNumber = response.data[0].cardNumber;
						console.log("Пациент существует", this.cardNumber);
					})
					.catch((XMLHttpRequest) => {
						this.errorPatient = true;
						throw new Error("Пациент не существует", XMLHttpRequest.response.data);
					});
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		// Функция получает ФИО и специализацию врача.
		onGetEmployee() {
			let sUrl = "https://localhost:44312/api/data/registry/get-partial-employee";
			let sLogin = JSON.parse(localStorage["user"]).username;

			try {
				axios.post(sUrl, { login: sLogin })
					.then((response) => {						
						this.aEmployee = response.data;
						console.log("Авторизованный сотрудник", this.aEmployee);
					})
					.catch((XMLHttpRequest) => {
						console.log("Ошибка получения сотрудника", XMLHttpRequest.response.data);
					})
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		// Функция получает список сотрудников.
		loadEmployees() {
			let sUrl = "https://localhost:44312/api/data/registry/get-employees";

			try {
				axios.post(sUrl)
					.then((response) => {
						this.aEmployees = response.data;
						console.log("Список сотрудников", this.aEmployees);
					})
					.catch((XMLHttpRequest) => {
						console.log("Ошибка получения списка сотрудников", XMLHttpRequest.response.data);
					})
			}
			catch (ex) {
				throw new Error(ex);
			}
		}
	}
});