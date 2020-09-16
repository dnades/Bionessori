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
		aMaterialsGroups: [],
		aDistinctMaterials: [],
		selectedRequests: [],
		aAddedMaterials: [],
		aMeasures: [],
		oTable: {
			aMaterials: [],
			aGroups: [],
			aCountMaterials: [],
			aAddMeasures: [],
			aDates: [],
			aSums: []
		},
		isLoadReq: false
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

		// Функция добавляет строку в таблицу.
		onAddedTableRow() {
			let sMaterial = $("#id-inp-mat").val();	// Материал.
			let sGroup = $("#id-inp-group").val();	// Группа.
			let sMeasure = $("#id-inp-meas").val();	// Ед.Изм.
			let iCount = +$("#id-int-count").val();	// Кол-во.
			let sDate = $("#id-int-date").val();	// Дата поставки.
			let iSum = +$("#id-int-max-sum").val();	// Максимальная сумма.			

			this.errorEmptyRequiredRow(sMaterial, sGroup, sMeasure, iCount, sDate);
			this.addDataArray(sMaterial, sGroup, sMeasure, iCount, sDate, iSum);
			this.addTableRow();
		},

		// Валидация полей.
		errorEmptyRequiredRow: function (material, group, measure, count, date) {
			if (!material || !group || !measure || !count || !date) {
				swal("Ошибка", "Не все обязательные поля заполнены.", "error");
				return;
			}				
		},

		// Добавление данных.
		addDataArray: function (material, group, measure, count, date, sum) {
			purchases.oTable.aMaterials.push(material);
			purchases.oTable.aGroups.push(group);
			purchases.oTable.aAddMeasures.push(measure);
			purchases.oTable.aCountMaterials.push(+count);
			purchases.oTable.aDates.push(date);
			purchases.oTable.aSums.push(sum);
		},

		// Вставка данных в таблицу только на фронте.
		addTableRow: function () {
			let oTable = purchases.oTable;

			$('#id-request-list-table').append(`<tr>
			<td>${oTable.aMaterials[oTable.aMaterials.length - 1]}</td>
			<td>${oTable.aGroups[oTable.aGroups.length - 1]}</td>
			<td>${oTable.aAddMeasures[oTable.aAddMeasures.length - 1]}</td>
			<td>${oTable.aCountMaterials[oTable.aCountMaterials.length - 1]}</td>
			<td>${oTable.aDates[oTable.aDates.length - 1]}</td>
			<td>${oTable.aSums[oTable.aSums.length - 1]}</td></tr>`);
		},

		// Функция формирует новое коммерческое предложение.
		onFormsOffer() {
			let sUrl = "https://localhost:44312/api/purchase/forms-offer";
			let oTable = purchases.oTable;

			try {
				axios.post(sUrl, oTable)
					.then((response) => {
						console.log(response.data);
						swal("Создание коммерческого предложения", "Коммерческое предложение успешно создано.", "success");
						setTimeout(function () { window.history.go(-1); }, 3000);
					})
					.catch((XMLHttpRequest) => {
						throw new Error(XMLHttpRequest.response.data);
					});
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		// Функция загружает данные заявки.
		onGetDataRequest() {
			let iNumber = +$("#id-number-req").val();
			let sUrl = "https://localhost:44312/api/purchase/load-request?number=".concat(iNumber);
			let oTable = purchases.oTable;

			try {
				axios.get(sUrl)
					.then((response) => {
						console.log(response.data);

						// Заполнение таблицы.
						response.data.forEach(el => {
							oTable.aMaterials.push(el.material);
							oTable.aGroups.push(el.materialGroup);
							oTable.aAddMeasures.push(el.measure);
							oTable.aCountMaterials.push(el.count);
							oTable.aDates.push("нет");
							oTable.aSums.push("нет");

							$('#id-request-list-table').append(`<tr>
							<td>${oTable.aMaterials[oTable.aMaterials.length - 1]}</td>
							<td>${oTable.aGroups[oTable.aGroups.length - 1]}</td>
							<td>${oTable.aAddMeasures[oTable.aAddMeasures.length - 1]}</td>
							<td>${oTable.aCountMaterials[oTable.aCountMaterials.length - 1]}</td>
							<td>${oTable.aDates[oTable.aDates.length - 1]}</td>
							<td>${oTable.aSums[oTable.aSums.length - 1]}</td></tr>`);
						});						
					})
					.catch((XMLHttpRequest) => {
						swal("Загрузка данных заявки", "Ошибка загрузки заявки.", "error");
						throw new Error(XMLHttpRequest.response.data);
					});
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		// Функция очищает таблицу.
		onClearTable() {
			$(".content-body")[0].innerText = [];	// Очищает таблицу.	
		}
	}
});