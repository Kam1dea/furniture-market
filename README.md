# Pet-проект: Furniture Market API (в разработке)

RESTful API для платформы по продаже мебели, где мастера (Workers) могут выставлять свои товары, а пользователи — оставлять отзывы.
>  **.NET 8** | **PostgreSQL** | **JWT** | **Swagger**

---

##  Основные возможности
- ✅ **Аутентификация и авторизация** (JWT + Refresh Token)
- ✅ **Роли**: `User`, `Worker`, `Admin`
- ✅ **Регистрация и вход** с безопасным хранением паролей
- ✅ **Товары**: получить, создать, редактировать, удалить
- ✅ **Изображения товаров и отзывов**: загрузка и хранение изображений (до 5 МБ, форматы: JPEG, JPG, PNG, GIF)
- ✅ **Отзывы**: получить, создать, редактировать, удалить (с привязкой к пользователю и товару)
- ✅ **Профиль мастера**: `WorkerProfile` с описанием и списком товаров
- ✅ **Полная документация API** через Swagger


##  Технологии
 Категория          | Используется 
--------------------|----------------------------------------------------------------
 **Backend**        | ASP.NET Core 8 
 **Архитектура**    | Clean Architecture (Domain, Application, Infrastructure, WebApi) 
 **База данных**    | PostgreSQL + Entity Framework Core 
 **Аутентификация** | ASP.NET Core Identity + JWT 
 **Валидация**      | FluentValidation 
 **Маппинг**        | AutoMapper 
 **Документация**   | Swagger / Swagger UI 


## Планы по развитию
- Админ-панель: назначение ролей (Worker, Admin)
- Тестирование: юнит и интеграционные тесты (xUnit, Moq, FluentAssertions)
- Пагинация и фильтрация: GET /products?page=1&size=10&category=chairs
- Облачное хранилище: переход с локального хранения на Cloudinary или AWS S3

## 📊 Скриншоты
<img width="1920" height="1032" alt="Снимок экрана 2025-09-07 211723" src="https://github.com/user-attachments/assets/17e6eaee-ecb4-4563-9eae-a5a830d259ed" />
<img width="1920" height="1032" alt="Снимок экрана 2025-09-07 211748" src="https://github.com/user-attachments/assets/53ade617-e84e-45b4-b027-aff026882041" />

