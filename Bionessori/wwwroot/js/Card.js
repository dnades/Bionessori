"use strict";

var list_card = new Vue({
	el: '#list_card',
	created() {		
		this.onLoadCards();
		
		if (localStorage["user"]) {
			RoleBase.initRole();
		}

		if (localStorage["selectCard"]) {
			this.aSelectCard = JSON.parse(localStorage["selectCard"]);
			console.log("Выбранная карта", this.aSelectCard);
		}
	},
	data: {
		aCards: [],
		aSelectCard: [],
		aFindCard: [],
		visibleYear: false,
		visibleDo: false,
		visibleReg: false
	},
	methods: {
		onLoadCards() {
			let sUrl = "https://localhost:44312/api/data/card/get-cards";

			// Отправляет данные на Back-end.
			axios.post(sUrl, {})
				.then((response) => {
					console.log("Список карт", response.data);

					this.aCards = response.data;

					this.aCards.forEach(el => {
						// Форматирует дату и время рождения.
						let tempDate = new Date(el.dateOfBirth).toLocaleDateString();
						el.dateOfBirth = tempDate;

						// Форматирует дату и время записей на процедуры.
						let tempProc = new Date(el.timeProcRecommend).toLocaleDateString();
						el.timeProcRecommend = tempProc;
					});
				})
				.catch((XMLHttpRequest) => {
					console.log("Ошибка получения списка карт пациентов", XMLHttpRequest.response.data);
				});
		},

		// Функция выводит карту для редактирования.
		onEditCard(event) {
			// Получает Id карты, на которую нажали.
			let cardNumber = $(event.target).parent().parent().parent()[0].textContent.split(" ")[2];

			// Записывает выделенную карту в массив.
			localStorage["selectCard"] = JSON.stringify(this.aCards.filter(el => el.cardNumber == cardNumber));

			window.location.href = "https://localhost:44312/route/edit-card";
		},

		// Функция удаляет карту пациента.
		onDeleteCard(event) {
			// Получает номер карты, на которую нажали.
			let cardNumber = $(event.target).parent().parent().parent()[0].textContent.split(" ")[2];

			// Записывает выделенную карту в массив.
			this.aSelectCard = this.aCards.filter(el => el.cardNumber == cardNumber);
		},

		// Функция редактирует карту пациента.
		onSaveEditChange() {
			let sUrl = "https://localhost:44312/api/data/card/update-card";
			let cardNumber = JSON.parse(localStorage["selectCard"])[0].cardNumber;
			let sFullName = $("#id-family").val();	// ФИО пациента.
			let dDateBirth;

			$("#id-check-year").prop("checked") == false ? dDateBirth = $("#id-date-year").val().replace(/-/g, "/") :
				dDateBirth = $("#id-date-year-new").val().replace(/-/g, "/");
			
			let sAddress = $("#id-address").val();	// Адрес пациента.
			let sNumber = $("#id-number").val();	// Телефон пациента.
			let sBloodGroup = $("#id-blood-group").val();	// Группа крови.
			let sPolicy = $("#id-policy").val();	// Полис пациента.
			let sDoc = $("#id-doc").val();	// Доктор.
			let sCategory = $("#id-category").val();	// Категория пациента.
			let sSeatWork = $("#id-word").val();	// Место работы.
			let sPosition = $("#id-position").val();	// Должность пациента.
			let sTabNumber = $("#id-tab").val();	// Таб №.
			let sInsurance = $("#id-insurance-company").val();	// Страховая компания.

			let sDateTo;	// Обслуж.до
			$("#id-check-do").prop("checked") == false ? sDateTo = $("#id-do").val().replace(/-/g, "/") :
				sDateTo = $("#id-do-new").val().replace(/-/g, "/");

			let sComment = $("#id-comment").val();	// Комментарии.
			let sIndicator = $("#id-indicator").val();	// Сигнальная информация.
			let isVich = $("#id-vich").val();	// ВИЧ.
			let isHb = $("#id-hb").val();	// Hb.
			let isRw = $("#id-rw").val();	// Rw.
			let sCity = $("#id-city").val();	// Город.
			let sDopAddress = $("#id-address-dop").val();	// Дополнительный адрес.
			let sDop = $("#id-dop").val();	// Дополнительное описание.
			let sDistrict = $("#id-district").val();	// Район.
			let sRegion = $("#id-region").val();	// Регион.
			let sFormPay = $("#id-form-pay").val();	// Форма оплаты.
			let sPlan = $("#id-plan").val();	// Тариф.

			let sRegistry;	// Зарегистрирован.
			$("#id-check-reg").prop("checked") == false ? sRegistry = $("#id-date-registry").val().replace(/-/g, "/") :
				sRegistry = $("#id-date-registry-new").val().replace(/-/g, "/");

			let sWhoChange = $("#id-change").val().replace(/-/g, "/");		// Изменен.
			let sOperator = $("#id-operator").val();	// Оператор.
			let sEmail = $("#id-email-address").val();
			let sIndex = $("#id-index").val();

			let oCard = {
				CardNumber: cardNumber,
				FullName: sFullName,
				DateOfBirth: new Date(dDateBirth).toLocaleDateString(),
				Address: sAddress,
				Number: sNumber,
				BloodGroup: sBloodGroup,
				Policy: sPolicy,
				Doctor: sDoc,
				Category: sCategory,
				SeatWork: sSeatWork,
				Position: sPosition,
				TabNum: sTabNumber,
				InsuranceCompany: sInsurance,
				DateTo: new Date(sDateTo).toLocaleDateString(),
				Comment: sComment,
				Indicator: sIndicator,
				isVich: isVich,
				isHb: isHb,
				isRw: isRw,
				City: sCity,
				District: sDistrict,
				Region: sRegion,
				FormPay: sFormPay,
				Plan: sPlan,
				Registry: new Date(sRegistry).toLocaleDateString(),
				WhoChange: sWhoChange,
				Operator: sOperator,
				Email: sEmail,
				IndexNumber: sIndex
			};

			// Отправляет данные на Back-end.
			axios.post(sUrl, oCard)
				.then((response) => {
					console.log(response);

					// Выводит модальное окно об успешном изменении карты пациента.
					$('#success-change-card-modal').modal('toggle');
				})
				.catch((XMLHttpRequest) => {
					console.log("Ошибка изменение карты.", XMLHttpRequest.response.data);
				});
		},

		// Функция удаляет карту пациента.
		onDelete() {
			let sUrl = "https://localhost:44312/api/data/card/delete-card";
			let iId = +$("#id").val();	// Id карты пациента.

			// Отправляет данные на Back-end.
			axios.post(sUrl, {
				Id: iId
			})
				.then((response) => {
					console.log(response);

					// Убирает модальное окно удаления карты пациента.
					$('#delete-card-modal').modal('toggle');

					// Выводит модальное окно об успешном удалении карты пациента.
					$('#success-delete-card-modal').modal('toggle');
				})
				.catch((XMLHttpRequest) => {
					console.log("Ошибка удаления карты.", XMLHttpRequest.response.data);
				});
		},

		// Функция создает новую карту пациента.
		onCreateCard() {
			let sUrl = "https://localhost:44312/api/data/card/create-card";
			let sFullName = $("#id-family").val();	// ФИО пациента.
			let dDateBirth = $("#id-date-year").val().replace(/-/g, "/");	// Дата рождения пациента.
			let sAddress = $("#id-address").val();	// Адрес пациента.
			let sNumber = $("#id-number").val();	// Телефон пациента.
			let sBloodGroup = $("#id-blood-group").val();	// Группа крови.
			let sPolicy = $("#id-policy").val();	// Полис пациента.
			let sDoc = $("#id-doc").val();	// Доктор.
			let sCategory = $("#id-category").val();	// Категория пациента.
			let sSeatWork = $("#id-word").val();	// Место работы.
			let sPosition = $("#id-position").val();	// Должность пациента.
			let sTabNumber = $("#id-tab").val();	// Таб №.
			let sInsurance = $("#id-insurance-company").val();	// Страховая компания.
			let sDateTo = $("#id-do").val().replace(/-/g, "/");	// Обслуж.до
			let sComment = $("#id-comment").val();	// Комментарии.
			let sIndicator = $("#id-indicator").val();	// Сигнальная информация.
			let isVich = $("#id-vich").val();	// ВИЧ.
			let isHb = $("#id-hb").val();	// Hb.
			let isRw = $("#id-rw").val();	// Rw.
			let sCity = $("#id-city").val();	// Город.
			let sDopAddress = $("#id-address-dop").val();	// Дополнительный адрес.
			let sDop = $("#id-dop").val();	// Дополнительное описание.
			let sDistrict = $("#id-district").val();	// Район.
			let sRegion = $("#id-region").val();	// Регион.
			let sFormPay = $("#id-form-pay").val();	// Форма оплаты.
			let sPlan = $("#id-plan").val();	// Тариф.
			let sRegistry = $("#id-date-registry").val().replace(/-/g, "/");	// Зарегистрирован.
			let sWhoChange = $("#id-change").val().replace(/-/g, "/");		// Изменен.
			let sOperator = $("#id-operator").val();	// Оператор.
			let sEmail = $("#id-email-address").val();

			let oCard = {
				FullName: sFullName,
				DateOfBirth: dDateBirth,
				Address: sAddress,
				Number: sNumber,
				BloodGroup: sBloodGroup,
				Policy: sPolicy,
				Doctor: sDoc,
				Category: sCategory,
				SeatWork: sSeatWork,
				Position: sPosition,
				TabNum: sTabNumber,
				InsuranceCompany: sInsurance,
				DateTo: sDateTo,
				Comment: sComment,
				Indicator: sIndicator,
				isVich: isVich,
				isHb: isHb,
				isRw: isRw,
				City: sCity,
				DopAddress: sDopAddress,
				Dop: sDop,
				District: sDistrict,
				Region: sRegion,
				FormPay: sFormPay,
				Plan: sPlan,
				Registry: sRegistry,
				WhoChange: sWhoChange,
				Operator: sOperator,
				Email: sEmail
			};

			// Отправляет данные на Back-end.
			axios.post(sUrl, oCard)
				.then((response) => {
					console.log("Карта пациента успешно создана." , response);
				})
				.catch((XMLHttpRequest) => {
					console.log("Ошибка создания карты.", XMLHttpRequest.response.data);
				});
		},

		// Функция сортирует таблицу со списком карт пациентов.
		sortedList(event) {
			let sParam = event.target.value;

			switch (sParam) {
				case 'card_number':
					return this.aCards.sort(BaseClass.sortByCardNumber);

				case 'fio':
					return this.aCards.sort(BaseClass.sortByFio);

				case 'year_of_birth':
					return this.aCards.sort(BaseClass.sortByYearOfBirth);

				case 'address':
					return this.aCards.sort(BaseClass.sortByAddress);

				case 'number':
					return this.aCards.sort(BaseClass.sortByNumber);

				case 'policy':
					return this.aCards.sort(BaseClass.sortByPolicy);

				case 'snails':
					return this.aCards.sort(BaseClass.sortBySnails);

				case 'date_time_proc':
					return this.aCards.sort(BaseClass.sortByDateTimeProc);

				case 'drugs':
					return this.aCards.sort(BaseClass.sortByDrugs);

				case 'diagnosis':
					return this.aCards.sort(BaseClass.sortByDiagnosis);

				case 'recommends':
					return this.aCards.sort(BaseClass.sortByRecommends);

				case 'history':
					return this.aCards.sort(BaseClass.sortByHistory);

				case 'doctor':
					return this.aCards.sort(BaseClass.sortByDoctor);

				default: return this.aCards;
			}
		},

		// Функция ищет карту пациента в таблице.
		// Поиск работает пока только по № карты или ФИО пациента.
		searchCard() {
			let searchCard = $("#id-search").val();
			let aFirstTemp;
			let aSecondTemp;
			let aRes = [];
			let aTemp = [];

			// Если поиск пустой, значит нужно снова загрузить все карты.
			if (searchCard == "") {
				this.onLoadCards();
			}

			for (let value1 of Object.values(this.aCards)) {
				for (let value2 of Object.values(value1)) {
					if (value2 !== null && value2 !== undefined) {
						aTemp.push(value2.match(searchCard));
						aFirstTemp = aTemp.filter(el => el !== null && el !== undefined);
					}
				}
			}

			aFirstTemp.map(el => {
				console.log(el.input);	// TODO: хорошо для дебага.
				aSecondTemp = el.input;
			});

			// Ищет по ФИО пациента.
			aRes = this.aCards.filter(el => el.fullName == aSecondTemp);

			// Если не найдено по ФИО пациента, то ищет по номеру карты.
			if (!aRes.length) {
				aRes = this.aCards.filter(el => el.cardNumber == +aSecondTemp);
			}

			// Записывает карты, которые соответствуют условиям поиска.
			this.aCards = aRes;
		},

		// Функция выгружает список карт в Excel.
		onExportExcel() {
			let wb = XLSX.utils.table_to_book(document.getElementById('id-card-list-table'), { sheet: "Список карт" });
			let wbout = XLSX.write(wb, { bookType: 'xls', bookSST: true, type: 'binary' });

			saveAs(new Blob([s2ab(wbout)], { type: "application/octet-stream" }), 'Список карт пациентов.xls');

			function s2ab(s) {
				let buf = new ArrayBuffer(s.length);
				let view = new Uint8Array(buf);

				for (let i = 0; i < s.length; i++) view[i] = s.charCodeAt(i) & 0xFF;

				return buf;
			}			
		},

		// Функция передает роут в главную точку распределения маршрутов.
		onRouteCreateCard(event) {
			main.onRouteMatched(event);
		},

		// Функция получает конкретную карту.
		onGetCard(event) {
			// Получает Id карты, на которую нажали.
			let cardId = $(event.target).parent().parent()[0].textContent.split(" ")[2];

			// Записывает выделенную карту в массив.
			localStorage["selectCard"] = JSON.stringify(this.aCards.filter(el => el.cardNumber == cardId));
			// Форматирует дату и время рождения.
			//let tempDate = new Date(el.dateOfBirth).toLocaleDateString();
			//el.dateOfBirth = tempDate;

			//// Форматирует дату и время записей на процедуры.
			//let tempProc = new Date(el.timeProcRecommend).toLocaleDateString();
			//el.timeProcRecommend = tempProc;

			window.location.href = "https://localhost:44312/route/get-card";
		}
	},
});