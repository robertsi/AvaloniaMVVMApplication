using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using AvaloniaMVVMApplication.Models;
using AvaloniaMVVMApplication.Services;
using ReactiveUI;

namespace AvaloniaMVVMApplication.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase content;

        public MainWindowViewModel(Database db)
        {
            Content = List = new TodoListViewModel(db.GetItems());
        }

        public TodoListViewModel List { get; }

        public ViewModelBase Content
        {
            get => content;
            private set => this.RaiseAndSetIfChanged(ref content, value);
        }

        public void AddItem()
        {
            var vm = new AddItemViewModel();
            Observable.Merge(
                    vm.Ok,
                    vm.Cancel.Select(_ => (TodoItem) null))
                .Take(1)
                .Subscribe(model =>
                {
                    if (model != null)
                    {
                        List.Items.Add(model);
                    }

                    Content = List;
                });

            Content = vm;
        }

    }
}
