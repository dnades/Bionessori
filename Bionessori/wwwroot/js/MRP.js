"use strict";

var main_mrp = new Vue({
	el: '#main_mrp',
	created() {
		this.loadMaterials();
		this.loadRequests();
		this.loadNameWerehouses();
		this.loadGroupsMaterials();
		this.loadDistinctMaterials();
		this.loadMeasures();

		// Блокирует поля от изменений в модальных окнах просмотра деталей, но не блокирует копирование.
		$(".not-edit").prop("disabled", true);	

		if (localStorage["selectRequest"]) {
			this.aSelectRequest = JSON.parse(localStorage["selectRequest"]);
			console.log("Выбранная заявка", this.aSelectRequest);
		}

		// Еслим список материалов к заявке уже добавляли, то запишет их в массив.
		if (localStorage["addedMaterials"]) {
			this.aAddedMaterials = JSON.parse(localStorage["addedMaterials"]);
			console.log("Материалы заявки.", this.aAddedMaterials);
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
		aAddedMaterials: []
	},
	methods: {
		// Функция загружает список материалов.
		loadMaterials() {
			let sUrl = "https://localhost:44312/api/werehouse/material/get-materials";
			
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

					// Парсит объект заявки с материалами.
					this.aRequests.forEach(el => el.material = JSON.parse(el.material));
					
					console.log("Список заявок.", this.aRequests);
				})
				.catch((XMLHttpRequest) => {
					console.log("Ошибка получения списка заявок.", XMLHttpRequest.response.data);
				});
		},

		// Функция получает конкретную заявку.
		onGetRequest(event) {
			let reqId = $(event.target).parent().parent()[0].textContent.split(" ")[0];

			// Находит заявку, на которую нажали.
			localStorage["selectRequest"] = JSON.stringify(this.aRequests.filter(el => el.number == reqId));

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

		// Функция перенаправляет роут к списку материалов MRP.
		onRouteMaterial(event) {
			main.onRouteMatched(event);	// Передает роут в главную точку роутов.
		},

		// Функция получает список названий складов.
		loadNameWerehouses() {
			let sUrl = "https://localhost:44312/api/werehouse/material/get-werehouses";

			axios.post(sUrl, {})
				.then((response) => {
					this.aWerehousesNames = response.data;
					console.log("Список названий складов.", this.aWerehousesNames);
				})
				.catch((XMLHttpRequest) => {
					console.log("Ошибка получения названий складов.", XMLHttpRequest.response.data);
				});
		},

		// Функция получает список групп материалов.
		loadGroupsMaterials() {
			let sUrl = "https://localhost:44312/api/werehouse/material/get-groups";

			axios.post(sUrl, {})
				.then((response) => {
					this.aMaterialsGroups = response.data;
					console.log("Список групп.", this.aMaterialsGroups);
				})
				.catch((XMLHttpRequest) => {
					console.log("Ошибка получения списка групп.", XMLHttpRequest.response.data);
				});
		},

		// Функция получает список материалов без дублей.
		loadDistinctMaterials() {
			let sUrl = "https://localhost:44312/api/werehouse/material/get-distinct-materials";

			axios.post(sUrl, {})
				.then((response) => {
					this.aDistinctMaterials = response.data;
					console.log("Список материалов без дублей.", this.aDistinctMaterials);
				})
				.catch((XMLHttpRequest) => {
					console.log("Ошибка получения списка материалов без дублей.", XMLHttpRequest.response.data);
				});
		},

		// Функция получает список ед.изм.
		loadMeasures() {
			let sUrl = "https://localhost:44312/api/werehouse/material/get-measures";

			axios.post(sUrl, {})
				.then((response) => {
					this.aMeasures = response.data;
					console.log("Список ед.изм.", this.aMeasures);
				})
				.catch((XMLHttpRequest) => {
					console.log("Ошибка получения ед.изм.", XMLHttpRequest.response.data);
				});
		},

		// Функция создает новую заявку на потребность в материалах.
		onCreateRequest() {			
			let sMaterial = $("#id-select-material").val();
			let sGroup = $("#id-select-group").val();
			let nCount = +$("#id-select-count").val();
			let sMeasure = $("#id-select-measure").val();
			let sWerehouse = $("#id-select-werehouse").val();

			let oRequest = {
				Material: main_mrp.aAddedMaterials,
				MaterialGroup: sGroup,
				Measure: sMeasure,
				Count: nCount,
				WerehouseNumber: sWerehouse
			};

			let sUrl = "https://localhost:44312/api/werehouse/request/create-request";

			axios.post(sUrl, oRequest)
				.then((response) => {
					console.log("Заявка на потребность успешно создана.", response);
				})
				.catch((XMLHttpRequest) => {
					console.log("Ошибка создания заявки на потребность.", XMLHttpRequest.response.data);
				});
		},

		// Функция передает роут создания заявки в главную точку роутинга.
		onRouteCreateRequest(event) {	
			// Очищает список материалов заявки.
			localStorage.removeItem("addedMaterials");
			main.onRouteMatched(event);
		},

		// Функция добавляет материал к заявке.
		onAddMaterialRequest() {
			this.aAddedMaterials.push($("#id-select-material").val());
		},

		// Функция переходит к редактированию заявки.
		onRouteEditRequest(event) {
			// Очищает список материалов заявки.
			localStorage.removeItem("addedMaterials");
			let reqId = $(event.target).parent().parent().parent()[0].textContent.split(" ")[1];

			// На всякий случай чистит массив материалов заявки.
			this.aAddedMaterials = [];

			// Находит заявку, на которую нажали.
			localStorage["selectRequest"] = JSON.stringify(this.aRequests.filter(el => el.number == reqId));

			this.aRequests[0].material.forEach(el => {
				this.aAddedMaterials.push(el);
			});
			localStorage["addedMaterials"] = JSON.stringify(this.aAddedMaterials);	

			window.location.href = "https://localhost:44312/edit-request";
		},

		// Функция получает материалы группы.
		onChangeGroup(event) {
			console.log("change group", event.target.value);
			let sGroup = event.target.value;			

			let sUrl = "https://localhost:44312/api/werehouse/material/get-material-group?group=".concat(sGroup);

			axios.get(sUrl)
				.then((response) => {
					console.log("Материалы группы", response.data);
					this.aDistinctMaterials = response.data;
				})
				.catch((XMLHttpRequest) => {
					console.log("Ошибка получения материалов группы", XMLHttpRequest.response.data);
				});
		},

		// Функция удаляет материал из заявки (удаления на бэке не произодет, если изменения в заявке не были сохранены).
		onDeleteMaterialRequest(event) {
			let elem = $(event.target).parent().parent().parent()[0].textContent.split(" × ")[0];

			// Оставляет в массиве лишь те материалы, которые не равны выбранному.
			let temp = this.aAddedMaterials.filter(el => el !== elem);
			this.aAddedMaterials = temp;
		}
	}
});