# Цель

Реализовать систему банкомата

# Функциональные требования

- создание счета
- просмотр баланса счета
- снятие денег со счета
- пополнение счета
- просмотр истории операций

# Не функциональные требования

- интерактивный консольный интерфейс
- возможность выбора режима работы (пользователь, администратор)
    - при выборе пользователя должны быть запрошены данные счета
    - при выборе администратора должен быть запрошен системный пароль
    - при некорректном вводе пароля - система прекращает работу
- при попытке выполнения некорректных операций, должны выводиться сообщения об ошибке
- данные должны быть персистентно сохранены в базе данных (PostgreSQL)