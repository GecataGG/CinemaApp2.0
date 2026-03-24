using System;
using System.Windows.Input;

namespace CinemaApp.Wpf.HelpersWpf
{
    public class RelayCommandWpf : ICommand
    {
        private readonly Action<object?> execute;
        private readonly Func<object?, bool>? canExecute;

        // Конструкторът приема два параметъра:
        // - Action (което е методът, който ще се изпълнява)
        // - Func (което е методът за проверка дали командата може да се изпълни)
        public RelayCommandWpf(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));  // Проверка дали execute не е null
            this.canExecute = canExecute;  // Функция за проверка дали командата може да се изпълни
        }

        // Събитие за CanExecuteChanged, което ще се извиква, когато може да се промени състоянието на командата
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;  // Това добавя събитието за CanExecute
            remove => CommandManager.RequerySuggested -= value;  // Това премахва събитието за CanExecute
        }

        // Метод за проверка дали командата може да бъде изпълнена
        public bool CanExecute(object? parameter)
        {
            // Ако canExecute е null, командата може да се изпълни
            return canExecute == null || canExecute(parameter);
        }

        // Метод за изпълнение на командата
        public void Execute(object? parameter)
        {
            execute(parameter);  // Изпълнява предадената логика
        }
    }
}