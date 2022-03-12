# ТРПО
Back-end: Microsoft ASP.NET Core 6 + SQL Server 13 + Entity Framework + Razor

## TRPO_DM - Доменная модель
Содержит модели как части онтологии:
### Element (Элемент)
- ID: автоматически назначаемый идентификатор в БД SQL
- Data: Данные об элементе в формате JSON
- CategoryID: идентификатор категории, которой принадлежит элемент
- Category: lazy-loading для категории, которой принадлежит элемент
### Category (Категория)
- ID: автоматически назначаемый идентификатор в БД SQL
- ParentID: идентификатор родительской категории, может быть пустым
- Name: Название категории
- ParentCategory: lazy-loading для родительской категории
- Elements: lazy-loading для элементов, принадлежащих категории

## TRPO_DA - Доступ к данным
Содержит контекст данных для элементов и категорий, а также методы доступа к данным из БД

## TRPO_BL - Бизнес-логика
Содержит методы управления доступом к данным. По сути, слой безопасности между доступом к данным и интерфейсом

## TRPO_API - Интерфейс
Является приложением, взаимодействующим с БД посредством HTTP-запросов

## TRPO_MVC - Веб-клиент
Является приложением-сервером веб-сайта. Сайт заполняется данными, получаемыми при помощи запросов к API. Построен по модели Model-View-Controller:
- контроллеры принимают HTTP-запросы, создают модели и возвращают представления, отрендеренные с помощью Razor
- модели являются структурами данных, которыми заполняются представления
- представления являются страницами Razor Pages, которые автоматически заполняются данными при помощи движка Razor и отправляются пользователю как HTML-страница
- сервисы делают грязную работу контроллеров по получению данных для моделей путём общения с API