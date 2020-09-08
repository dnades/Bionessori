"use strict";

var main_mrp = new Vue({
	el: '#main_mrp',
	mounted() {
		this.onInit();
		this.loadMaterials();
		this.loadRequests();
		this.loadNameWerehouses();
		this.loadGroupsMaterials();
		this.loadDistinctMaterials();
		this.loadMeasures();
		this.getCountRequestStatusNew();
		this.getCountRequestStatusWork();
		this.getCountMaterialsRefill();
		this.getCountMaterialsMapping();
		this.getCountAcceptDeleteRequests();

		// Блокирует поля от изменений в модальных окнах просмотра деталей, но не блокирует копирование.
		$(".not-edit").prop("disabled", true);

		if (localStorage["selectRequest"]) {
			this.aSelectRequest = JSON.parse(localStorage["selectRequest"]);

			// Материалы выбранной заявки.
			this.aEditMaterials = JSON.parse(localStorage["addedMaterials"]).aMaterials;

			// Группы выбранной заявки.
			this.aEditGroups = JSON.parse(localStorage["addedMaterials"]).aGroups;

			// Кол-во материалов выбранной заявки.
			this.aEditCounts = JSON.parse(localStorage["addedMaterials"]).aCounts;

			// Ед.Изм. материалов выбранной заявки.
			this.aEditMeasures = JSON.parse(localStorage["addedMaterials"]).aMeasures;
			console.log("Выбранная заявка", this.aSelectRequest);
			console.log("Выбранная заявка (полная)", JSON.parse(localStorage["addedMaterials"]));
		}

		// Еслим список материалов к заявке уже добавляли, то запишет их в массив.
		if (localStorage["addedMaterials"]) {
			this.aAddedMaterials = JSON.parse(localStorage["addedMaterials"]);
			console.log("Материалы заявки", this.aAddedMaterials);
		}
	},
	data: {
		aMaterials: [],
		aRequests: [],
		concreteRequest: [],
		concreteMaterial: [],
		aWerehousesNames: [],
		aMaterialsGroups: [],
		aDistinctMaterials: [],
		aMeasures: [],
		aSelectRequest: [],
		aAddedMaterials: [],
		visibleGroup: false,
		visibleMaterial: false,
		visibleMeasure: false,
		werehouseNum: false,
		selectedRequests: [],
		countNewRequests: null,
		countRequestsInWork: null,
		countRefillMaterials: null,
		countMappingMaterials: null,
		countAcceptDeleteReq: null,
		aNewRequests: [],
		aInWorkRequests: [],
		aAcceptDeleteRequests: [],
		aRefillMaterials: [],
		aCountMaterials: [],
		aMeasuresMaterials: [],
		aGroups: [],
		aMappingMaterials: [],
		aEditMaterials: [],
		aEditGroups: [],
		aEditCounts: [],
		aEditMeasures: [],
		isStateGroup: null,
		isStateMaterial: null,
		isStateCount: null,
		isStateMeasure: null
	},
	methods: {
		onInit() {
			console.log("oninit");
			let sParam = localStorage["dynamicParam"];
			sParam !== null && sParam !== "" ? this.onGetDynamicData(sParam) : null;
		},

		// Функция получает динамические данные определенной структуры.
		onGetDynamicData(sParam) {
			localStorage["dynamicParam"] = sParam;
			console.log("dynamicParam", sParam);

			switch (sParam) {
				// Получить новые заявки.
				case "new_req":
					var sUrlNewReq = "https://localhost:44312/api/werehouse/request/get-new-requests";

					try {
						axios.get(sUrlNewReq)
							.then((response) => {
								this.aNewRequests = response.data;
								console.log("Список новых заявок", this.aNewRequests);
							})
							.catch((XMLHttpRequest) => {
								throw new Error("Ошибка получения списка новых заявок", XMLHttpRequest.response.data);
							});
					}
					catch (ex) {
						throw new Error(ex);
					}
					break;

				// Получить заявки в работе.
				case "inwork_req":
					var sUrlInWorkReq = "https://localhost:44312/api/werehouse/request/get-inwork-requests";

					try {
						axios.get(sUrlInWorkReq)
							.then((response) => {
								this.aInWorkRequests = response.data;
								console.log("Список заявок в работе", this.aInWorkRequests);
							})
							.catch((XMLHttpRequest) => {
								throw new Error("Ошибка получения заявок в работе", XMLHttpRequest.response.data);
							});
					}
					catch (ex) {
						throw new Error(ex);
					}
					break;

				// Получить заявки ожидающих подтверждения удаления.
				case "accept_del_req":
					var sUrlAcceptDeleteReq = "https://localhost:44312/api/werehouse/request/get-accept-delete-requests";

					try {
						axios.get(sUrlAcceptDeleteReq)
							.then((response) => {
								this.aAcceptDeleteRequests = response.data;
								console.log("Список заявок ожидающие подтверждения удаления", this.aAcceptDeleteRequests);
							})
							.catch((XMLHttpRequest) => {
								throw new Error("Ошибка получения заявок ожидающих подтверждения удаления", XMLHttpRequest.response.data);
							});
					}
					catch (ex) {
						throw new Error(ex);
					}
					break;

				// Получить материалы для пополнения.
				case "ref_mat":
					var sUrlRefill = "https://localhost:44312/api/werehouse/request/get-refill-materials";

					try {
						axios.post(sUrlRefill)
							.then((response) => {
								this.aRefillMaterials = response.data;
								console.log("Материалы для пополнения", this.aRefillMaterials);
							})
							.catch((XMLHttpRequest) => {
								throw new Error("Ошибка получения материалов для пополнения", XMLHttpRequest.response.data);
							});
					}
					catch (ex) {
						throw new Error(ex);
					}
					break;

				// Получить материалы для сопоставления.
				case "mapp_mat":
					var sUrlMapp = "https://localhost:44312/api/werehouse/request/";

					try {
						axios.post(sUrlMapp)
							.then((response) => {
								this.aMappingMaterials = response.data;
								console.log("Материалы для сопоставления", this.aMappingMaterials);
							})
							.catch((XMLHttpRequest) => {
								throw new Error("Ошибка получения материалов для сопоставления", XMLHttpRequest.response.data);
							});
					}
					catch (ex) {
						throw new Error(ex);
					}
					break;
			}
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

		// Функция загружает список заявок на потребности.
		loadRequests() {
			let sUrl = "https://localhost:44312/api/werehouse/request/get-requests";

			try {
				axios.post(sUrl)
					.then((response) => {
						this.aRequests = response.data;

						// Парсит объект заявки с материалами.
						//this.aRequests.forEach(el => el.material = JSON.parse(el.material));

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

		// Функция получает конкретную заявку.
		onGetRequest(event) {
			localStorage.removeItem("addedMaterials");
			let reqId = $(event.target).parent().parent()[0].textContent.split(" ")[1];

			// Находит заявку, на которую нажали.
			localStorage["selectRequest"] = JSON.stringify(this.aRequests.filter(el => el.id == reqId));

			this.aRequests[0].material.forEach(el => {
				this.aAddedMaterials.push(el);
			});

			localStorage["addedMaterials"] = JSON.stringify(this.aAddedMaterials);

			window.location.href = "https://localhost:44312/view-request";
		},

		// Функция получает конкретный материал склада.
		onGetMaterial(event) {
			let materialId = $(event.target).parent().parent()[0].textContent.split(" ")[1];

			this.concreteMaterial = this.aMaterials.filter(el => el.code == materialId);
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
			if (aMaterial.length > 0) {
				this.aMaterials = aMaterial;
			}
			// Если по названию не нашли, значит искали по группе.
			else {
				aMaterialGroup = this.aMaterials.filter(el => el.materialGroup.includes(sMaterial));
				this.aMaterials = aMaterialGroup;
			}
		},

		// Функция сортирует заявки по номеру или статусу.
		sortedRequeest(event) {
			let sParam = event.target.value;

			switch (sParam) {
				case "card_number":
					return this.aRequests.sort(BaseClass.sortByRequestNumber);

				case "req_status":
					return this.aRequests.sort(BaseClass.sortByRequestStatus);
			}
		},

		// Функция ищет заявку по ее номеру или статусу.
		onSearchRequest() {
			let sRequest = $("#id-search-request").val();	// Что ищет.
			let aRequest, aRequestStatus;

			if (sRequest == "")
				this.loadRequests();

			aRequest = this.aRequests.filter(el => el.number.includes(sRequest));

			// Если искали по номеру заявки.
			if (aRequest.length > 0) {
				this.aRequests = aRequest;
			}
			// Если по номеру заявки не нашли, значит искали по статусу заявки.
			else {
				aRequestStatus = this.aRequests.filter(el => el.status.includes(sRequest));
				this.aRequests = aRequestStatus;
			}
		},

		// Функция перенаправляет роут к списку заявок MRP.
		onRouteReq(event) {
			main.onRouteMatched(event);	// Передает роут в главную точку роутов.
		},

		// Функция получает список названий складов.
		loadNameWerehouses() {
			let sUrl = "https://localhost:44312/api/werehouse/material/get-werehouses";

			try {
				axios.post(sUrl, {})
					.then((response) => {
						this.aWerehousesNames = response.data;
						console.log("Список названий складов", this.aWerehousesNames);
					})
					.catch((XMLHttpRequest) => {
						throw new Error("Ошибка получения названий складов", XMLHttpRequest.response.data);
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

		// Функция создает новую заявку на потребность в материалах.
		onCreateRequest() {
			localStorage.removeItem("addedMaterials");

			let oRequest = {
				Material: main_mrp.aAddedMaterials,
				MaterialGroup: main_mrp.aGroups,
				Measure: main_mrp.aMeasuresMaterials,
				Count: main_mrp.aCountMaterials
			};

			let sUrl = "https://localhost:44312/api/werehouse/request/create-request";

			try {
				axios.post(sUrl, oRequest)
					.then((response) => {
						console.log("Заявка на потребность успешно создана", response);

						setTimeout(function () {
							window.location.href = "https://localhost:44312/view/request";
						}, 3000);

						swal("Создание заявки", "Заявка на потребность успешно создана.", "success");
					})
					.catch((XMLHttpRequest) => {
						swal("Ошибка", "Ошибка создания заявки на потребность.", "error");
						throw new Error("Ошибка создания заявки на потребность", XMLHttpRequest.response.data);
					});
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		// Функция передает роут создания заявки в главную точку роутинга.
		onRouteCreateRequest(event) {
			// Очищает список материалов заявки.
			localStorage.removeItem("addedMaterials");
			main.onRouteMatched(event);
		},

		// Функция добавляет материал к заявке.
		onAddMaterialRequest() {
			let sMaterial = $("#id-select-material").val();
			let sGroup = $("#id-select-group").val();
			let sMeasure = $("#id-select-measure").val();
			let iCount = +$("#id-select-count").val();

			//// Если не все поля заполнены, не дает добавить материал.
			if (!this.isStateGroup || !this.isStateMaterial || !this.isStateCount || !this.isStateMeasure) {
				swal("Внимание", "Для добавления материалов заявки необходимо заполнить все поля.", "error");
				return;
			}

			// Если материал еще не добавлен к заявке, то добавит, иначе не добавит и дублей не будет.
			if (this.aAddedMaterials.length) {
				this.aAddedMaterials.forEach(el => {
					if (el !== sMaterial) {
						this.aAddedMaterials.push(sMaterial);
						this.aGroups.push(sGroup);
						this.aMeasuresMaterials.push(sMeasure);
						this.aCountMaterials.push(iCount);
					}
					else {
						swal("Внимание", "Вы пытаетесь добавить к заявке материал, который уже добавлен. " +
							"Материал «" + sMaterial + "» уже включен в заявку.", "error");
						return;
					}
				});
			}
			else {	// Если к заявке еще не добавляли ни одного материала.
				this.aAddedMaterials.push(sMaterial);
				this.aGroups.push(sGroup);
				this.aMeasuresMaterials.push(sMeasure);
				this.aCountMaterials.push(iCount);
			}
		},

		// Функция переходит к редактированию заявки.
		onRouteEditRequest(e) {
			// Очищает список материалов заявки.
			localStorage.removeItem("addedMaterials");
			let reqId = e.currentTarget.value;	// Id нажатой заявки.
			let reqStatus = this.aRequests.filter(el => el.id == reqId)[0].status;

			// На всякий случай чистит массив материалов заявки.
			this.aAddedMaterials = [];

			if (reqStatus === "Новая" || reqStatus === "В работе") {
				// № заявки для которой нужно получить все данные.
				let numRequest = +this.aRequests.filter(el => el.id == reqId)[0].number;

				let sUrl = "https://localhost:44312/api/werehouse/request/get-request?number=".concat(numRequest);

				try {
					axios.get(sUrl)
						.then((response) => {
							console.log("Выбранная заявка", response.data);
							this.aAddedMaterials = response.data;
							localStorage["addedMaterials"] = JSON.stringify(this.aAddedMaterials);

							window.location.href = "https://localhost:44312/edit-request";
						})
						.catch((XMLHttpRequest) => {
							throw new Error("Ошибка получения данных заявки", XMLHttpRequest.response.data);
						});
				}
				catch (ex) {
					throw new Error(ex);
				}
			}
			else {
				swal("Внимание", "Статус заявки не позволяет редактировать.", "info");
				return;
			}
		},

		// Функция получает материалы группы.
		onChangeGroup(event) {
			console.log("change group", event.target.value);
			let sGroup = event.target.value;
			let sUrl = "https://localhost:44312/api/werehouse/material/get-material-group?group=".concat(sGroup);

			try {
				axios.get(sUrl)
					.then((response) => {
						console.log("Материалы группы", response.data);
						this.aDistinctMaterials = response.data;
					})
					.catch((XMLHttpRequest) => {
						throw new Error("Ошибка получения материалов группы", XMLHttpRequest.response.data);
					});
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		// Функция удаляет материал из заявки (удаления на бэке не произойдет, если изменения в заявке не были сохранены).
		onDeleteMaterialRequest(event) {
			let elem = $(event.target).parent().parent().parent()[0].textContent.split(" × ")[0];

			// Оставляет в массиве лишь те материалы, которые не равны выбранному.
			let temp = main_mrp.aEditMaterials.filter(el => el !== elem) || [];
			main_mrp.aEditMaterials = temp;
			main_mrp.aEditGroups = temp;
			main_mrp.aEditCounts = temp;
			main_mrp.aEditMeasures = temp;
		},

		// Функция сохраняет отредактированную заявку.
		onSaveChangeRequest() {
			let reqNumber = +JSON.parse(localStorage["addedMaterials"]).numberRequest;	// Номер заявки.
			let oRequest = {
				Number: reqNumber,
				Material: main_mrp.aEditMaterials,
				MaterialGroup: main_mrp.aEditGroups,
				Measure: main_mrp.aEditMeasures,
				Count: main_mrp.aEditCounts
			};

			let sUrl = "https://localhost:44312/api/werehouse/request/save-change-request";

			try {
				axios.put(sUrl, oRequest)
					.then((response) => {
						setTimeout(function () {
							window.location.href = "https://localhost:44312/view/request";
						}, 3000);

						swal("Редактирование заявки", "Заявка №" + reqNumber + " успешно изменена.", "success");
						console.log("Заявка №" + reqNumber + " успешно изменена", response);
						this.loadRequests();
					})
					.catch((XMLHttpRequest) => {
						swal("Ошибка", "Ошибка изменения заявки на потребность.", "error");
						throw new Error("Ошибка изменения заявки на потребность", XMLHttpRequest.response.data);
					});
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		onBeforeDeleteRequest() {
			let reqId = $(event.target).parent().parent().parent()[0].textContent.split(" ")[1];

			// Находит заявку, на которую нажали.
			let temp = this.aRequests.filter(el => el.id == reqId);
			this.aSelectRequest = temp;
		},

		// Функция удаляет заявку.
		onPostDeleteRequest() {
			let reqNumber = +JSON.parse(localStorage["addedMaterials"]).numberRequest;	// Номер заявки.
			let sUrl = "https://localhost:44312/api/werehouse/request/post-delete-request?number=".concat(reqNumber);

			try {
				axios.put(sUrl)
					.then((response) => {
						console.log("Заявка № " + reqNumber + " переведена в статус «Требует подтверждения удаления».", response);
						swal("Пометка заявки для удаления", "Заявка № " + reqNumber + "переведена в статус «Требует подтверждения удаления».", "success");
						this.loadRequests();
					})
					.catch((XMLHttpRequest) => {
						swal("Ошибка", "Ошибка удаления заявки на потребность", "error");
						throw new Error("Ошибка удаления заявки на потребность", XMLHttpRequest.response.data);
					});
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		// Функция выгружает список карт в Excel.
		onExportExcelRequests() {
			let wb = XLSX.utils.table_to_book(document.getElementById('id-request-list-table'), { sheet: "Список заявок" });
			let wbout = XLSX.write(wb, { bookType: 'xls', bookSST: true, type: 'binary' });

			saveAs(new Blob([s2ab(wbout)], { type: "application/octet-stream" }), 'Список заявок.xls');

			function s2ab(s) {
				let buf = new ArrayBuffer(s.length);
				let view = new Uint8Array(buf);

				for (let i = 0; i < s.length; i++) view[i] = s.charCodeAt(i) & 0xFF;

				return buf;
			}
		},

		// Функция выгружает список материалов в Excel.
		onExportExcelMaterials() {
			let wb = XLSX.utils.table_to_book(document.getElementById('id-material-list-table'), { sheet: "Список материалов" });
			let wbout = XLSX.write(wb, { bookType: 'xls', bookSST: true, type: 'binary' });

			saveAs(new Blob([s2ab(wbout)], { type: "application/octet-stream" }), 'Список материалов.xls');

			function s2ab(s) {
				let buf = new ArrayBuffer(s.length);
				let view = new Uint8Array(buf);

				for (let i = 0; i < s.length; i++) view[i] = s.charCodeAt(i) & 0xFF;

				return buf;
			}
		},

		onCheckedReq() {
			console.log(this.selectedRequests);
		},

		// Функция получает кол-во заявок со статусом "Новая".
		getCountRequestStatusNew() {
			let sUrl = "https://localhost:44312/api/werehouse/material/count-status-new";

			try {
				axios.get(sUrl)
					.then((response) => {
						console.log("Кол-во заявок со статусом - Новая: ", response.data);
						this.countNewRequests = response.data;
					})
					.catch((XMLHttpRequest) => {
						throw new Error("Ошибка получения кол-ва заявок со статусом - Новая", XMLHttpRequest.response.data);
					});
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		// Функция получает кол-во заявок со статусом "В работе".
		getCountRequestStatusWork() {
			let sUrl = "https://localhost:44312/api/werehouse/material/count-status-work";

			try {
				axios.get(sUrl)
					.then((response) => {
						console.log("Кол-во заявок со статусом - В работе: ", response.data);
						this.countRequestsInWork = response.data;
					})
					.catch((XMLHttpRequest) => {
						throw new Error("Ошибка получения кол-ва заявок со статусом - В работе", XMLHttpRequest.response.data);
					});
			}
			catch (ex) {
				throw new Error(ex);
			}
		},
	
		// Функция получает кол-во материалов, требующих пополнения.
		getCountMaterialsRefill() {
			let sUrl = "https://localhost:44312/api/werehouse/material/count-refill-materials";

			try {
				axios.get(sUrl)
					.then((response) => {
						console.log("Кол-во материалов, требующих пополнения: ", response.data);
						this.countRefillMaterials = response.data;
					})
					.catch((XMLHttpRequest) => {
						throw new Error("Ошибка получения материалов, требующих пополнения", XMLHttpRequest.response.data);
					});
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		// Функция получает кол-во материалов, требующих сопоставления.
		getCountMaterialsMapping() {
			let sUrl = "https://localhost:44312/api/werehouse/material/count-mapping-materials";

			try {
				axios.get(sUrl)
					.then((response) => {
						console.log("Кол-во материалов, требующих сопоставления: ", response.data);
						this.countMappingMaterials = response.data;
					})
					.catch((XMLHttpRequest) => {
						throw new Error("Ошибка получения материалов, требующих сопоставления", XMLHttpRequest.response.data);
					});
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		// Функция получает кол-во заявок, требующих подтверждения.
		getCountAcceptDeleteRequests() {
			let sUrl = "https://localhost:44312/api/werehouse/material/count-accept-delete-request";

			try {
				axios.get(sUrl)
					.then((response) => {
						console.log("Кол-во заявок, требующих подтверждения удаления: ", response.data);
						this.countAcceptDeleteReq = response.data;
					})
					.catch((XMLHttpRequest) => {
						throw new Error("Ошибка получения заявок, требующих подтверждения удаления", XMLHttpRequest.response.data);
					});
			}
			catch (ex) {
				throw new Error(ex);
			}
		},

		// Функция отправляет заявку в отдел закупок.
		onSendPurchases() {
			let reqNumber = +main_mrp.selectedRequests;
			let sUrl = "https://localhost:44312/api/werehouse/request/change-status-inwork?number=".concat(reqNumber);

			try {
				axios.get(sUrl)
					.then((response) => {
						this.loadRequests();
						main_mrp.selectedRequests = [];	// Убирает чекбокс выбранной заявки.
						console.log(response.data);
						swal("Отправка заявки в отдел закупок", "Заявка успешно отправлена в отдел закупок.", "success");						
					})
					.catch((XMLHttpRequest) => {
						main_mrp.selectedRequests = [];

						if (XMLHttpRequest.response.status === 400) {
							swal("Отправка заявки в отдел закупок", XMLHttpRequest.response.data.data.responseText, "error");
							return;
						}
						swal("Отправка заявки в отдел закупок", "Ошибка отправки заявки в отдел закупок. Заявка не была отправлена.", "error");
						throw new Error("Ошибка изменения статуса заявки", XMLHttpRequest.response.data);
					});
			}
			catch (ex) {
				throw new Error(ex);
			}
		},
	}
});