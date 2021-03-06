﻿"use strict";

var registry = new Vue({
	el: "#registry",
	created() {
		this.loadNumbersCards();
		this.onGetEmployee();
		this.loadEmployees();
		this.loadReceptions();
		this.loadSeatDirections();
		this.loadPatientNames();
		this.loadDirections();
		this.loadDirectionsType();
		this.loadDirectionsStatus();

		localStorage["editCard"] ? this.editCard = localStorage["editCard"] : "";		
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
		aEmployees: [],
		selectFullName: "",
		selectSpec: "",
		selectDate: "",
		aReceptions: [],
		editCard: "",
		deleteReception: null,
		deleteDirection: null,
		aSeatDirections: [],
		selectSeatDirection: "",
		selectPatient: "",
		aPatientNames: [],
		aDirections: [],
		directionType: "",
		directionStatus: "",
		selectedEmployee: "",
		aTypes: [],
		aStatuses: [],
		aSelectedReceptions: [],
		concreteDirection: [],
		concreteReception: []
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
		},

		// Функция получает расписания врача.
		onGetSchedules(event) {
			let sDoctor = event.target.value;
			let sUrl = "https://localhost:44312/api/data/registry/get-schedules";

			this.selectFullName = sDoctor;

			try {
				axios.post(sUrl, { FullName: sDoctor })
					.then((response) => {
						this.aSchedules = response.data;
						console.log("Список расписаний", this.aSchedules);
					})
					.catch((XMLHttpRequest) => {
						console.log("Ошибка получения списка расписаний", XMLHttpRequest.response.data);
					})
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		// Функция создает запись к врачу.
		onSaveReception() {
			let sUrl = "https://localhost:44312/api/data/registry/write-reception";
			let sTimeWrite = $("#id-select-date-val").val();
			let sName = $("#id-select-doctor-val").val();
			let sCardNumber = $("#id-select-number-val").val();

			try {
				axios.post(sUrl, {
					TimeProcRecommend: sTimeWrite,
					FullName: sName,
					CardNumber: sCardNumber
				})
					.then((response) => {
						console.log(response.data);
					})
					.catch((XMLHttpRequest) => {
						console.log("Ошибка создания записи", XMLHttpRequest.response.data);
					})
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		// Функция получает значение специализации для таблицы подтверждения.
		onChangeSpec(event) {
			let sValue = event.target.value;
			this.selectSpec = sValue
		},

		// Функция получает дату приема для таблицы подтверждения.
		onChangeSchedule(event) {
			let sValue = event.target.value;
			this.selectDate = sValue
		},

		// Функция получает список записей на прием.
		loadReceptions() {
			let sUrl = "https://localhost:44312/api/data/registry/get-receptions";

			try {
				axios.post(sUrl, {})
					.then((response) => {						
						this.aReceptions = response.data;
						console.log("Список записей", this.aReceptions);
					})
					.catch((XMLHttpRequest) => {
						console.log("Ошибка получения списка записей", XMLHttpRequest.response.data);
					})
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		// Функция получает запись для редактирования и переходит на страницу редактирования записи.
		onRouteEditReception(event) {			
			let receptionId = +$(event.target).parent().parent().parent()[0].textContent.split(" ")[1];
			localStorage["receptionId"] = +receptionId;
			localStorage["editCard"] = $(event.target).parent().parent().parent()[0].textContent.split(" ")[5];
			window.location.href = "https://localhost:44312/edit-reception";
		},

		// Функция редактирует запись на прием.
		onEditReception() {
			let sUrl = "https://localhost:44312/api/data/registry/edit-reception";
			let sTimeWrite = $("#id-select-date-val").val();

			try {
				axios.post(sUrl, {
					Date: sTimeWrite,
					Id: localStorage["receptionId"]
				})
					.then((response) => {
						setTimeout(function () {
							window.location.href = "https://localhost:44312/registry";
							this.loadReceptions();
						}, 3000);

						console.log(response.data);
						swal("Редактирование записи", "Запись успешно изменена.", "success");	
					})
					.catch((XMLHttpRequest) => {
						swal("Ошибка", "Ошибка редактирования записи.", "error");	
						console.log("Ошибка редактирования записи", XMLHttpRequest.response.data);
					})
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		// Функция получает запись, которую нужно удалить.
		onAfterDeleteReception(event) {
			let receptionId = +$(event.target).parent().parent().parent()[0].textContent.split(" ")[1];
			this.concreteReception = this.aReceptions.filter(el => el.id == receptionId);
			this.deleteReception = receptionId;
		},

		// Функция получает направление, которое нужно удалить.
		onAfterDeleteDirection() {
			let directionId = +$(event.target).parent().parent().parent()[0].textContent.split(" ")[0];
			this.concreteDirection = this.aDirections.filter(el => el.id == directionId);
			this.deleteDirection = directionId;
		},

		// Функция удаляет запись.
		onDeleteReception() {
			let sUrl = "https://localhost:44312/api/data/registry/delete-reception?id=".concat(this.deleteReception);

			try {
				axios.delete(sUrl)
					.then((response) => {
						setTimeout(function () {
							window.location.href = "https://localhost:44312/registry";
							this.loadReceptions();
						}, 3000);

						console.log(response.data);
						swal("Удаление записи", "Запись успешно удалена.", "success");						
					})
					.catch((XMLHttpRequest) => {
						swal("Ошибка", "Ошибка удаления записи.", "error");						
						console.log("Ошибка удаления записи", XMLHttpRequest.response.data);
					})
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		// Функция переходит на страницу направлений.
		onRouteDirection(event) {
			main.onRouteMatched(event);
		},

		// Функция находит запись по ее id.
		onSearchReception() {
			let sUrl = "https://localhost:44312/api/data/registry/get-reception";
			let iId = +$(".input-search").val();

			// Если нет id, значит нужно подгрузить все записи в таблицу.
			if (iId == "") {
				this.loadReceptions();
				return;
			}				

			try {
				axios.post(sUrl, { Id: iId })
					.then((response) => {
						console.log(response.data);
						this.aReceptions = response.data;
					})
					.catch((XMLHttpRequest) => {
						console.log("Ошибка получения записи", XMLHttpRequest.response.data);
					})
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		// Функция переходит к созданию направления.
		onRouteCreateDirection(e) {
			main.onRouteMatched(e);
		},

		// Функция получает список мест направлений.
		loadSeatDirections() {
			let sUrl = "https://localhost:44312/api/data/registry/get-seat-directions";

			try {
				axios.post(sUrl)
					.then((response) => {
						this.aSeatDirections = response.data;
						console.log("Места направлений", this.aSeatDirections);
					})
					.catch((XMLHttpRequest) => {
						console.log("Ошибка получения мест направлений", XMLHttpRequest.response.data);
					})
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		// Функция подгружает список карт пациентов.
		loadPatientNames() {
			let sUrl = "https://localhost:44312/api/data/registry/get-patient-names";

			try {
				axios.post(sUrl)
					.then((response) => {
						this.aPatientNames = response.data;
						console.log("Список имен пациентов", this.aPatientNames);
					})
					.catch((XMLHttpRequest) => {
						console.log("Ошибка получения списка имен пациентов", XMLHttpRequest.response.data);
					})
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		// Функция создает направление.
		onCreateDirection() {
			let sUrl = "https://localhost:44312/api/data/registry/create-direction";
			let sPatient = $("#id-select-number-val").text();
			let sDirection = $("#id-select-doctor-val").text();
			let sDirectionType = $("#id-select-type").text();
			let sDirectionStatus = $("#id-select-status").text();
			let sDirectionEmployee = $("#id-select-employee").text();

			try {
				axios.post(sUrl, {
					PatientName: sPatient,
					SeatDirection: sDirection,
					Type: sDirectionType,
					Status: sDirectionStatus,
					EmployeeName: sDirectionEmployee
				})
					.then((response) => {
						setTimeout(function () {
							window.location.href = "https://localhost:44312/direction";
						}, 3000);	

						swal("Создание направления", "Направление успешно создано.", "success");
						console.log(response.data);
					})
					.catch((XMLHttpRequest) => {
						console.log("Ошибка создания направления", XMLHttpRequest.response.data);
					})
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		// Функция редактирует направление.
		onEditDirection() {
			let sUrl = "https://localhost:44312/api/data/registry/edit-direction";
			let sPatient = $("#id-select-number-val").text();
			let sDirection = $("#id-select-doctor-val").text();
			let sDirectionType = $("#id-select-type").text();
			let sDirectionStatus = $("#id-select-status").text();
			let sDirectionEmployee = $("#id-select-employee").text();

			try {
				axios.post(sUrl, {
					Id: localStorage["editDirectId"],
					PatientName: sPatient,
					SeatDirection: sDirection,
					Type: sDirectionType,
					Status: sDirectionStatus,
					EmployeeName: sDirectionEmployee
				})
					.then((response) => {
						setTimeout(function () {
							window.location.href = "https://localhost:44312/direction";
							this.loadDirections();
						}, 3000);

						console.log(response.data);
						swal("Редактирование направления", "Направление успешно изменено.", "success");	
					})
					.catch((XMLHttpRequest) => {
						swal("Ошибка", "Ошибка редактирования направления.", "success");	
						console.log("Ошибка редактирования направления", XMLHttpRequest.response.data);
					})
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		// Функция удаляет направление.
		onDeleteDirection() {		
			// Получает id направления, которое нужно удалить.
			let sUrl = "https://localhost:44312/api/data/registry/delete-direction?id=".concat(this.deleteDirection);
			
			try {
				axios.delete(sUrl)
					.then((response) => {
						setTimeout(function () {
							window.location.href = "https://localhost:44312/direction";
							this.loadDirections();
						}, 3000);

						console.log(response.data);
						swal("Удаление направления", "Направление успешно удалено.", "success");						
					})
					.catch((XMLHttpRequest) => {
						console.log("Ошибка удаления направления", XMLHttpRequest.response.data);
					})
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		// Функция получает типы направлений.
		loadDirections() {
			let sUrl = "https://localhost:44312/api/data/registry/get-directions";

			try {
				axios.post(sUrl)
					.then((response) => {
						this.aDirections = response.data;
						console.log("Список направлений", this.aDirections);
					})
					.catch((XMLHttpRequest) => {
						console.log("Ошибка получения списка направлений", XMLHttpRequest.response.data);
					})
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		// Функция получает типы направлений.
		loadDirectionsType() {
			let sUrl = "https://localhost:44312/api/data/registry/get-types";

			try {
				axios.post(sUrl)
					.then((response) => {
						this.aTypes = response.data;
						console.log("Типы направлений", this.aTypes);
					})
					.catch((XMLHttpRequest) => {
						console.log("Ошибка получения типов направлений", XMLHttpRequest.response.data);
					})
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		// Функция получает статусы направлений.
		loadDirectionsStatus() {
			let sUrl = "https://localhost:44312/api/data/registry/get-statuses";

			try {
				axios.post(sUrl)
					.then((response) => {
						this.aStatuses = response.data;
						console.log("Статусы направлений", this.aStatuses);
					})
					.catch((XMLHttpRequest) => {
						console.log("Ошибка получения статусов направлений", XMLHttpRequest.response.data);
					})
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		// Переход на страницу редактирования направления.
		onRouteEditDirection(e) {
			// Получает id направления, которое нужно редактировать.
			let directId = +$(event.target).parent().parent().parent()[0].textContent.split(" ")[0];
			localStorage["editDirectId"] = directId;
			main.onRouteMatched(e);
		},

		// При нажатии на checkbox записи.
		onChecked() {
			console.log(this.aSelectedReceptions);
		}
	}
});