# Pet-проект: Furniture Market API (в разработке)

RESTful API для платформы по продаже мебели, где мастера (Workers) могут выставлять свои товары, а пользователи — оставлять отзывы.
>  **.NET 8** | **PostgreSQL** | **JWT** | **Swagger**

---

##  Основные возможности
- ✅ **Аутентификация и авторизация** (JWT + Refresh Token)
- ✅ **Роли**: `User`, `Worker`, `Admin`
- ✅ **Регистрация и вход** с безопасным хранением паролей
- ✅ **Товары**: получить, создать, редактировать, удалить
- ✅ **Изображения товаров**: загрузка и хранение изображений (до 5 МБ, форматы: JPEG, JPG, PNG, GIF)
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
- Изображения для отзывов: ReviewImage (пользователи смогут прикреплять фото к отзыву)
- Тестирование: юнит и интеграционные тесты (xUnit, Moq, FluentAssertions)
- Пагинация и фильтрация: GET /products?page=1&size=10&category=chairs
- Облачное хранилище: переход с локального хранения на Cloudinary или AWS S3

`## 📊 Скриншоты`
- <img width="1920" height="1032" alt="Снимок экрана 2025-09-05 210507" src="https://github.com/user-attachments/assets/891aad31-33e9-4ed3-847a-af879f6d3b87" />
- <img width="1920" height="1032" alt="Снимок экрана 2025-09-05 210533" src="https://github.com/user-attachments/assets/28ad407a-2faf-4ea0-8777-41d4e4461fd5" />
- <img width="1920" height="1032" alt="Снимок экрана 2025-09-05 210547" src="https://github.com/user-attachments/assets/550327f0-7e91-4ab4-ac1b-ed3c09c96850" />



