# Templater
Шаблонизатор для стажировки GeekBrains

О проекте:
  .net core 5.0 WPF + MVVW + DependencyInjection + SQLite
  
Проект Templater (Основной проект (WPF) ):
  Data:
    инициализатор базы данных
    
  Docs:
    готовые документы
    
  Infrastructure:
    Commands:
      обработчики событий кнопок
    Interfaces:
      интерфесы для сервисов
    Services:
      сервисы
      
  Templates:
    документы - шаблоны
    
  ViewModels:
    хранеие моделей-представлений - для связывания с представлениями
    
  Views:
    частичные представления
    
  App.xaml
    файлы для внедрения зависимостей
  
  MainWindow.xaml
    основное представление
    
    
Проект Templater.DTO (библиатека классов):    
    Все классы для БД и не только (models)
    
    
Проект Templater.SQLite (библиатека классов):    
    Context
      TeplaterSQLDB.cs
        класс для управления бд (можно использовать LazyLoading)
      Migrations
        изменения струтуры бд
